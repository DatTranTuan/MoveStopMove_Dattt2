using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Axe = 0,
    Hammer = 1,
    Knife = 2,
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

