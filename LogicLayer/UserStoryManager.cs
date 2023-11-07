using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class UserStoryManager : IUserStoryManager {
        private IUserStoryAccessor _userStoryAccessor = null;

        public UserStoryManager() {
            _userStoryAccessor = new UserStoryAccessor();
        }

        public UserStoryManager(IUserStoryAccessor userStoryAccessor) {
            _userStoryAccessor = userStoryAccessor;
        }
        public List<UserStory> GetFeatureUserStories(int featureID) {
            List<UserStory> result = new List<UserStory>();
            try {
                result = _userStoryAccessor.SelectUserStoriesByFeatureID(featureID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
