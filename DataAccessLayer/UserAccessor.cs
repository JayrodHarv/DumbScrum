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

        public int CheckIfEmailHasBeenUsedAlready(string email) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_user_by_email";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

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
                    rows++;
                } else {
                    rows = 0;
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }

        public int InsertUser(User user) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_insert_user";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Pfp", SqlDbType.VarBinary);

            // set parameter values
            cmd.Parameters["@DisplayName"].Value = user.DisplayName;
            cmd.Parameters["@Email"].Value = user.Email;
            cmd.Parameters["@PasswordHash"].Value = user.Password;
            cmd.Parameters["@Pfp"].Value = user.Pfp;

            try {
                // open connection
                conn.Open();

                // execute command
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return rows;
        }

        public User SelectUserByUserID(int userID) {
            User user = new User();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_user_by_userid";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

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

                // process results
                if (reader.HasRows) {
                    if (reader.Read()) { // use while loop if multiple rows are returned
                        user.UserID = reader.GetInt32(0);
                        user.DisplayName = reader.GetString(1);
                        user.Email = reader.GetString(2);
                        user.Pfp = (byte[])reader[3];
                        user.Bio = reader.IsDBNull(4) ? "" : reader.GetString(3);
                        user.Active = reader.GetBoolean(5);
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

            return user;
        }

        public UserVM SelectUserVMByEmail(string email) {
            UserVM userVM = null;

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
                        userVM = new UserVM();
                        userVM.UserID = reader.GetInt32(0);
                        userVM.DisplayName = reader.GetString(1);
                        userVM.Email = reader.GetString(2);
                        userVM.Pfp = (byte[])reader[3];
                        userVM.Bio = reader.IsDBNull(4) ? "" : reader.GetString(3);
                        userVM.Active = reader.GetBoolean(5);
                        // null example
                        //employeeVM.EmployeeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return userVM;
        }

        public int UpdateDisplayName(int userID, string newDisplayName) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_DisplayName";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@DisplayName"].Value = newDisplayName;

            try {

                conn.Open();

                // an update is executed nonquery (returns an int)
                rows = cmd.ExecuteNonQuery();

                if (rows == 0) {
                    // treat failed update as exception
                    throw new ArgumentException("Update Failed");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }

        public int UpdatePasswordHash(string email, string newPasswordHash) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_PasswordHash";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try {

                conn.Open();

                // an update is executed nonquery (returns an int)
                rows = cmd.ExecuteNonQuery();

                if (rows == 0) {
                    // treat failed update as exception
                    throw new ArgumentException("Bad email or password");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }

        public int UpdateUser(User newUser, User oldUser) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_user";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters.Add("@NewDisplayName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewPfp", SqlDbType.VarBinary);

            // set parameter values
            cmd.Parameters["@UserID"].Value = oldUser.UserID;

            cmd.Parameters["@NewDisplayName"].Value = newUser.DisplayName;
            cmd.Parameters["@NewPfp"].Value = newUser.Pfp;

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

        public List<string> SelectAllRoles() {
            List<string> result = new List<string>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_get_all_roles";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    while (reader.Read()) {
                        result.Add(reader.GetString(0));
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return result;
        }

        public int DeleteUserRole(int userID, string roleID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_delete_user_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters["@RoleID"].Value = roleID;

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

        public int InsertUserRole(int userID, string roleID) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_add_user_role";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters["@RoleID"].Value = roleID;

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
