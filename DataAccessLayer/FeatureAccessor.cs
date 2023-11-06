using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class FeatureAccessor : IFeatureAccessor {
        public Feature SelectFeatureByFeatureID(int featureID) {
            throw new NotImplementedException();
        }

        public List<Feature> SelectFeaturesByProjectID(string projectID) {
            List<Feature> result = new List<Feature>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_project_features";

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
                        Feature f = new Feature();
                        f.FeatureID = reader.GetInt32(0);
                        f.ProjectID = reader.GetString(1);
                        f.Name = reader.GetString(2);
                        f.Description = reader.GetString(3);
                        f.Status = reader.GetString(4);
                        
                        result.Add(f);
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
