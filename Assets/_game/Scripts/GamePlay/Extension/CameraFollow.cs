using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private GameObject cam;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float speed;

    private Player player;

    public Player Player { get => player; set => player = value; }
    public GameObject Cam { get => cam; set => cam = value; }
    public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; set => virtualCamera = value; }


    private void Update()
    {
        VirtualCamera.m_Lens.FieldOfView += Time.deltaTime * speed;
        if (VirtualCamera.m_Lens.FieldOfView >= 90)
        {
            VirtualCamera.m_Lens.FieldOfView = 90;
        }
    }

    public void SmoothCameraChange()
    {
        Cam.transform.rotation = Quaternion.Euler(41f, 0f, 0f);
        //virtualCamera.m_Lens.FieldOfView = 100;
    }
}
