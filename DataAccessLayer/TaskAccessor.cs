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
    public class TaskAccessor : ITaskAccessor {
        public int InsertTask(int sprintID, int storyID, string status) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_task";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SprintID", SqlDbType.Int);
            cmd.Parameters.Add("@StoryID", SqlDbType.Int);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);

            cmd.Parameters["@SprintID"].Value = sprintID;
            cmd.Parameters["@StoryID"].Value = storyID;
            cmd.Parameters["@Status"].Value = status;

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

        public List<TaskVM> SelectTaskVMsBySprintID(int sprintID) {
            List<TaskVM> result = new List<TaskVM>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_sprint_tasks";

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
                    while (reader.Read()) {
                        TaskVM t = new TaskVM();
                        t.TaskID = reader.GetInt32(0);
                        t.SprintID = reader.GetInt32(1);
                        t.StoryID = reader.GetInt32(2);
                        t.UserID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                        t.Status = reader.GetString(4);
                        result.Add(t);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public List<TaskVM> SelectTaskVMsByUserID(int userID) {
            List<TaskVM> result = new List<TaskVM>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_sprint_tasks";

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
                        TaskVM t = new TaskVM();
                        t.TaskID = reader.GetInt32(0);
                        t.SprintID = reader.GetInt32(1);
                        t.StoryID = reader.GetInt32(2);
                        t.UserID = reader.GetInt32(3);
                        t.Status = reader.GetString(4);
                        result.Add(t);
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
