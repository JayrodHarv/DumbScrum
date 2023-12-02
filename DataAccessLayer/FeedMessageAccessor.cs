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
    public class FeedMessageAccessor : IFeedMessageAccessor {
        public int InsertFeedMessage(FeedMessage message) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_feed_message";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SprintID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@Text", SqlDbType.Text);
            cmd.Parameters.Add("@SentAt", SqlDbType.DateTime);

            cmd.Parameters["@SprintID"].Value = message.SprintID;
            cmd.Parameters["@UserID"].Value = message.UserID;
            cmd.Parameters["@Text"].Value = message.Text;
            cmd.Parameters["@SentAt"].Value = message.SentAt;

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

        public List<FeedMessageVM> SelectSprintFeedMessages(int sprintID) {
            List<FeedMessageVM> result = new List<FeedMessageVM>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_sprint_feed_messages";

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
                        FeedMessageVM m = new FeedMessageVM();
                        m.MessageID = reader.GetInt32(0);
                        m.SprintID = reader.GetInt32(1);
                        m.UserID = reader.GetInt32(2);
                        m.Text = reader.GetString(3);
                        m.SentAt = reader.GetDateTime(4);
                        m.UserDisplayName = reader.GetString(5);
                        result.Add(m);
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
