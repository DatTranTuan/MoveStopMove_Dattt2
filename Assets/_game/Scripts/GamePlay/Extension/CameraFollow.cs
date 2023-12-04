using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private GameObject cam;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private Player player;
    //private int time = 20;
    //private int timePlus = 100;

    private void Start()
    {
        player = LevelManager.Instance.player;
        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
        virtualCamera.m_Lens.FieldOfView = 20;
        cam.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
    }

    private void Update()
    {
        
    }

    public void SmoothCameraChange()
    {
        cam.transform.rotation = Quaternion.Euler(31f, 0f, 0f);
        virtualCamera.m_Lens.FieldOfView = 100;
    }
}
