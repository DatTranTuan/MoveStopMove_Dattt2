using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;

    [SerializeField] public Player playerPrefab;

    public Player player;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(startPoint.position.x, 0.5f, startPoint.position.y), Quaternion.identity);
        player.OnInit();
        player.Joystick = JoystickManager.Instance._joystick;
        CameraFollow.Instance.Player = player;
        CameraFollow.Instance.VirtualCamera.Follow = player.transform;
        CameraFollow.Instance.VirtualCamera.LookAt = player.transform;
        CameraFollow.Instance.VirtualCamera.m_Lens.FieldOfView = 20;
        CameraFollow.Instance.Cam.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
    }

}
