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
    public class UserAccessor : IUserAccessor {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_authenticate_user";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try {
                // open connection
                conn.Open();

                // execute command
                rows = Convert.ToInt32(cmd.ExecuteScalar());
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return rows;
        }

        public bool InsertUser(string email, string passwordHash) {
            throw new NotImplementedException();
        }

        public List<UserVM> SelectMembersByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            throw new NotImplementedException();
        }

        public UserVM SelectUserVMByEmail(string email) {
            UserVM userVM = new UserVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_user_by_email";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    if (reader.Read()) { // use while loop if multiple rows are returned
                        userVM.UserID = reader.GetInt32(0);
                        userVM.DisplayName = reader.GetString(1);
                        userVM.Email = reader.GetString(2);
                        userVM.Bio = reader.GetString(3);
                        userVM.Active = reader.GetBoolean(4);
                        // null example
                        //employeeVM.EmployeeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    }
                } else {
                    throw new ArgumentException("User not found");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return userVM;
        }

        public int UpdateDisplayName(int userID, string newDisplayName) {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash) {
            throw new NotImplementedException();
        }
    }
}
