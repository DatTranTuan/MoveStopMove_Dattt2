using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject cam;

    private void Start()
    {
        cam.transform.position = new Vector3(0f, 3f, -6f);
        cam.transform.rotation = Quaternion.Euler(16f, 0f, 0f);
    }


}
