using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIPage : MonoBehaviour
{ 
    public Transform canvasTransform;   
    public bool isActive;
    public List<GameObject> initializedUIPanels = new List<GameObject>();
    public List<GameObject> initializedUIElements = new List<GameObject>();
    //Instead of whole class register only the necessary information
    private List<IFocusUIElement> focusedUIElements = new List<IFocusUIElement>();
    [SerializeField]
    public UIPagePreset uIPage;
    private CoroutineToolMethods coroutineToolMethods;
    public UIPageHolder uIPageHolder;

    public List<IFocusUIElement> FocusedUIElements
    {
        get
        {
            return focusedUIElements;
        }

        set
        {
            focusedUIElements = value;
        }
    }

    private void Start()
    {   
        canvasTransform = GameObject.Find("UICanvas").transform;
        uIPageHolder = canvasTransform.gameObject.GetComponent<UIPageHolder>();
        uIPageHolder.AddPage(this);
        gameObject.AddComponent<CoroutineToolMethods>();
        coroutineToolMethods = gameObject.GetComponent<CoroutineToolMethods>();
      
        foreach (UIPanel panel in uIPage.panels)
        {
            GameObject newPanel = null;

            //InitializePanel 
            //Load prefab panel if given
            if (panel.panelPreset != null)
            {
                newPanel = UIToolMethods.AddUIPanel(gameObject.transform, panel.panelName, panel.panelPreset.name);
            }
            else
            {
                newPanel = UIToolMethods.AddUIPanel(gameObject.transform, panel.panelName);
                 //Add Layout if nessecary
                AddPanelLayout(newPanel, panel);
            }
            initializedUIPanels.Add(newPanel);
            //Set panel properties(size, position)             
            if (panel.usesCustomTransformProperty)
            {
                RectTransform panelTransform = newPanel.GetComponent<RectTransform>();
                panelTransform.anchoredPosition = panel.panelPosition;
                panelTransform.localScale = new Vector3(panel.panelSize.x, panel.panelSize.y, 0);
            }

            //Initialize UI elements
            //Initialize buttons.
            if (panel.uIButtons.Count > 0)
            {
                for (int i = 0; i < panel.uIButtons.Count; i++)
                {
                    initializedUIElements.Add(AddButton(newPanel, panel.uIButtons[i]));
                }
            }          
        }
    }

    //Functionality move to other script
    private void OnDisable()
    {
        isActive = false;
    }

    private void OnEnable()
    {
        isActive = true;
        //foreach (GameObject g in initializedUIPanels) {
        //    if (!g.activeInHierarchy) {
        //        g.SetActive(true);
        //    }
        //}
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

    private GameObject AddButton(GameObject panel, UIButton uIButton)
    {
        GameObject newButton = UIToolMethods.AddUIButton(panel.transform, uIButton.buttonName);
               
        //Add functionality
        switch (uIButton.buttonFunction)
        {
            case UIButton.ButtonFunctions.ClosePanel:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.DisableGameObject(uIButton.panelToOpen.name); });
                break;
            case UIButton.ButtonFunctions.OpenPanel:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.OpenUIPanel(canvasTransform, uIButton.panelToOpen.name); });
                break;
            case UIButton.ButtonFunctions.LoadScene:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(coroutineToolMethods.LoadScene(uIButton.sceneToOpen)); });
                break;
            case UIButton.ButtonFunctions.ExitGame:
                newButton.GetComponent<Button>().onClick.AddListener(delegate { UIToolMethods.ExitGame(); });
                break;
            case UIButton.ButtonFunctions.OpenUIPage:
                newButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    UIToolMethods.OpenUIPanel(canvasTransform, uIButton.panelToOpen.name);
                    UIToolMethods.DisableGameObject(gameObject.name);
                });
                break;
                //Complex UI functions can be added through adding a script component.
        }
        if (uIButton.usesCustomTransformProperty)
        {
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = uIButton.buttonPosition;
            buttonTransform.localScale = new Vector3(uIButton.buttonSize.x, uIButton.buttonSize.y, 0);
        }
        return newButton;
    }
}
