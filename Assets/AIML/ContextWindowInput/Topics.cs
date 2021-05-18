using System.Collections.Generic;

namespace AIML.ContextWindowInput
{
    public class Topics
    {
        private string topicName;
        private string pathToTopic;
        private List<Topics> topic;

        public Topics()
        {
        }

        public Topics(string topicName, string pathToTopic)
        {
            this.topicName = topicName;
            this.pathToTopic = pathToTopic;
        }

        public Topics(List<Topics> topic)
        {
            this.topic = topic;
        }

        public List<Topics> Topic
        {
            get => topic;
            set => topic = value;
        }

        public string TopicName
        {
            get => topicName;
            set => topicName = value;
        }

        public string PathToTopic
        {
            get => pathToTopic;
            set => pathToTopic = value;
        }
    }
}
