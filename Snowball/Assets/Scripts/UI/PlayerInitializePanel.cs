using UnityEngine;
using System.Collections;

public class PlayerInitializePanel : MonoBehaviour
{
    //Double Buffer
    public panelType currentPanelType;
    private panelType nextPanelType;

    //preset for different paneltype

    public panelType NextPanelType
    {
        get
        {
            return nextPanelType;
        }

        set
        {
            nextPanelType = value;
        }
    }

    public enum panelType { }


}
