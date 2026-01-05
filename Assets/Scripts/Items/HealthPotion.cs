using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Items/Health Item")]
public class HealthPotion : ItemDataSO
{
    [SerializeField] private float health;
    public override void PickUp()
    {
        base.PickUp();
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerVitality>().RecoverHealth(Random.Range(1,health));
    }
}
