using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(UIPage))]
public class PlayerInitializePanel : MonoBehaviour, IFocusUIElement
{
    private bool focused;
    private UIPage uIPage;
    private List<GameObject> panels;
    private PanelType currentPanelType;
    private PanelType nextPanelType;

    public PanelType NextPanelType
    {
        get
        {
            return nextPanelType;
        }

        set
        {
            nextPanelType = value;
            RefreshPanels();
        }
    }

    public enum PanelType
    {
        None,
        ControllerCharacterSelection,
        KeyBoardCharacterSelection,
    }

    //preset for different paneltype 
    private void Start()
    {
        uIPage = gameObject.GetComponent<UIPage>();
        panels = uIPage.initializedUIPanels;
        uIPage.FocusedUIElements.Add(this);
        for (int i = 1; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }
        currentPanelType = PanelType.None;
        nextPanelType = PanelType.None;
    }

    public void RefreshPanels()
    {
        if (NextPanelType == currentPanelType)
        {
            return;
        }
        focused = true;
        panels[(int)currentPanelType].SetActive(false);      
        panels[(int)NextPanelType].SetActive(true);
        currentPanelType = nextPanelType;
        if (currentPanelType == PanelType.None)
        {
            Debug.Log((int)currentPanelType);
            focused = false;
        }
    }

    public bool IsThisElementInFocus()
    {
        return focused;
    }
}
public class InitializePanelAdapter
{
    private List<PlayerInitializePanel> playerInitializePanels = new List<PlayerInitializePanel>();
    private List<ControllerInformation> controllerInformations = new List<ControllerInformation>();

    public InitializePanelAdapter(GameObject gameObject, List<ControllerInformation> controllerInformation)
    {
        PlayerInitializePanel[] panels = gameObject.GetComponentsInChildren<PlayerInitializePanel>();
        for (int i = 0; i < panels.Length; i++)
        {
            playerInitializePanels.Add(panels[i]);
        }
        this.controllerInformations = controllerInformation;
    }

    public void RefreshPanel()
    {

        int i = 0;
        foreach (ControllerInformation c in controllerInformations)
        {
            switch (c.controller)
            {
                case ControllerInformation.ControllerType.None:
                    playerInitializePanels[i].NextPanelType = PlayerInitializePanel.PanelType.None;
                    break;
                case ControllerInformation.ControllerType.Controller:
                    playerInitializePanels[i].NextPanelType = PlayerInitializePanel.PanelType.ControllerCharacterSelection;
                    break;
                case ControllerInformation.ControllerType.Keyboard:
                    playerInitializePanels[i].NextPanelType = PlayerInitializePanel.PanelType.KeyBoardCharacterSelection;
                    break;
            }
            i++;
        }
    }
}


