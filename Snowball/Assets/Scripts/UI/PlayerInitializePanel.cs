using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UIPage))]
public class PlayerInitializePanel : MonoBehaviour
{
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
        panels = gameObject.GetComponent<UIPage>().initializedUIPanels;
   
        for (int i = 1; i < panels.Count; i++) {
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
        panels[(int)currentPanelType].SetActive(false);
        panels[(int)NextPanelType].SetActive(true);
        currentPanelType = nextPanelType;
    }
}


