﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PowerUpInventory : MonoBehaviour, IPowerUpHolder
{
    private Action[] collectedPowerUp = new Action[2];

    public void CollectPowerUp(PowerUp powerUpToAdd)
    {
        for (int i = 0; i < collectedPowerUp.Length; i++) {
            if (collectedPowerUp[i] == null) {
                collectedPowerUp[i] = delegate { powerUpToAdd.Activate(); };                
                Destroy(powerUpToAdd.gameObject);
                return;
            }
        }      
    }  
    
    public void UsePowerUp(int index) {
        if (collectedPowerUp[index] != null)
        {   
            //Action reference is set to the class in the codebase and is not 
            //a reference to the methode of the component class in the scene.
            Action powerUpActivate = collectedPowerUp[index];
            collectedPowerUp[index] = null;
            powerUpActivate();
        }
    }
}
