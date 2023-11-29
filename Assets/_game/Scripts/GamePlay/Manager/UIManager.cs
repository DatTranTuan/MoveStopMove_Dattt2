using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject menuStartCanvas;

    private void Start()
    {
        Time.timeScale = 0;
        //cam.transform.position = new Vector3(0f, 3f, -6f);
        //cam.transform.rotation = Quaternion.Euler(16f, 0f, 0f);
    }

    public void OnClickPlayButton()
    {
        Time.timeScale = 1;
        menuStartCanvas.SetActive(false);
        //cam.transform.position = new Vector3(0f, 21f, -18f);
        //cam.transform.rotation = Quaternion.Euler(31f, 0f, 0f);
    }
}
