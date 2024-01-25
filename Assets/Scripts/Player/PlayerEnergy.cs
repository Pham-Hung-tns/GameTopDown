using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;

    public bool CanUseEnergy => playerConfig.currentEnergy > 0;
    public void RecoverEnergy(float amount)
    {
        playerConfig.currentEnergy += amount;
        if (playerConfig.currentEnergy > playerConfig.MaxEnergy)
        {
            playerConfig.currentEnergy = playerConfig.MaxEnergy;
        }
    }

    public void UseEnergy(float amount)
    {
        playerConfig.currentEnergy -= amount;
        if(playerConfig.currentEnergy < 0)
        {
            playerConfig.currentEnergy = 0;
        }
    }
}
