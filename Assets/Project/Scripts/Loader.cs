using System.Collections;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DragonWorm {

    public class Loader : MonoBehaviour {
        static SceneReference loadingScene = new (SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/Project/Scenes/Loading.unity"]);
        static SceneReference targetScene;
        static Loader runner;

        public static void Load(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single) {
            targetScene = scene;

            if (runner == null) {
                GameObject obj = new GameObject("Loader");
                runner = obj.AddComponent<Loader>();
                DontDestroyOnLoad(obj);
            }

            if (mode == LoadSceneMode.Single) {
                SceneManager.LoadScene(loadingScene.Name);
                runner.StartCoroutine(LoadTargetScene());
            } else {
                SceneManager.LoadScene(scene.Name, mode);
            }
        }

        static IEnumerator LoadTargetScene() {
            yield return null; // wait one frame
            SceneManager.LoadScene(targetScene.Name);
        }
    }
}
