using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatData")]
public class HatDataSO : ScriptableObject
{
    public List<HatData> listHatData = new List<HatData>();
}
