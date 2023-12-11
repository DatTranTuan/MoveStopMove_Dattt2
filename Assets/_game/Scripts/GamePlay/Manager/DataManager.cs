using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO weaponDataSO;
    public List<WeaponData> listWeaponData;

    public HatDataSO hatDataSO;
    public List<HatData> listHatData;

    private PlayerData playerData;

    public int weaponIndex;

    private void Awake()
    {
        listWeaponData = weaponDataSO.listWeaponData;
        listHatData = hatDataSO.listHatData;
    }

    public void Init()
    {
        if (!PlayerPrefs.HasKey(CacheString.PLAYERPREFKEY))
        {
            CreatePlayerData();
        }

        playerData = LoadPlayerData();
    }

    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        List<WeaponData> weapons = weaponDataSO.listWeaponData;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponType == weapons[i].weaponType)
            {
                return weapons[i];
            }
        }
        return null;
    }

    public HatData GetHatData(HatType hatType)
    {
        List<HatData> hats = hatDataSO.listHatData;
        for (int i = 0; i < hats.Count; i++)
        {
            if (hatType == hats[i].hatType)
            {
                return hats[i];
            }
        }
        return null;
    }

    public void SeekWeaponPlayerData(WeaponType weaponType)
    {
        playerData.weaponTypeData = weaponType;
        SavePlayerData(playerData);
    }

    public void SeekHatPlayerData(HatType hatType)
    {
        playerData.hatTypeData = hatType;
        SavePlayerData(playerData);
    }

    public void SavePlayerData(PlayerData playerData)
    {
        string dataString = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(CacheString.PLAYERPREFKEY, dataString);
    }

    public PlayerData LoadPlayerData()
    {
        string dataString = PlayerPrefs.GetString(CacheString.PLAYERPREFKEY);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(dataString);
        return playerData;
    }

    public void CreatePlayerData()
    {
        playerData = new PlayerData();
        SavePlayerData(playerData);
    }
}
