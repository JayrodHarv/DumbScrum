using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IUserStoryAccessor {
        List<UserStory> SelectUserStoriesByFeatureID(string featureID);
        int CreateFeatureUserStory(UserStory story);
    }
}
