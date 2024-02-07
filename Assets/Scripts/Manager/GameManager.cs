using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Persistance<GameManager>
{

    

    public Color weaponNormalColor;
     public Color weaponRareColor;
     public Color weaponEpicColor;
     public Color weaponLegendColor;
    public GameData gameData;
    public PlayerConfig playerPrefab;
    protected override void Awake()
    {
        base.Awake();
        gameData = SaveSystem.Load();
    }

    public Color ChooseColorForWeapon(WeaponData weapon)
    {
        switch (weapon.weaponRarity)
        {
            case WeaponData.WeaponRarity.Normal:
                return weaponNormalColor;
            case WeaponData.WeaponRarity.Rare:
                return weaponRareColor;
            case WeaponData.WeaponRarity.Epic:
                return weaponEpicColor;
            case WeaponData.WeaponRarity.Legend:
                return weaponLegendColor;
        }
        return weaponNormalColor;
    }
    
}
