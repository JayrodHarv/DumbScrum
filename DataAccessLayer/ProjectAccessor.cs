using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace DataAccessLayer {
    public class ProjectAccessor : IProjectAccessor {
        public List<ProjectVM> SelectAllProjects() {
            List<ProjectVM> result = new List<ProjectVM>();

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
                        ProjectVM p = new ProjectVM();
                        p.ProjectID = reader.GetString(0);
                        p.UserID = reader.GetInt32(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        p.ProjectOwner = reader.GetString(5);
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
                        result.UserID = reader.GetInt32(1);
                        result.DateCreated = reader.GetDateTime(2);
                        result.Status = reader.GetString(3);
                        result.Description = reader.GetString(4);
                        result.ProjectOwner = reader.GetString(5);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public List<ProjectVM> SelectProjectsByUserID(int userID) {
            List<ProjectVM> result = new List<ProjectVM>();

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
                        ProjectVM p = new ProjectVM();
                        p.ProjectID = reader.GetString(0);
                        p.UserID = reader.GetInt32(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        p.ProjectOwner = reader.GetString(5);
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

        public int CreateProject(Project project) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_project";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);

            cmd.Parameters["@ProjectID"].Value = project.ProjectID;
            cmd.Parameters["@UserID"].Value = project.UserID;
            cmd.Parameters["@Description"].Value = project.Description;

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

        public int LeaveProject(int userID, string projectID) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_user_leave_project";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);

            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;

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
