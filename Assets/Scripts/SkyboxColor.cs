using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class SkyboxColor : MonoBehaviour
    {
        [SerializeField] private Color color1;

        [SerializeField] private Color color2;

        public float duration = 1.0F;
        private void Update()
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            //RenderSettings.skybox.SetColor("_Tint", Color.Lerp(Color.black, Color.white, lerp));
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(color1, color2, lerp));
        }
    }
}
