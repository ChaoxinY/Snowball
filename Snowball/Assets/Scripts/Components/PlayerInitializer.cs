using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Getting player prefabs ready to be played
public class PlayerInitializer : MonoBehaviour
{
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>();
    private InputSchemeAssigner inputSchemeAssigner;
    private InputSchemeRevoker inputSchemeRevoker;
    private List<IUpdater> updaters = new List<IUpdater>();

    private void Start()
    {
        inputSchemeAssigner = new InputSchemeAssigner(connectedControllers);
        inputSchemeRevoker = new InputSchemeRevoker(connectedControllers);
        updaters.AddRange(new List<IUpdater>() { inputSchemeAssigner, inputSchemeRevoker});
    }

    private void Update()
    {
        //Worth to store this as an static method?
        //For now i say yes because two classes already repeat this code
        SystemToolMethods.UpdateIUpdaters(updaters);
    }

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
                if (Resources.Load(c.controllerOrder+"CustomControllerInput"))
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
