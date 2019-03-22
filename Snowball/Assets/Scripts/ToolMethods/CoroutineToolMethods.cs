using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CoroutineToolMethods : MonoBehaviour
{
    public  IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Called "+ sceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
