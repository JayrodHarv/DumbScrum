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
    public class UserStoryAccessor : IUserStoryAccessor {
        public List<UserStory> SelectUserStoriesByFeatureID(int featureID) {
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
            cmd.Parameters.Add("@FeatureID", SqlDbType.Int);

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
                        s.StoryID = reader.GetInt32(0);
                        s.FeatureID = reader.GetInt32(1);
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
