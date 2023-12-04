using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;

    [SerializeField] public Player playerPrefab;

    public Player player;

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(startPoint.position.x, 0.5f, startPoint.position.y), Quaternion.identity);
        player.OnInit();
    }

}
