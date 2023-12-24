using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public void ChangeMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
