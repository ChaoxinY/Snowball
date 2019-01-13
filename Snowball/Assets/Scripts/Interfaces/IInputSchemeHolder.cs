using UnityEngine;
using System.Collections;

//Instead of creating a new class just to return the inputscheme property or 
//make players classes inherit an abstract class that has the inputscheme property.
//I made this interface which lets the client know that a certain class contains a 
//inputscheme property.
public interface IInputSchemeHolder
{
    InputScheme GetInputScheme();
    void SetInputScheme(InputScheme inputScheme);
}
