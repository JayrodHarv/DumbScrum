using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IFeatureManager {
        Feature GetFeatureByFeatureID(int featureID);
        List<Feature> GetFeaturesByProjectID(string projectID);
        bool AddProjectFeature(string projectID, string name, string description, string priority);
    }
}
