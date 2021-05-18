using System.Collections;
using System.IO;
using System.Xml.Serialization;
using Menu.NewGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public void loadGame()
        {
            //AsyncOperation operation = SceneManager.UnloadSceneAsync("Menu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            //Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(1);
            SceneManager.LoadScene((int) 1);
            //StartCoroutine(Wait());
        }
        //
        // IEnumerator Wait()
        // {
        //     AsyncOperation operation = SceneManager.LoadSceneAsync("Interaction");
        //     SceneManager.UnloadSceneAsync("Menu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        //
        //     while (operation.isDone == false)
        //     {
        //
        //         Debug.Log(operation.progress);
        //         yield return null;
        //     }
        // }

        public GameObject UIRootObject;
        private AsyncOperation sceneAsync;

        // public void loadGame()
        // {
        //     Scene currentScene = SceneManager.GetActiveScene();
        //     StartCoroutine(LoadYourAsyncScene(currentScene));
        // }

        IEnumerator LoadYourAsyncScene(Scene current)
        {
            // Set the current Scene to be able to unload it later
            //Scene currentScene = SceneManager.GetActiveScene();

            // The Application loads the Scene in the background at the same time as the current Scene.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Interaction", LoadSceneMode.Additive);

            // Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
            SceneManager.MoveGameObjectToScene(UIRootObject, SceneManager.GetSceneByName("Interaction"));
            // Unload the previous Scene
            SceneManager.UnloadSceneAsync(current);
        }

        void enableScene(int index)
        {
            //Activate the Scene
            sceneAsync.allowSceneActivation = true;


            Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
            if (sceneToLoad.IsValid())
            {
                Debug.Log("Scene is Valid");
                SceneManager.MoveGameObjectToScene(UIRootObject, sceneToLoad);
                SceneManager.SetActiveScene(sceneToLoad);
            }
        }

        void OnFinishedLoadingAllScene()
        {
            Debug.Log("Done Loading Scene");
            enableScene(1);
            Debug.Log("Scene Activated!");
        }
    }
}
