using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class FeedMessage {
        public int MessageID { get; set; }
        public int SprintID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class FeedMessageVM : FeedMessage {
        public string UserDisplayName { get; set; }
    }
}
