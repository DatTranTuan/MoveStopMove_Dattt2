using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Hammer,
    Knife,
    Axe,
    Candy
}

[Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public Bullet bullet;
    public Weapon weapon;
    public float range;
}

