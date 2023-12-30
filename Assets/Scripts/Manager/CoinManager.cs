using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Persistance<CoinManager>
{
    [SerializeField] private int initialCoinTest;
    public int totalCoins { get; private set; }
    public const string CONIN_KEY = "COIN";

    protected override void Awake()
    {
        base.Awake();
        totalCoins = initialCoinTest;
    }

    private void Start()
    {
        //coinValue = PlayerPrefs.GetInt(CONIN_KEY);
    }
    public void AddCoin(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt(CONIN_KEY, totalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoin(int amount) 
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            Debug.Log(totalCoins);
            PlayerPrefs.SetInt(CONIN_KEY, totalCoins);
            PlayerPrefs.Save();
        }
    }
}
