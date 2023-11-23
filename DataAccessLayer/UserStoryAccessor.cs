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
    public class UserStoryAccessor : IUserStoryAccessor {
        public int CreateFeatureUserStory(UserStory story) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_feature_userstory";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StoryID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FeatureID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Person", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 255);

            cmd.Parameters["@StoryID"].Value = story.StoryID;
            cmd.Parameters["@FeatureID"].Value = story.FeatureID;
            cmd.Parameters["@Person"].Value = story.Person;
            cmd.Parameters["@Action"].Value = story.Action;
            cmd.Parameters["@Reason"].Value = story.Reason;

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

        public List<UserStory> SelectUserStoriesByFeatureID(string featureID) {
            List<UserStory> result = new List<UserStory>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_feature_user_stories";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@FeatureID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@FeatureID"].Value = featureID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        UserStory s = new UserStory();
                        s.StoryID = reader.GetString(0);
                        s.FeatureID = reader.GetString(1);
                        s.Person = reader.GetString(2);
                        s.Action = reader.GetString(3);
                        s.Reason = reader.GetString(4);

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
    }
}
