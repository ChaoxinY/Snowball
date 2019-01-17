using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Representing different menu panels 
public class ButtonNavigationPanel : MonoBehaviour {

    public GameObject panel;
    public List<string> panelNames = new List<string>();

    private void Start()
    {
        UIToolMethods.AddPanelNavigateButtons(panel, panelNames);
    }

}
