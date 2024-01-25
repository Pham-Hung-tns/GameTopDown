using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy Potion", menuName = "Items/Energy Item")]
public class EnergyPotion : ItemData
{
    [SerializeField] private float energy;
    public override void PickUp()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerEnergy>().RecoverEnergy(energy);
    }
}
