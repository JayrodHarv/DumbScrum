using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace DataAccessLayer {
    public class ProjectAccessor : IProjectAccessor {
        public List<Project> SelectAllProjects() {
            List<Project> result = new List<Project>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_all_projects";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if(reader.HasRows) {
                    while(reader.Read()) {
                        Project p = new Project();
                        p.ProjectID = reader.GetString(0);
                        p.ProjectOwner = reader.GetString(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        result.Add(p);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public ProjectVM SelectProjectVMByProjectID(string projectID) {
            ProjectVM result = new ProjectVM();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_project_by_projectid";

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
                    if (reader.Read()) {
                        result.ProjectID = reader.GetString(0);
                        result.ProjectOwner = reader.GetString(1);
                        result.DateCreated = reader.GetDateTime(2);
                        result.Status = reader.GetString(3);
                        result.Description = reader.GetString(4);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            List<Project> result = new List<Project>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_user_projects";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        Project p = new Project();
                        p.ProjectID = reader.GetString(0);
                        p.ProjectOwner = reader.GetString(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        result.Add(p);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public int CreateProject(string projectID, string projectOwner, string description, int userID) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_project";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ProjectOwner", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@ProjectOwner"].Value = projectOwner;
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@UserID"].Value = userID;

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
    }
}
