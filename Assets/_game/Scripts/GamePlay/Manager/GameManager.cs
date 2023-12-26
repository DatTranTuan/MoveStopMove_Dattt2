using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Text coinText;

    private float giftCountDown = 5f;

    private int index = 0;

    public int Index { get => index; set => index = value; }
    public Text CoinText { get => coinText; set => coinText = value; }

    private void Awake()
    {
        DataManager.Instance.Init();

        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start()
    {
        coinText.text = DataManager.Instance.CoinData.ToString();
    }

    public IEnumerator GiftEffect()
    {
        while (giftCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            giftCountDown--;
            if (giftCountDown <= 5f)
            {
                LevelManager.Instance.player.gameObject.layer = CacheString.DEFAULT_LAYER;
            }
        }
        LevelManager.Instance.player.gameObject.layer = CacheString.CHARACTER_LAYER;
        giftCountDown = 5f;
    }

    public void TakeGift ()
    {
        StartCoroutine(GiftEffect());
    }
}
