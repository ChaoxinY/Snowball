using UnityEngine;
using System.Collections;

//Sandbox pattern
//Cohesion :

//Coupling : Currently none

public abstract class PowerUp : MonoBehaviour
{
    //Single Responsibility: Removed picked up methode because it has nothing to with the powerup class
    //public abstract void PickedUp();
    public abstract void Activate();
}

