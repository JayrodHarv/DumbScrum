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
    public class ProjectAccessor : IProjectAccessor {
        public List<Project> SelectAllProjects() {
            List<Project> result = new List<Project>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_all_projects";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();
                if(reader.HasRows) {
                    while(reader.Read()) {
                        Project p = new Project();
                        p.ProjectID = reader.GetString(0);
                        p.ProjectOwner = reader.GetString(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        result.Add(p);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return result;
        }

        public Project SelectProjectByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            List<Project> result = new List<Project>();

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_user_projects";

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
                        Project p = new Project();
                        p.ProjectID = reader.GetString(0);
                        p.ProjectOwner = reader.GetString(1);
                        p.DateCreated = reader.GetDateTime(2);
                        p.Status = reader.GetString(3);
                        p.Description = reader.GetString(4);
                        result.Add(p);
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