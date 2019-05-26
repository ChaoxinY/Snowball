using System;
using UnityEngine;

public class PlayerInitializePanel : MonoBehaviour, IPlayerInformationHolder
{
    #region Variables
    public PlayerInformation PlayerInformation { get; set; } = new PlayerInformation();
    public PlayerInitializePanelRefresher PlayerInitializePanelRefresher { get; private set; }

    #endregion

    #region Initialization
    private void Awake()
    {
        PlayerInitializePanelRefresher = new PlayerInitializePanelRefresher(gameObject);
    }

    #endregion

    #region Functionality
    #endregion
}

