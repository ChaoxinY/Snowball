using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public static class UIToolMethods 
{
    public static void OpenUIPanel(Transform canvasTransform,string panelName)
    {
        GameObject panelToOpen = null;
        Transform canvas = canvasTransform.Find(panelName);
      
        if (canvas != null)
        {
            panelToOpen = canvasTransform.Find(panelName).gameObject;
            panelToOpen.SetActive(true);
        }
        else
        {  
            //Preload and set the gameobject to its actual name instead of ...+ Clone.
            //This also helps to prevent opening the same tabs.
            GameObject panelToLoad = Resources.Load("Prefabs/UIElements/" + panelName) as GameObject;
            string panelToLoadName = panelToLoad.name;
            panelToOpen= GameObject.Instantiate(Resources.Load("Prefabs/UIElements/" + panelName) as GameObject);
            panelToOpen.name = panelToLoadName;
            panelToOpen.transform.SetParent(canvasTransform,false);
        }   
    }

    public static void ClosePanel(GameObject panel) {
        panel.SetActive(false);
    }

    public static void AddPanelNavigateButtons(GameObject panel, List<string> panelNames)
    {
        for (int i = 0; i < panelNames.Count; i++)
        {
            GameObject panelButton = AddDefaultPanelButton(panel.transform, panelNames[i]);
            //Not using int i because when put into the delegate it register as a reference instead of a value. Thus causing augment out of range error.
            string panelName = panelNames[i];
            panelButton.GetComponent<Button>().onClick.AddListener(delegate { OpenUIPanel(panel.transform.root, panelName); });
        }
    }

    public static GameObject AddDefaultPanelButton(Transform parentPanel,string name) {
        GameObject button = GameObject.Instantiate(Resources.Load("Prefabs/UIElements/PanelButton") as GameObject);
        button.transform.SetParent(parentPanel.transform);
        button.GetComponentInChildren<Text>().text = name;
        return button;
    }

}
