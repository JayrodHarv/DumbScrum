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
    public class FileAccessor : IFileAccessor {
        public int DeleteFile(int fileID) {
            throw new NotImplementedException();
        }

        public int InsertFile(File file) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_file";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Data", SqlDbType.VarBinary);
            cmd.Parameters.Add("@Extension", SqlDbType.Char, 4);
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);

            cmd.Parameters["@Data"].Value = file.Data;
            cmd.Parameters["@Extension"].Value = file.Extension;
            cmd.Parameters["@TaskID"].Value = file.TaskID;
            cmd.Parameters["@FileName"].Value = file.FileName;
            cmd.Parameters["@Type"].Value = file.Type;

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

        public List<File> SelectTaskFilesByType(int taskID, string type) {
            List<File> result = new List<File>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_task_files_by_type";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@TaskID"].Value = taskID;
            cmd.Parameters["@Type"].Value = type;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        File f = new File();
                        f.FileID = reader.GetInt32(0);
                        f.Data = (byte[])reader[1];
                        f.Extension = reader.GetString(2);
                        f.TaskID = reader.GetInt32(3);
                        f.FileName = reader.GetString(4);
                        f.Type = reader.GetString(5);

                        result.Add(f);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public int UpdateFile(File file) {
            throw new NotImplementedException();
        }
    }
}
