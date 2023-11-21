using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer {
    public class SprintAccessor : ISprintAccessor {
        public int CreateSprint(Sprint sprint) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_sprint";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FeatureID", SqlDbType.Int);
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);

            cmd.Parameters["@FeatureID"].Value = sprint.FeatureID;
            cmd.Parameters["@StartDate"].Value = sprint.StartDate;
            cmd.Parameters["@EndDate"].Value = sprint.EndDate;

            try {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public List<SprintVM> SelectSprintVMsByProjectID(string projectID) {
            List<SprintVM> result = new List<SprintVM>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_project_sprints";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@ProjectID"].Value = projectID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        SprintVM s = new SprintVM();
                        s.SprintID = reader.GetInt32(0);
                        s.FeatureID = reader.GetInt32(1);
                        s.StartDate = reader.GetDateTime(2);
                        s.EndDate = reader.GetDateTime(3);
                        s.Active = reader.GetBoolean(4);
                        s.FeatureName = reader.GetString(5);
                        result.Add(s);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public SprintVM SelectSprintVMBySprintID(int sprintID) {
            SprintVM result = new SprintVM();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_sprint_by_sprintid";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@SprintID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@SprintID"].Value = sprintID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    if (reader.Read()) {
                        result.SprintID = reader.GetInt32(0);
                        result.FeatureID = reader.GetInt32(1);
                        result.StartDate = reader.GetDateTime(2);
                        result.EndDate = reader.GetDateTime(3);
                        result.Active = reader.GetBoolean(4);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public SprintVM SelectSprintVMByFeatureID(int featureID) {
            SprintVM result = new SprintVM();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_sprint_by_featureid";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@FeatureID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@FeatureID"].Value = featureID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    if (reader.Read()) {
                        result.SprintID = reader.GetInt32(0);
                        result.FeatureID = reader.GetInt32(1);
                        result.StartDate = reader.GetDateTime(2);
                        result.EndDate = reader.GetDateTime(3);
                        result.Active = reader.GetBoolean(4);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }
    }
}
