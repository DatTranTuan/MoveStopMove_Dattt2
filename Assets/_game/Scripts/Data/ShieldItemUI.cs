using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldItemUI : MonoBehaviour
{
    private ShieldData shieldData;
    [SerializeField] private Image shieldImage;
    [SerializeField] private Button shieldButton;

    public void SetData(ShieldData shieldData, Action<ShieldData> callBack)
    {
        this.shieldData = shieldData;
        this.shieldImage.sprite = shieldData.shieldSprite;
        shieldButton.onClick.AddListener(() =>
        {
            callBack?.Invoke(shieldData);
        });
    }
}
