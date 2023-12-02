using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class FeedMessageManager : IFeedMessageManager {
        private IFeedMessageAccessor _feedMessageAccessor;

        public FeedMessageManager() {
            _feedMessageAccessor = new FeedMessageAccessor();
        }

        public FeedMessageManager(IFeedMessageAccessor feedMessageAccessor) {
            _feedMessageAccessor = feedMessageAccessor;
        }

        public bool CreateFeedMessage(FeedMessage message) {
            bool result = false;
            try {
                result = (1 == _feedMessageAccessor.InsertFeedMessage(message));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<FeedMessageVM> GetSprintFeedMessages(int sprintID) {
            List<FeedMessageVM> result = new List<FeedMessageVM>();
            try {
                result = _feedMessageAccessor.SelectSprintFeedMessages(sprintID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
