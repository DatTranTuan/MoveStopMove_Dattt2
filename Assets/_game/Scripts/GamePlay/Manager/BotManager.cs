using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    private int botIndex = 20;

    public static Stack<Bot> stack = new Stack<Bot>();

    [SerializeField] private Bot botPrefab;

    private Vector3 randomPos;

    private void Start()
    {
        randomPos = new Vector3(Random.Range(-10, 11), 0, UnityEngine.Random.Range(-9, 10));
    }

    private void SpawnBot(Bot bot)
    {
        botIndex++;
        if (stack.Count > 0)
        {
            Bot spawnBot;
            spawnBot = stack.Pop();
            spawnBot.gameObject.SetActive(true);
            spawnBot.gameObject.transform.position = new Vector3(Random.Range(-10, 11), 0, Random.Range(-9, 10));
            bot.IsAlive = true;
            bot.agent.ResetPath();
        }
        else
        {
            Instantiate(botPrefab, randomPos, Quaternion.identity);
            botPrefab.OnInit();
        }
    }

    public void ReturnToPool(Bot bot)
    {
        botIndex--;
        LevelManager.Instance.survivorIndex--;
        bot.agent.ResetPath();
        stack.Push(bot);
        bot.gameObject.SetActive(false);
        bot.MainTarget = null;
        bot.OtherTarget.Clear();
        if (BotManager.Instance.botIndex > LevelManager.Instance.survivorIndex)
        {
            SpawnBot(bot);
        }
    }

    //public void SpawnBotWhenStart()
    //{
    //    for (int i = 1; i < 11; i++)
    //    {
    //        SpawnBot();
    //        BotManager.Instance.botIndex++;
    //    }
    //}
}
