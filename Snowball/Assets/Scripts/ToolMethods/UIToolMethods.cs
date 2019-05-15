using UnityEngine;
using UnityEngine.UI;

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
