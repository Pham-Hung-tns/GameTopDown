using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBonus : BonusBase
{
    protected override void GetBonus()
    {
        base.GetBonus();
        player.GetComponent<PlayerEnergy>().RecoverEnergy(1f);
    }
}
