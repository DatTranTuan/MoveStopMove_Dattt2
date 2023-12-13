using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantItemUI : MonoBehaviour
{
    private PantData pantData;
    [SerializeField] private Image pantImage;
    [SerializeField] private Button pantButton;

    public void SetData(PantData pantData, Action<PantData> callBack)
    {
        this.pantData = pantData;
        this.pantImage.sprite = pantData.pantSprite;
        pantButton.onClick.AddListener(() =>
        {
            callBack?.Invoke(pantData);
        });
    }
}
