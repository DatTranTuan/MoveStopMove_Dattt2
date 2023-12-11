using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public WeaponType weaponTypeData;
    public HatType hatTypeData;

    public PlayerData() {
        weaponTypeData = WeaponType.Axe;
        hatTypeData = HatType.Arrow;
    }
}