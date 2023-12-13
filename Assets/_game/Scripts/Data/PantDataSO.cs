using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class PantDataSO : ScriptableObject
{
    public List<PantData> listPantData = new List<PantData>();
}
