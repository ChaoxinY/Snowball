using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public static class UIToolMethods
{
    public static void OpenUIPanel(Transform canvasTransform, string panelName)
    {
        GameObject panelToOpen = null;
        Transform panel = canvasTransform.Find(panelName);

        if (panel != null)
        {
            panelToOpen = panel.gameObject;
            panelToOpen.SetActive(true);
        }
        else
        {
            AddUIPanel(canvasTransform, panelName, panelName);
        }
    }

    public static void DisableGameObject(string name)
    {
        GameObject.Find(name).SetActive(false);
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //Would be legacy
    //public static void AddPanelNavigateButtons(GameObject panel, List<string> panelNames)
    //{
    //    for (int i = 0; i < panelNames.Count; i++)
    //    {
    //        GameObject panelButton = AddUIButton(panel.transform, panelNames[i]);
    //        //Not using int i because when put into the delegate it register as a reference instead of a value. Thus causing augment out of range error.
    //        string panelName = panelNames[i];
    //        panelButton.GetComponent<Button>().onClick.AddListener(delegate { OpenUIPanel(panel.transform.root, panelName); });
    //    }
    //}

    //public static void AddExitGameButton(GameObject panel)
    //{
    //    GameObject panelButton = AddUIButton(panel.transform, "Exit");
    //    panelButton.GetComponent<Button>().onClick.AddListener(delegate { ExitGame(); });
    //}

    public static GameObject AddUIButton(Transform parentPanel, string name, string buttonPrefabName = "PanelButton")
    {
        GameObject button = GameObject.Instantiate(Resources.Load("Prefabs/UIElements/" + buttonPrefabName) as GameObject);
        button.transform.SetParent(parentPanel.transform);
        button.name = name;
        button.GetComponentInChildren<Text>().text = name;
        return button;
    }
    public static GameObject AddUIPanel(Transform canvasTranform, string name, string panelPrefabPath = "DefaultPanel")
    {
        GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/UIElements/" + panelPrefabPath) as GameObject, canvasTranform.transform,false);
        //panel.transform.SetParent(parentPanel.transform);
        panel.name = name;   
        return panel;
    }
}
