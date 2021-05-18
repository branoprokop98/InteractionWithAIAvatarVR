using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIML.ContextWindowInput
{
    public class ContextWindowTopic : ContextLayer
    {
        private readonly Canvas canvas;
        private readonly LoadTopics topics;


        public  ContextWindowTopic(Canvas canvas)
        {
            this.canvas = canvas;
            topics = new LoadTopics();
            initTopicsName();
        }

        public void initTopicsName()
        {
            ContextWindowService.actualLayerOfTopic = 0;
            ContextWindowService.actualLayerOfSentences = -2;
            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
                    button.gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        public void getNextLayer()
        {
            ContextWindowService.actualLayerOfTopic++;
            if (topics.tryLayerOfTopicBounce(1) == -1)
            {
                return;
            };

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
                    button.gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        public void getPrevLayer()
        {
            ContextWindowService.actualLayerOfTopic--;
            if (topics.tryLayerOfTopicBounce(0) == -1)
            {
                return;
            };

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = this.topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
                    button.gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
    }
}
