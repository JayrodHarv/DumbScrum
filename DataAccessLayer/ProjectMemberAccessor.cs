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
    public class ProjectMemberAccessor : IProjectMemberAccessor {
        public int InsertProjectMember(int userID, string projectID, string projectRoleID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_insert_project_member";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@ProjectRoleID"].Value = projectRoleID;

            try {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }

        public int MemberLeaveProject(int userID, string projectID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_member_leave_project";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;

            try {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }

        public ProjectMemberVM SelectProjectMember(int userID, string projectID) {
            ProjectMemberVM result = new ProjectMemberVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_project_member";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    if (reader.Read()) {
                        result.User = new User() {
                            UserID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            DisplayName = reader.GetString(2),
                            Pfp = (byte[])reader[3]
                        };

                        result.Active = reader.GetBoolean(4);

                        result.ProjectRole = new ProjectRole() {
                            ProjectRoleID = reader.GetString(5),
                            FeaturePrivileges = reader.GetBoolean(6),
                            UserStoryPrivileges = reader.GetBoolean(7),
                            SprintPlanningPrivileges = reader.GetBoolean(8),
                            FeedMessagingPrivileges = reader.GetBoolean(9),
                            TaskPrivileges = reader.GetBoolean(10),
                            TaskReviewingPrivileges = reader.GetBoolean(11),
                            ProjectManagementPrivileges = reader.GetBoolean(12)
                        };
                    }
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return result;
        }

        public List<ProjectMemberListVM> SelectProjectMembers(string projectID) {
            List<ProjectMemberListVM> result = new List<ProjectMemberListVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_project_members";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

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

                // process results
                if (reader.HasRows) {
                    while (reader.Read()) {
                        ProjectMemberListVM member = new ProjectMemberListVM();
                        member.UserID = reader.GetInt32(0);
                        member.Email = reader.GetString(1);
                        member.DisplayName = reader.GetString(2);
                        member.Pfp = (byte[])reader[3];
                        member.ProjectRoleID = reader.GetString(4);
                        member.Active = reader.GetBoolean(5);
                        result.Add(member);
                    }
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return result;
        }

        public int UpdateMemberRole(int userID, string projectID, string projectRoleID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_member_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@ProjectRoleID"].Value = projectRoleID;

            try {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }
    }
}
