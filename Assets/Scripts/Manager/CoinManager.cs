using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int totalCoins { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
       totalCoins = GameManager.Instance.gameData.totalCoin;
    }
    public void AddCoin(int amount)
    {
        totalCoins += amount;

    }

    public void RemoveCoin(int amount) 
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;

        }
    }
}
