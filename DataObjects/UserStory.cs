namespace DataObjects {
    public class UserStory {
        public string StoryID { get; set; }
        public string FeatureID { get; set; }
        public string Person { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
    }

    public class UserStoryVM : UserStory {
        public string FeatureName { get; set; }
    }
}
