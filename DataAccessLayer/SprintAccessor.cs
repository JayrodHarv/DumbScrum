﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class SprintAccessor : ISprintAccessor {
        public List<Sprint> SelectSprintsByProjectID(string projectID) {
            List<Sprint> result = new List<Sprint>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_project_sprints";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

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
                if (reader.HasRows) {
                    while (reader.Read()) {
                        Sprint s = new Sprint();
                        s.SprintID = reader.GetInt32(0);
                        s.FeatureID = reader.GetInt32(1);
                        s.StartDate = reader.GetDateTime(2);
                        s.EndDate = reader.GetDateTime(3);
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

        public SprintVM SelectSprintVMBySprintID(int sprintID) {
            throw new NotImplementedException();
        }
    }
}