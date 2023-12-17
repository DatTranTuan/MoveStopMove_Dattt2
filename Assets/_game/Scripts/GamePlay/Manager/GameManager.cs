using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Text coinText;

    private int index = 0;

    public int Index { get => index; set => index = value; }
    public Text CoinText { get => coinText; set => coinText = value; }

    private void Awake()
    {
        DataManager.Instance.Init();
    }

    private void Start()
    {
        coinText.text = DataManager.Instance.CoinData.ToString();
    }
}
