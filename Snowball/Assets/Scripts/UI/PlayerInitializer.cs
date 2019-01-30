using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Getting player prefabs ready to be played
public class PlayerInitializer : MonoBehaviour
{
    //totalPlayers
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>();
    private InputSchemeAssigner inputSchemeAssigner;
    private InputSchemeRevoker inputSchemeRevoker;
    private List<IUpdater> updaters = new List<IUpdater>();

    public List<ControllerInformation> ConnectedControllers
    {
        get
        {
            return connectedControllers;
        }

        set
        {
            connectedControllers = value;
            //when set change ui
        }
    }

    private void Start()
    {
        inputSchemeAssigner = new InputSchemeAssigner(ConnectedControllers);
        inputSchemeRevoker = new InputSchemeRevoker(ConnectedControllers);
        updaters.AddRange(new List<IUpdater>() { inputSchemeAssigner, inputSchemeRevoker });
    }

    private void Update()
    {
        //Worth to store this as an static method?
        //For now i say yes because two classes already repeat this code
        SystemToolMethods.UpdateIUpdaters(updaters);
    }

    //put in other class
    public void GeneratePlayers()
    {
        foreach (ControllerInformation c in connectedControllers)
        {
            GameObject playerCharacter = Instantiate(Resources.Load("InsertCharacterStringHere") as GameObject);
            InputScheme inputScheme = new InputScheme();

            if (c.controller == ControllerInformation.ControllerType.Keyboard)
            {
                //if there is already a custom profile created, load that.
                if (Resources.Load("CustomKeyboardInput"))
                {
                    inputScheme = Resources.Load("CustomKeyboardInput") as InputScheme;
                }
                else
                {
                    //load the default profile for keyboard
                    inputScheme = Resources.Load("DefaultKeyboardInput") as InputScheme;
                }
            }

            else if (c.controller == ControllerInformation.ControllerType.Controller)
            {
                //if there is already a custom profile created, load that.
                if (Resources.Load(c.controllerOrder + "CustomControllerInput"))
                {
                    inputScheme = Resources.Load(c.controllerOrder + "CustomControllerInput") as InputScheme;
                }
                else
                {
                    //load the default profile for controller                   
                    inputScheme = Resources.Load("DefaultControllerInput") as InputScheme;
                    inputScheme.ControllerOrder = c.controllerOrder;
                }
            }
            playerCharacter.GetComponent<IInputSchemeHolder>().SetInputScheme(inputScheme);
        }
    }

    public class ControllerAssignerPanelChanger
    {
        //panelslot
        private Transform canvasTransform;
        private UIPagePreset UIPagePreset;
        private List<UIPanel> assignPanels;
        private List<ControllerInformation> controllerInformation;
        public ControllerAssignerPanelChanger(Transform canvasTransform, UIPagePreset uIPagePreset, List<ControllerInformation> controllerInformation)
        {
            this.canvasTransform = canvasTransform;
            this.UIPagePreset = uIPagePreset;
            for (int i = 0; i < 4; i++) {
                assignPanels.Add(uIPagePreset.panels[i]);
            }
            this.controllerInformation = controllerInformation;
        }

        public void UpdateAssignerPanels() {
            for (int i = 0; i < controllerInformation.Count; i++) {
                if(controllerInformation[i].controller == ControllerInformation.ControllerType.Controller) {
                    UIToolMethods.AddUIPanel(canvasTransform, "Player" + i , "ControllerCharacterAssignPanel");
                    UIToolMethods.DisableGameObject("Player" + i);
                }
                else if (controllerInformation[i].controller == ControllerInformation.ControllerType.Keyboard)
                {
                    UIToolMethods.AddUIPanel(canvasTransform, "Player" + i, "KeyBoardCharacterAssignPanel");
                    UIToolMethods.DisableGameObject("Player" + i);
                }
                else if (controllerInformation[i].controller == ControllerInformation.ControllerType.None)
                {
                    UIToolMethods.OpenUIPanel(canvasTransform, "ControllerAssignPanel");
                }
            }
        }
    }
}
