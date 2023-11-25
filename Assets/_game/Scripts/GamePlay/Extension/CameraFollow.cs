using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;

    private Player player;

    private Vector3 offset;

    private void Start()
    {
        player = LevelManager.Instance.playerPrefab;
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        if (player.transform != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.fixedDeltaTime);
        }
    }
}
