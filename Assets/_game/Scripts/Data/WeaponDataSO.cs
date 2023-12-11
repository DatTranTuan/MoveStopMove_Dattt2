using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data")]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponData> listWeaponData;

    //public Weapon GetWeapon(WeaponType weaponType)
    //{
    //    Debug.Log(weaponType);
    //    for (int i = 0; i < listWeaponData.Count; i++)
    //    {
    //        if (listWeaponData[i].weaponType == weaponType)
    //        {
    //            return listWeaponData[i].weapon;
    //        }
    //    }

    //    return null;
    //}
}
