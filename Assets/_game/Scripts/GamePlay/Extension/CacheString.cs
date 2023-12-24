using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheString : MonoBehaviour
{
    public static string IDLE_ANIMATION = "IsIdle";
    public static string ATTACK_ANIMATION = "IsAttack";
    public static string DEAD_ANIMATION = "IsDead";
    public static string WIN_ANIMATION;
    public static string DANCE_ANIMATION = "IsDance";
    public static string ULTI_ANIMATION;

    public static string SCENE_NAME = "SampleScene";

    public static string PLAYERPREFKEY = "PlayerData";

    public static string BULLET_TAG = "Bullet";
    public static string PLAYER_TAG = "Player";
    public static string BOT_TAG = "Bot";

    public static int DEFAULT_LAYER = 0;
    public static int BOT_LAYER = 3;
    public static int PLAYER_LAYER = 6;
    public static int RADAR_LAYER = 7;
    public static int CHARACTER_LAYER = 9;
    public static int CUP_LAYER = 10;
}
