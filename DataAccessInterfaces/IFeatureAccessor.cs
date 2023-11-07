using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IFeatureAccessor {
        Feature SelectFeatureByFeatureID(int featureID);
        List<Feature> SelectFeaturesByProjectID(string projectID);
        int CreateProjectFeature(string projectID, string name, string description, string priority, string status);
    }
}
