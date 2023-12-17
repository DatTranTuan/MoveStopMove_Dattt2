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

    public PlayerData() {
        weaponTypeData = WeaponType.Axe;
        hatTypeData = HatType.Arrow;
        pantTypeData = PantType.Batman;
        shieldTypeData = ShieldType.Shield_1;
        coin = 0;

        hatList.Add(0);
    }

}