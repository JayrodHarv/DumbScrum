using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IFeatureAccessor {
        FeatureVM SelectFeatureByFeatureID(string featureID);
        List<FeatureVM> SelectFeaturesByProjectID(string projectID);
        int CreateProjectFeature(Feature feature);
        int UpdateFeatureStatus(string featureID, string status);
    }
}
