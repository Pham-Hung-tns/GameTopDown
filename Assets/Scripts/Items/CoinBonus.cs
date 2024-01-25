using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonus : BonusBase
{
    [SerializeField] private int coinAmount;
    protected override void GetBonus()
    {
        base.GetBonus();
        CoinManager.Instance.AddCoin(coinAmount);
    }
}
