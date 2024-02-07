using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonus : BonusBase
{
    [SerializeField] private int coinAmount;

    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void GetBonus()
    {
        base.GetBonus();
        CoinManager.Instance.AddCoin(coinAmount);
    }
}
