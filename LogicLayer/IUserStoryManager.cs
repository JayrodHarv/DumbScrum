using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IUserStoryManager {
        List<UserStory> GetFeatureUserStories(int featureID);
        bool AddFeatureUserStory(int featureID, string person, string action, string reason);
    }
}
