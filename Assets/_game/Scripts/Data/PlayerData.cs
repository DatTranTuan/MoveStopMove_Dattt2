using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public WeaponType weaponTypeData;
    public HatType hatTypeData;
    public PantType pantTypeData;
    public ShieldType shieldTypeData;
    public int coin;

    public List<int> hatList = new List<int>();
    public List<int> pantList = new List<int>();
    public List<int> shieldList = new List<int>();

    public PlayerData() {
        weaponTypeData = WeaponType.Axe;
        hatTypeData = HatType.Arrow;
        pantTypeData = PantType.Batman;
        shieldTypeData = ShieldType.Shield_1;
        coin = 0;

        hatList.Add(0);
        pantList.Add(0);
        shieldList.Add(0);
    }

}