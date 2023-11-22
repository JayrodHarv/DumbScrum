using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicLayer {
    public class UserStoryManager : IUserStoryManager {
        private IUserStoryAccessor _userStoryAccessor = null;

        public UserStoryManager() {
            _userStoryAccessor = new UserStoryAccessor();
        }

        public UserStoryManager(IUserStoryAccessor userStoryAccessor) {
            _userStoryAccessor = userStoryAccessor;
        }

        public bool AddFeatureUserStory(UserStory story) {
            bool result = false;

            try {
                if (1 == _userStoryAccessor.CreateFeatureUserStory(story)) {
                    result = true;
                }
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<UserStory> GetFeatureUserStories(string featureID) {
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
