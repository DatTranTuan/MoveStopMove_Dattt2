using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantType
{
    Batman = 0,
    chambi = 1,
    comy = 2,
    dabao = 3,
    onion = 4,
    pokemon = 5,
    rainbow = 6,
    skull = 7,
    vantim = 8
}

[Serializable]
public class PantData
{
    public PantType pantType;
    public Material material;
    public Sprite pantSprite;
}
