using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData")]
public class ShieldDataSO : ScriptableObject
{
    public List<ShieldData> listShield = new List<ShieldData>();
}
