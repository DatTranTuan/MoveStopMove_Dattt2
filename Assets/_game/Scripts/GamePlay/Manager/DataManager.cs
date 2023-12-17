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

    public PantDataSO pantDataSO;
    public List<PantData> listPantData;

    public ShieldDataSO shieldDataSO;
    public List<ShieldData> listShieldData;

    private PlayerData playerData;

    public int weaponIndex;

    private void Awake()
    {
        listWeaponData = weaponDataSO.listWeaponData;
        listHatData = hatDataSO.listHatData;
        listPantData = pantDataSO.listPantData;
        listShieldData = shieldDataSO.listShield;
    }

    public void Init()
    {
        if (!PlayerPrefs.HasKey(CacheString.PLAYERPREFKEY))
        {
            CreatePlayerData();
        }

        playerData = LoadPlayerData();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
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

    public HatData GetHatData()
    {
        List<HatData> hats = hatDataSO.listHatData;
        for (int i = 0; i < hats.Count; i++)
        {
            if (playerData.hatTypeData == hats[i].hatType)
            {
                return hats[i];
            }
        }
        return null;
    }

    public int CoinData { get => playerData.coin; set => playerData.coin = value; }

    public void SaveWeaponPlayerData(WeaponType weaponType)
    {
        playerData.weaponTypeData = weaponType;
        SavePlayerData(playerData);
    }

    public void SaveHatPlayerData(HatType hatType)
    {
        playerData.hatTypeData = hatType;
        SavePlayerData(playerData);
    }

    public void SavePantPlayerData(PantType pantType)
    {
        playerData.pantTypeData = pantType;
        SavePlayerData(playerData);
    }

    public void SaveShieldPlayerData(ShieldType shieldType)
    {
        playerData.shieldTypeData = shieldType;
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
