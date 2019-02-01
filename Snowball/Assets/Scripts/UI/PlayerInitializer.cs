using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Getting player prefabs ready to be played
public class PlayerInitializer : MonoBehaviour
{
    //totalPlayers
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>() { new ControllerInformation(), new ControllerInformation(), new ControllerInformation(), new ControllerInformation() };
    private InputSchemeAssigner inputSchemeAssigner;
    private InputSchemeRevoker inputSchemeRevoker;
    private InitializePanelAdapter initializePanelAdapter;
    private List<IUpdater> updaters = new List<IUpdater>();

    private void Start()
    {
        initializePanelAdapter = new InitializePanelAdapter(gameObject, connectedControllers);
        inputSchemeAssigner = new InputSchemeAssigner(initializePanelAdapter, connectedControllers);
        inputSchemeRevoker = new InputSchemeRevoker(connectedControllers);
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

            //Assign inputScheme
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
