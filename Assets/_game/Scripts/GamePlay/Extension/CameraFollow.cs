using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime;

    private Vector3 targetPos;
    private Player player;

    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        player = LevelManager.Instance.player;
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
    }
}
