using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Representing different menu panels 
public class SceneNavigationPanel : MonoBehaviour {

    public GameObject panel;
    public List<string> sceneNames = new List<string>();
    private CoroutineToolMethods coroutineToolMethods;

    private void Start()
    {
        gameObject.AddComponent<CoroutineToolMethods>();
        coroutineToolMethods = gameObject.GetComponent<CoroutineToolMethods>();
        coroutineToolMethods.AddSceneLoadingButtons(panel, sceneNames);
    }
}
