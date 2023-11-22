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
        player = Instantiate<Player>(playerPrefab, startPoint.position, Quaternion.identity);
    }

}
