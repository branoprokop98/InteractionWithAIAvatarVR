using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using AIML.ContextWindowInput;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace AIML.ContextWindowInput
{
    public class LoadTopics
    {
        private XDocument xmlDocument;
        private readonly Topics topics;
        private readonly List<Topics> tempListOfTopics;
        public static List<List<Topics>> listOfTopics { get; set; }

        public LoadTopics()
        {
            loadXmlFile();
            topics = new Topics();
            tempListOfTopics = new List<Topics>();
            listOfTopics = new List<List<Topics>>();
            readTopic();
            initListOfTopics();
            //instantiateListOfTopics();
        }

        private void readTopic()
        {
            IEnumerable<XElement> topics = from topic in xmlDocument.Descendants("content") select topic;
            foreach (XElement element in topics)
            {
                IEnumerable<XElement> topicChildNodes = element.Elements();
                foreach (XElement nodes in topicChildNodes)
                {
                    switch (nodes.Name.ToString())
                    {
                        case "title":
                            this.topics.TopicName = nodes.Value;
                            break;
                        case "link":
                            this.topics.PathToTopic = nodes.Value;
                            break;
                        default:
                            throw new XmlException();
                    }
                }
                addTopicToList(this.topics);
            }
        }

        private void addTopicToList(Topics topic)
        {
            tempListOfTopics.Add(new Topics(){TopicName = topic.TopicName, PathToTopic = topic.PathToTopic});
        }

        private void loadXmlFile()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Topics.xml");
            this.xmlDocument = XDocument.Load(path);
            if (xmlDocument == null)
            {
                throw new FileLoadException();
            }
        }

        private void initListOfTopics()
        {
            int k = 0;
            double numOfItemsInLayer = Math.Ceiling(tempListOfTopics.Count / 9d);
            for (int i = 0; i < numOfItemsInLayer; i++)
            {
                listOfTopics.Add(new List<Topics>());
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        listOfTopics[i].Add(new Topics() {TopicName = tempListOfTopics[k].TopicName, PathToTopic = tempListOfTopics[k].PathToTopic});
                        k++;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
        }

        private void testLayerBounceForTopic()
        {
            if (ListOfTopics[ContextWindowService.actualLayerOfTopic][0] == null)
            {
                throw new Exception("Topic layer is out of range");
            }
        }

        public int tryLayerOfTopicBounce(int layerDirection)
        {
            try
            {
                testLayerBounceForTopic();
            }
            catch (Exception e)
            {
                if (layerDirection == 1)
                {
                    ContextWindowService.actualLayerOfTopic--;
                }
                else
                {
                    ContextWindowService.actualLayerOfTopic++;
                }
                Debug.Log(e.Message);
                return -1;
            }
            return 0;
        }
        public List<List<Topics>> ListOfTopics => listOfTopics;
    }
}
