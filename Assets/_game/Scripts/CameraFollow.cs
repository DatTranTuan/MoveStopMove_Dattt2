using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform transForm;
    [SerializeField] private Vector3 offset;

    private Transform playerTransForm;

    private void Start()
    {
        playerTransForm = LevelManager.Instance.playerPrefab.transform;
    }

    private void FixedUpdate()
    {
        transForm.position = Vector3.Lerp(transForm.position, playerTransForm.position + offset, Time.fixedDeltaTime *5f);
    }
}
