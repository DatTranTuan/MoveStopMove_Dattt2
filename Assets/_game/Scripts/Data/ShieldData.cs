using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldType
{
    Shield_1 = 0,
    Shield_2 = 1
}

[Serializable]
public class ShieldData
{
    public ShieldType shieldType;
    public Shield shield;
    public Sprite shieldSprite;
    public int price;
}
