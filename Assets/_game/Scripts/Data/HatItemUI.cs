using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatItemUI : MonoBehaviour 
{
    private HatData hatData;
    [SerializeField] private Image hatImage;
    [SerializeField] private Button hatButton;

    public void SetData(HatData hatData, Action<HatData> callBack)
    {
        this.hatData = hatData;
        this.hatImage.sprite = hatData.hatSprite;
        hatButton.onClick.AddListener(() =>
        {
            callBack?.Invoke(hatData);
        });
    }
}
