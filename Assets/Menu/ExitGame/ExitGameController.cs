using UnityEngine;

namespace Menu.ExitGame
{
    public class ExitGameController
    {
        public void exitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
