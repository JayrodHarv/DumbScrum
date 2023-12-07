using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class FeatureManager : IFeatureManager {

        private IFeatureAccessor _featureAccessor = null;

        public FeatureManager() {
            _featureAccessor = new FeatureAccessor();
        }

        public FeatureManager(IFeatureAccessor featureAccessor) {
            _featureAccessor = featureAccessor;
        }

        public bool AddProjectFeature(Feature feature) {
            bool result = false;

            try {
                if(1 == _featureAccessor.CreateProjectFeature(feature)) {
                    result = true;
                }    
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool EditFeatureStatus(string featureID, string status) {
            bool result = false;
            try {
                result = (1 == _featureAccessor.UpdateFeatureStatus(featureID, status));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public FeatureVM GetFeatureByFeatureID(int featureID) {
            throw new NotImplementedException();
        }

        public List<FeatureVM> GetFeaturesByProjectID(string projectID) {
            List<FeatureVM> result = new List<FeatureVM>();
            try {
                result = _featureAccessor.SelectFeaturesByProjectID(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
