using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Hammer = 0,
    Knife = 1,
    Axe = 2,
    Candy = 3
}

[Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public Bullet bullet;
    public Weapon weapon;
    public float range;
}

