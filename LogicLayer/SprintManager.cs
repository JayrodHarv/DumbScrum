﻿using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class SprintManager : ISprintManager {
        private ISprintAccessor _sprintAccessor = null;

        public SprintManager() {
            _sprintAccessor = new SprintAccessor();
        }

        public SprintManager(ISprintAccessor sprintAccessor) {
            _sprintAccessor = sprintAccessor;
        }

        public List<Sprint> GetSprintsByProjectID(string projectID) {
            List<Sprint> result = new List<Sprint>();
            try {
                result = _sprintAccessor.SelectSprintsByProjectID(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public SprintVM GetSprintVMBySprintID(int sprintID) {
            throw new NotImplementedException();
        }
    }
}
