using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIPageInitializer : MonoBehaviour
{
    public UIPage uIPage;
    public Transform canvasTransform;
    private List<GameObject> initializedPanels = new List<GameObject>();
    private CoroutineToolMethods coroutineToolMethods;

    private void Start()
    {
        canvasTransform = GameObject.Find("UICanvas").transform;
        gameObject.AddComponent<CoroutineToolMethods>();
        coroutineToolMethods = gameObject.GetComponent<CoroutineToolMethods>();
        foreach (UIPanel panel in uIPage.panels)
        {
            GameObject newPanel = null;

            //InitializePanel
            //Load prefab panel if given
            if (panel.panelPresetName != null)
            {
                newPanel = UIToolMethods.AddUIPanel(gameObject.transform, panel.panelName, panel.panelPresetName);
            }
            else
            {
                newPanel = UIToolMethods.AddUIPanel(gameObject.transform, panel.panelName);
                //Add Layout if nessecary
                AddPanelLayout(newPanel, panel);
            }

            //Initialize UI elements
            //Initialize buttons.
            if (panel.uIButtons.Count > 0)
            {
                for (int i = 0; i < panel.uIButtons.Count; i++)
                {
                    AddButton(newPanel, panel.uIButtons[i]);
                }
            }
        }
    }

    private void AddPanelLayout(GameObject panel, UIPanel panelData)
    {
        //Add functionality
        switch (panelData.layout)
        {
            case UIPanel.LayoutTypes.None:
                break;
            case UIPanel.LayoutTypes.Horizontal:
                panel.AddComponent<HorizontalLayoutGroup>();
                break;
            case UIPanel.LayoutTypes.Vertical:
                panel.AddComponent<VerticalLayoutGroup>();
                break;
            case UIPanel.LayoutTypes.Grid:
                panel.AddComponent<GridLayoutGroup>();
                break;
        }
    }

    private void AddButton(GameObject panel, UIButton uIButton)
    {
        GameObject newButton = UIToolMethods.AddUIButton(panel.transform, uIButton.buttonName);
        //Add functionality
        switch (uIButton.buttonFunction)
        {
            case UIButton.ButtonFunctions.ClosePanel:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.DisableGameObject(uIButton.panelName); });
                break;
            case UIButton.ButtonFunctions.OpenPanel:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.OpenUIPanel(canvasTransform, uIButton.panelName); });
                break;
            case UIButton.ButtonFunctions.LoadScene:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(coroutineToolMethods.LoadScene(uIButton.sceneName)); });
                break;
            case UIButton.ButtonFunctions.ExitGame:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.ExitGame(); });
                break;
            case UIButton.ButtonFunctions.OpenUIPage:
                newButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    UIToolMethods.OpenUIPanel(canvasTransform, uIButton.panelName);
                    UIToolMethods.DisableGameObject(gameObject.name);
                });
                break;
        }
    }
}
