using UnityEngine;
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
        inputSchemeRevoker = new InputSchemeRevoker(initializePanelAdapter, connectedControllers);
        updaters.AddRange(new List<IUpdater>() { inputSchemeAssigner, inputSchemeRevoker });
    }

    private void Update()
    {
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
            playerCharacter.GetComponent<IInputSchemeHolder>().InputScheme = inputScheme;
        }
    }
}

