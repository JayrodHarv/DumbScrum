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
    public class ProjectRoleAccessor : IProjectRoleAccessor {
        public int DeleteProjectRole(int projectRoleID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_delete_project_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.Int);

            // set parameter values
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

        public int InsertProjectRole(ProjectRole projectRole) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_insert_project_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FeaturePrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@UserStoryPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@SprintPlanningPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@FeedMessagingPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@TaskPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@TaskReviewingPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@ProjectManagementPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);

            // set parameter values
            cmd.Parameters["@RoleName"].Value = projectRole.RoleName;
            cmd.Parameters["@ProjectID"].Value = projectRole.ProjectID;
            cmd.Parameters["@FeaturePrivileges"].Value = projectRole.FeaturePrivileges;
            cmd.Parameters["@UserStoryPrivileges"].Value = projectRole.UserStoryPrivileges;
            cmd.Parameters["@SprintPlanningPrivileges"].Value = projectRole.SprintPlanningPrivileges;
            cmd.Parameters["@FeedMessagingPrivileges"].Value = projectRole.FeedMessagingPrivileges;
            cmd.Parameters["@TaskPrivileges"].Value = projectRole.TaskPrivileges;
            cmd.Parameters["@TaskReviewingPrivileges"].Value = projectRole.TaskReviewingPrivileges;
            cmd.Parameters["@ProjectManagementPrivileges"].Value = projectRole.ProjectManagementPrivileges;
            cmd.Parameters["@Description"].Value = projectRole.Description;

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

        public ProjectRoleVM SelectProjectRole(int projectRoleID) {
            ProjectRoleVM result = new ProjectRoleVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_project_role";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@ProjectRoleID"].Value = projectRoleID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    if (reader.Read()) {
                        result.ProjectRoleID = reader.GetInt32(0);
                        result.RoleName = reader.GetString(1);
                        result.FeaturePrivileges = reader.GetBoolean(2);
                        result.UserStoryPrivileges = reader.GetBoolean(3);
                        result.SprintPlanningPrivileges = reader.GetBoolean(4);
                        result.FeedMessagingPrivileges = reader.GetBoolean(5);
                        result.TaskPrivileges = reader.GetBoolean(6);
                        result.TaskReviewingPrivileges = reader.GetBoolean(7);
                        result.ProjectManagementPrivileges = reader.GetBoolean(8);
                        result.Description = reader.GetString(9);
                    }
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return result;
        }

        public List<ProjectRoleListVM> SelectProjectRoles(string projectID) {
            List<ProjectRoleListVM> result = new List<ProjectRoleListVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_project_roles";

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
                        ProjectRoleListVM role = new ProjectRoleListVM();
                        role.ProjectRoleID = reader.GetInt32(0);
                        role.RoleName = reader.GetString(1);
                        role.MembersWithRole = reader.GetInt32(2);
                        role.Description = reader.GetString(3);
                        result.Add(role);
                    }
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return result;
        }

        public int UpdateProjectRole(ProjectRole projectRole) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_project_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.Int);
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FeaturePrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@UserStoryPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@SprintPlanningPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@FeedMessagingPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@TaskPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@TaskReviewingPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@ProjectManagementPrivileges", SqlDbType.Bit);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);

            // set parameter values
            cmd.Parameters["@ProjectRoleID"].Value = projectRole.ProjectRoleID;
            cmd.Parameters["@RoleName"].Value = projectRole.RoleName;
            cmd.Parameters["@ProjectID"].Value = projectRole.ProjectID;
            cmd.Parameters["@FeaturePrivileges"].Value = projectRole.FeaturePrivileges;
            cmd.Parameters["@UserStoryPrivileges"].Value = projectRole.UserStoryPrivileges;
            cmd.Parameters["@SprintPlanningPrivileges"].Value = projectRole.SprintPlanningPrivileges;
            cmd.Parameters["@FeedMessagingPrivileges"].Value = projectRole.FeedMessagingPrivileges;
            cmd.Parameters["@TaskPrivileges"].Value = projectRole.TaskPrivileges;
            cmd.Parameters["@TaskReviewingPrivileges"].Value = projectRole.TaskReviewingPrivileges;
            cmd.Parameters["@ProjectManagementPrivileges"].Value = projectRole.ProjectManagementPrivileges;
            cmd.Parameters["@Description"].Value = projectRole.Description;

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
