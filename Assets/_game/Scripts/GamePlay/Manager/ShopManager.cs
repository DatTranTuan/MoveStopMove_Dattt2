using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Transform weaponPos;
    [SerializeField] private Transform hatPos;
    [SerializeField] private Transform pantPos;
    [SerializeField] private Transform shieldPos;
    [SerializeField] private Text textWeapon;

    [SerializeField] private HatItemUI hatItemUI;
    [SerializeField] private PantItemUI pantItemUI;
    [SerializeField] private ShieldItemUI shieldItemUI;

    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Button equipedBtn;

    private WeaponData weaponData;
    private WeaponType currentWeaponShop;

    public int weaponIndex;
    public int hatIndex;

    public List<WeaponData> listWeapon;
    private Weapon currentWeapon;
    private Weapon lastWeapon;
    private PlayerData playerData;

    public List<Weapon> list = new List<Weapon>();

    public List<HatData> listHat;
    private HatImage hatImage;
    private HatData currentSelectHatData;

    public List<PantData> listPant;
    private PantImage pantImage;
    private PantData currentSelectPantData;

    private List<ShieldData> listShield;
    private ShieldData currentSelectShieldData;

    private List<Hat> listEquipHat;

    private void Start()
    {
        OnInit();
        playerData = DataManager.Instance.GetPlayerData();
    }

    public void SpawnWeaponShop(Weapon wepon)
    {
        Weapon wp = LeanPool.Spawn(wepon, weaponPos);
        list.Add(wp);
        if (list.Count == 0)
        {
            currentWeapon = wp;
            lastWeapon = currentWeapon;
        }
        else if (list.Count > 0)
        {
            lastWeapon = list[0];
        }
    }


    public void OnInit()
    {
        weaponIndex = 0;
        listWeapon = DataManager.Instance.listWeaponData;
        listHat = DataManager.Instance.listHatData;
        listPant = DataManager.Instance.listPantData;
        listShield = DataManager.Instance.listShieldData;

        currentWeaponShop = WeaponType.Axe;
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponShop);
        }

        SpawnHatShop();
        SpawnPantShop();
        SpawnShieldShop();

        //hatType = HatType.Arrow;
        //if (hatData == null)
        //{
        //    hatData = DataManager.Instance.GetHatData(hatType);
        //}
    }

    public void SpawnHatShop()
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            hatItemUI = Instantiate(hatItemUI, hatPos);
            hatItemUI.SetData(listHat[i], OnCLickHatButton);
        }
    }

    public void SpawnPantShop()
    {
        for (int i = 0; i < listPant.Count; i++)
        {
            pantItemUI = Instantiate(pantItemUI, pantPos);
            pantItemUI.SetData(listPant[i], OnCLickPantButton);
        }
    }

    public void SpawnShieldShop()
    {
        for (int i = 0; i < listShield.Count; i++)
        {
            shieldItemUI = Instantiate(shieldItemUI, shieldPos);
            shieldItemUI.SetData(listShield[i], OnClickShieldButton);
        }
    }

    public void OnClickShieldButton(ShieldData shieldData)
    {
        currentSelectShieldData = shieldData;
        CheckShieldStatus(currentSelectShieldData);
    }

    public void OnClickEquipShieldButton()
    {
        LevelManager.Instance.player.CurrentShieldType = currentSelectShieldData.shieldType;
        LevelManager.Instance.player.EquipShield();
    }

    public void OnClickBuyShieldButton()
    {
        LevelManager.Instance.player.CurrentShieldType = currentSelectShieldData.shieldType;
        if (playerData.coin >= currentSelectShieldData.price)
        {
            playerData.coin -= currentSelectShieldData.price;
            playerData.shieldList.Add((int)currentSelectShieldData.shieldType);
            DataManager.Instance.SavePlayerData(playerData);
            LevelManager.Instance.player.EquipShield();
            GameManager.Instance.CoinText.text = DataManager.Instance.CoinData.ToString();
            CheckShieldStatus(currentSelectShieldData);
        }
    }

    public void OnCLickPantButton(PantData pantData)
    {
        currentSelectPantData = pantData;
        CheckPantStatus(currentSelectPantData);
    }

    public void OnClickEquipPantButton()
    {
        LevelManager.Instance.player.CurrentPantType = currentSelectPantData.pantType;
        LevelManager.Instance.player.EquipPant();
    }

    public void OnClickBuyPantButton()
    {
        LevelManager.Instance.player.CurrentPantType = currentSelectPantData.pantType;
        if (playerData.coin >= currentSelectPantData.price)
        {
            playerData.coin -= currentSelectPantData.price;
            playerData.pantList.Add((int)currentSelectPantData.pantType);
            DataManager.Instance.SavePlayerData(playerData);
            LevelManager.Instance.player.EquipPant();
            GameManager.Instance.CoinText.text = DataManager.Instance.CoinData.ToString();
            CheckPantStatus(currentSelectPantData);
        }
    }

    public void OnCLickHatButton(HatData hatData)
    {
        currentSelectHatData = hatData;
        CheckHatStatus(currentSelectHatData);
    }

    public void OnClickEquipHatButton()
    {
        LevelManager.Instance.player.CurrentHatType = currentSelectHatData.hatType;
        LevelManager.Instance.player.EquipHat();
    }

    public void OnClickBuyHatButton()
    {
        LevelManager.Instance.player.CurrentHatType = currentSelectHatData.hatType;
        // Add check money
        if (playerData.coin >= currentSelectHatData.price)
        {
            playerData.coin -= currentSelectHatData.price;
            playerData.hatList.Add((int)currentSelectHatData.hatType);
            DataManager.Instance.SavePlayerData(playerData);
            LevelManager.Instance.player.EquipHat();
            GameManager.Instance.CoinText.text = DataManager.Instance.CoinData.ToString();
            CheckHatStatus(currentSelectHatData);
        }
    }

    public void OnDespawnWeapon()
    {
        LeanPool.Despawn(lastWeapon);
        list.RemoveAt(0);
    }

    public void SpawnWeapon()
    {
        SpawnWeaponShop(listWeapon[weaponIndex].weapon);
    }

    public void NextWeapon()
    {
        if (weaponIndex < 2)
        {
            weaponIndex++;
            WeaponData weaponData = listWeapon[weaponIndex];
            SpawnWeaponShop(weaponData.weapon);
            textWeapon.text = weaponData.weaponType.ToString();
        }
    }

    public void PreviousWeapon()
    {
        if (weaponIndex > 0)
        {
            weaponIndex--;
            WeaponData weaponData = listWeapon[weaponIndex];
            SpawnWeaponShop(weaponData.weapon);
            textWeapon.text = weaponData.weaponType.ToString();
        }
    }

    //private void ChangeButton(Button on, Button off1)
    //{
    //    on.gameObject.SetActive(true);
    //    off1.gameObject.SetActive(false);
    //}

    private void CheckHatStatus(HatData hatData)
    {
        if (playerData.hatList.Contains((int)hatData.hatType))
        {
            if (LevelManager.Instance.player.CurrentHatType == hatData.hatType)
            {
                UIManager.Instance.ButtonSelectHat.gameObject.SetActive(false);
                UIManager.Instance.ButtonBuyHat.gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.ButtonSelectHat.gameObject.SetActive(true);
                UIManager.Instance.ButtonBuyHat.gameObject.SetActive(false);
                UIManager.Instance.ButtonSelectHat.onClick.RemoveAllListeners();
                UIManager.Instance.ButtonSelectHat.onClick.AddListener(OnClickEquipHatButton);

            }

        }
        else
        {
            UIManager.Instance.ButtonSelectHat.gameObject.SetActive(false);
            UIManager.Instance.ButtonBuyHat.gameObject.SetActive(true);
            UIManager.Instance.PriceHatText.text = hatData.price.ToString();
            UIManager.Instance.ButtonBuyHat.onClick.RemoveAllListeners();
            UIManager.Instance.ButtonBuyHat.onClick.AddListener(OnClickBuyHatButton);
        }
    }

    private void CheckPantStatus(PantData pantData)
    {
        if (playerData.pantList.Contains((int)pantData.pantType))
        {
            if (LevelManager.Instance.player.CurrentPantType == pantData.pantType)
            {
                UIManager.Instance.ButtonSelectPant.gameObject.SetActive(false);
                UIManager.Instance.ButtonBuyPant.gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.ButtonSelectPant.gameObject.SetActive(true);
                UIManager.Instance.ButtonBuyPant.gameObject.SetActive(false);
                UIManager.Instance.ButtonSelectPant.onClick.RemoveAllListeners();
                UIManager.Instance.ButtonSelectPant.onClick.AddListener(OnClickEquipPantButton);

            }
        }
        else
        {
            UIManager.Instance.ButtonSelectPant.gameObject.SetActive(false);
            UIManager.Instance.ButtonBuyPant.gameObject.SetActive(true);
            UIManager.Instance.PricePantText.text = pantData.price.ToString();
            UIManager.Instance.ButtonBuyPant.onClick.RemoveAllListeners();
            UIManager.Instance.ButtonBuyPant.onClick.AddListener(OnClickBuyPantButton);
        }
    }

    private void CheckShieldStatus(ShieldData shieldData)
    {
        if (playerData.shieldList.Contains((int)shieldData.shieldType))
        {
            if (LevelManager.Instance.player.CurrentShieldType == shieldData.shieldType)
            {
                UIManager.Instance.ButtonSelectShield.gameObject.SetActive(false);
                UIManager.Instance.ButtonBuyShield.gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.ButtonSelectShield.gameObject.SetActive(true);
                UIManager.Instance.ButtonBuyShield.gameObject.SetActive(false);
                UIManager.Instance.ButtonSelectShield.onClick.RemoveAllListeners();
                UIManager.Instance.ButtonSelectShield.onClick.AddListener(OnClickEquipShieldButton);
            }
        }
        else
        {
            UIManager.Instance.ButtonSelectShield.gameObject.SetActive(false);
            UIManager.Instance.ButtonBuyShield.gameObject.SetActive(true);
            UIManager.Instance.PriceShieldText.text = shieldData.price.ToString();
            UIManager.Instance.ButtonBuyShield.onClick.RemoveAllListeners();
            UIManager.Instance.ButtonBuyShield.onClick.AddListener(OnClickBuyShieldButton);
        }
    }
}
