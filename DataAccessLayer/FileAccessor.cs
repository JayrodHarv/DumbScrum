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
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_delete_file";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FileID", SqlDbType.Int);

            cmd.Parameters["@FileID"].Value = fileID;

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

        public int InsertTaskFile(File file) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_task_file";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Data", SqlDbType.VarBinary);
            cmd.Parameters.Add("@Extension", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastEdited", SqlDbType.DateTime);

            cmd.Parameters["@Data"].Value = file.Data;
            cmd.Parameters["@Extension"].Value = file.Extension;
            cmd.Parameters["@TaskID"].Value = file.TaskID;
            cmd.Parameters["@FileName"].Value = file.FileName;
            cmd.Parameters["@Type"].Value = file.Type;
            cmd.Parameters["@LastEdited"].Value = file.LastEdited;

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

        public int InsertTemplateFile(File file) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_insert_template_file";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Data", SqlDbType.VarBinary);
            cmd.Parameters.Add("@Extension", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastEdited", SqlDbType.DateTime);

            cmd.Parameters["@Data"].Value = file.Data;
            cmd.Parameters["@Extension"].Value = file.Extension;
            cmd.Parameters["@ProjectID"].Value = file.ProjectID;
            cmd.Parameters["@FileName"].Value = file.FileName;
            cmd.Parameters["@Type"].Value = file.Type;
            cmd.Parameters["@LastEdited"].Value = file.LastEdited;

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

        public File SelectProjectTemplateFileByType(string projectID, string type) {
            File result = null;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_get_project_template_file_by_type";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@Type"].Value = type;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    if(reader.Read()) {
                        result = new File() {
                            FileID = reader.GetInt32(0),
                            Data = (byte[])reader[1],
                            Extension = reader.GetString(2),
                            ProjectID = reader.GetString(3),
                            FileName = reader.GetString(4),
                            Type = reader.GetString(5),
                            LastEdited = reader.GetDateTime(6)
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
                        f.LastEdited = reader.GetDateTime(6);
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

        public int UpdateFile(File oldFile, File newFile) {
            int result = 0;

            var conn = SqlConnectionProvider.GetConnection();
            var cmdText = "sp_update_file";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FileID", SqlDbType.Int);

            cmd.Parameters.Add("@OldData", SqlDbType.VarBinary);
            cmd.Parameters.Add("@OldFileName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldLastEdited", SqlDbType.DateTime);

            cmd.Parameters.Add("@NewData", SqlDbType.VarBinary);
            cmd.Parameters.Add("@NewFileName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewLastEdited", SqlDbType.DateTime);

            cmd.Parameters["@FileID"].Value = oldFile.FileID;

            cmd.Parameters["@OldData"].Value = oldFile.Data;
            cmd.Parameters["@OldFileName"].Value = oldFile.FileName;
            cmd.Parameters["@OldLastEdited"].Value = oldFile.LastEdited;

            cmd.Parameters["@NewData"].Value = newFile.Data;
            cmd.Parameters["@NewFileName"].Value = newFile.FileName;
            cmd.Parameters["@NewLastEdited"].Value = newFile.LastEdited;

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

        public File SelectTaskFile(int fileID) {
            File result = null;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_get_task_file";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@FileID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@FileID"].Value = fileID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    if (reader.Read()) {
                        result = new File() {
                            FileID = reader.GetInt32(0),
                            Data = (byte[])reader[1],
                            Extension = reader.GetString(2),
                            FileName = reader.GetString(3),
                            Type = reader.GetString(4),
                            LastEdited = reader.GetDateTime(5)
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
    }
}
