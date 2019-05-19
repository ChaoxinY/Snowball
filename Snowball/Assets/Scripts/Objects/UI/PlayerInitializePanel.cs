﻿using UnityEngine;
using System.Collections.Generic;

public class PlayerInitializePanel : MonoBehaviour
{ 
    private bool focused;
    private List<GameObject> panels = new List<GameObject>();
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
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			panels.Add(gameObject.transform.GetChild(i).gameObject);
		} 
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
			//if (!panels[(int)currentPanelType].activeInHierarchy)
			//{
			//	panels[(int)currentPanelType].SetActive(true);
			//}
            return;
        }
        focused = true;
        panels[(int)currentPanelType].SetActive(false);      
        panels[(int)NextPanelType].SetActive(true);
        currentPanelType = nextPanelType;
        if (currentPanelType == PanelType.None)
        {
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

    public InitializePanelAdapter(GameObject gameObject, List<ControllerInformation> controllerInformations)
    {
        PlayerInitializePanel[] panels = gameObject.GetComponentsInChildren<PlayerInitializePanel>();
	
        for (int i = 0; i < panels.Length; i++)
        {
            playerInitializePanels.Add(panels[i]);
        }
		this.controllerInformations = controllerInformations;
    }

    public void RefreshPanel()
    {
        int i = 0;
		foreach (PlayerInitializePanel panel in playerInitializePanels)
		{
			switch (controllerInformations[i].controller)
			{
				case ControllerInformation.ControllerType.None:
					panel.NextPanelType = PlayerInitializePanel.PanelType.None;
					break;
				case ControllerInformation.ControllerType.Controller:
					panel.NextPanelType = PlayerInitializePanel.PanelType.ControllerCharacterSelection;
					break;
				case ControllerInformation.ControllerType.Keyboard:
					panel.NextPanelType = PlayerInitializePanel.PanelType.KeyBoardCharacterSelection;
					break;
			}
			i++;
		}
    }
}

