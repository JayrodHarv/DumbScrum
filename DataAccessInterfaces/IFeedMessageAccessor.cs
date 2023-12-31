﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IFeedMessageAccessor {
        int InsertFeedMessage(FeedMessage message);
        List<FeedMessageVM> SelectSprintFeedMessages(int sprintID);
    }
}
