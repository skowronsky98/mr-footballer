using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField]
    private int scene;

  

    // Updates once per frame
    void Start()
    {
        scene = LevelSelection.index + 3;
        StartCoroutine(LoadNewScene());
    }
    

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(1);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);                                                          //ONLY FOR TESTS!!!!!!!!!!!!!--------     

        AdManager.AdManagerInstance.LoadRewardBasedAd();

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone || !AdManager.AdManagerInstance.RewardBaseVideoAd.IsLoaded())
        {
            yield return null;
        }

    }

}