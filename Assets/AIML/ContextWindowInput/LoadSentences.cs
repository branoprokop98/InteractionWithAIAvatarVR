using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using AIML.ContextWindowInput;
using AIMLbot;

namespace AIML.ContextWindowInput
{
    public class LoadSentences
    {
        private readonly Bot bot;
        private XDocument aimlFile;
        private readonly AIMLStructure aimlStructure;
        private readonly List<AIMLStructure> sentences;
        private List<List<AIMLStructure>> listOfAimlSentences;


        public LoadSentences()
        {
            sentences = new List<AIMLStructure>();
            listOfAimlSentences = new List<List<AIMLStructure>>();
            aimlStructure = new AIMLStructure();
            bot = new Bot();
        }

        private void loadXmlDocument(string nameOfFile)
        {
            string path = bot.PathToAIML + "\\" + "aiml";
            string[] files = Directory.GetFiles(path, nameOfFile);
            aimlFile = XDocument.Load(files[0]);
        }

        public void listSentences(string nameOfFile)
        {
            sentences.Clear();
            loadXmlDocument(nameOfFile);
            IEnumerable<XElement> aimlNodes = from aiml in aimlFile.Descendants("category") select aiml;
            foreach (XElement aimlNode in aimlNodes)
            {
                IEnumerable<XElement> childNodes = aimlNode.Elements();
                foreach (XElement childNode in childNodes)
                {
                    switch (childNode.Name.ToString())
                    {
                        case "pattern":
                            string sentence = childNode.Value.ToLower();
                            aimlStructure.Pattern = sentence; //char.ToUpper(sentence[0]) + sentence.Remove(0, 1);
                            break;
                        default:
                            continue;
                    }
                }

                addSentenceToList();
            }

            addTo2DList();
        }

        private void addSentenceToList()
        {
            if (!aimlStructure.Pattern.Contains("*") && !aimlStructure.Pattern.Contains("_"))
            {
                sentences.Add(new AIMLStructure() {Pattern = aimlStructure.Pattern});
            }
        }

        public void addTo2DList()
        {
            int k = 0;
            listOfAimlSentences.Clear();
            double numOfItemsInLayer = Math.Ceiling(sentences.Count / 10d);
            for (int i = 0; i < numOfItemsInLayer; i++)
            {
                listOfAimlSentences.Add(new List<AIMLStructure>());
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        listOfAimlSentences[i].Add(new AIMLStructure() {Pattern = sentences[k].Pattern});
                        k++;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
        }

        private void testLayerBounceForSentences(List<List<AIMLStructure>> listOfSentences)
        {
            if (listOfSentences[ContextWindowService.actualLayerOfSentences][0] == null)
            {
                throw new Exception("Out of range");
            }
        }

        public int tryLayerOfSentencesBounce(int layerDirection, List<List<AIMLStructure>> listOfSentences)
        {
            try
            {
                testLayerBounceForSentences(listOfSentences);
            }
            catch (Exception e)
            {
                if (layerDirection == 0)
                {
                    ContextWindowService.actualLayerOfSentences++;
                }
                else
                {
                    ContextWindowService.actualLayerOfSentences--;
                }

                return -1;
            }

            return 0;
        }

        public List<List<AIMLStructure>> ListOfAimlSentences
        {
            get => listOfAimlSentences;
            set => listOfAimlSentences = value;
        }
    }
}
