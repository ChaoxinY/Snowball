using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInitializePanelRefresher
{
    private bool focused;	
    private List<GameObject> panels = new List<GameObject>();
    private GameObject gameObject;
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

    public PlayerInitializePanelRefresher(GameObject gameObject)
    {
        this.gameObject = gameObject;
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

public class PlayerPanelRefresherAdapter
{
    private List<PlayerInitializePanelRefresher> playerInitializePanelRefreshers = new List<PlayerInitializePanelRefresher>();
    public List<ControllerInformation> ControllerInformations { get; set; } = new List<ControllerInformation>();

    public PlayerPanelRefresherAdapter(List<PlayerInitializePanel> playerInitializePanels)
    {
        playerInitializePanelRefreshers = playerInitializePanels.Select(panel=>panel.PlayerInitializePanelRefresher).ToList();
        foreach (var panel in playerInitializePanelRefreshers)
        {
            Debug.Log(panel);
        }
		ControllerInformations = playerInitializePanels.Select(panel => panel.PlayerInformation.playerControllerInformation).ToList();
    }

    public void RefreshPanel()
    {
        int i = 0;
		foreach (PlayerInitializePanelRefresher panel in playerInitializePanelRefreshers)
		{
			switch (ControllerInformations[i].controller)
			{
				case ControllerInformation.ControllerType.None:
					panel.NextPanelType = PlayerInitializePanelRefresher.PanelType.None;
					break;
				case ControllerInformation.ControllerType.Controller:
					panel.NextPanelType = PlayerInitializePanelRefresher.PanelType.ControllerCharacterSelection;
					break;
				case ControllerInformation.ControllerType.Keyboard:
					panel.NextPanelType = PlayerInitializePanelRefresher.PanelType.KeyBoardCharacterSelection;
					break;
			}
			i++;
		}
    }
}


