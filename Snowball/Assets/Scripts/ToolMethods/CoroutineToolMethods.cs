using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CoroutineToolMethods : MonoBehaviour
{
    public  void AddSceneLoadingButtons(GameObject panel, List<string> sceneNames)
    {
        for (int i = 0; i < sceneNames.Count; i++)
        {            
            string sceneName = sceneNames[i];
            GameObject panelButton = UIToolMethods.AddDefaultPanelButton(panel.transform, sceneName);
            panelButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(LoadScene(sceneName)); });
        }
    }

    public  IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
