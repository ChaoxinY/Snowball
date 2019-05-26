using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerInformationAssigner : MonoBehaviour
{
    #region Variables
    public List<PlayerInitializePanel> playerInitializePanels = new List<PlayerInitializePanel>();
    private PlayerPanelRefresherAdapter playerPanelRefresherAdapter;
    private InputSchemeAssigner inputSchemeAssigner;
    private InputSchemeRevoker inputSchemeRevoker;
    private List<IUpdater> updaters = new List<IUpdater>();
    #endregion

    #region Initialization
    private void Start()
    {
        playerPanelRefresherAdapter = new PlayerPanelRefresherAdapter(playerInitializePanels);
        inputSchemeAssigner = new InputSchemeAssigner(playerPanelRefresherAdapter);
        inputSchemeRevoker = new InputSchemeRevoker(playerPanelRefresherAdapter);
        updaters.AddRange(new List<IUpdater>() { inputSchemeAssigner, inputSchemeRevoker});
    }
    #endregion

    #region Functionality
    private void Update()
    {
        SystemToolMethods.UpdateIUpdaters(updaters);
    }
    #endregion
}

