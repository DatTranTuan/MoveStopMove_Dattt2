using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HatType
{
    Arrow = 0,
    Cap = 1,
    Cowboy = 2,
    Crown = 3,
    Ear = 4,
    HeadPhone = 5,
    Horn = 6,
    PoliceCap = 7,
    StrawHat = 8
}

[Serializable]
public class HatData
{
    public HatType hatType;
    public Hat hat;
}
