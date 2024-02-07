using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Items/Health Item")]
public class HealthPotion : ItemData
{
    [SerializeField] private float health;
    public override void PickUp()
    {
        base.PickUp();
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerHealth>().RecoverHealth(Random.Range(1,health));
    }
}
