using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean.Pool;

public class UIManager : Singleton<UIManager>
{
    private float countDown = 5f;

    [SerializeField] private Text countDownText;
    [SerializeField] private GameObject circleCountDown;

    [SerializeField] private GameObject menuStartCanvas;
    [SerializeField] private GameObject weaponShopCanvas;
    [SerializeField] private GameObject clotheShopCanvas;
    [SerializeField] private GameObject hatShopCanvas;
    [SerializeField] private GameObject pantShopCanvas;
    [SerializeField] private GameObject shieldShopCanvas;
    [SerializeField] private GameObject loseCanvas;

    [SerializeField] private Button buttonClickPlay;
    [SerializeField] private Button buttonClickClotheShop;

    [SerializeField] private Button buttonClickWeaponShop;
    [SerializeField] private Button buttonCloseWeaponShop;
    [SerializeField] private Button buttonNextWeapon;
    [SerializeField] private Button buttonPreWeapon;
    [SerializeField] private Button buttonEquipWeapon;

    [SerializeField] private Button buttonClickHatShop;
    [SerializeField] private Button buttonCloseClothesShop;
    [SerializeField] private Button buttonSelectHat;
    [SerializeField] private Button buttonBuyHat;
    [SerializeField] private Text priceHatText;

    [SerializeField] private Button buttonClickPantShop;
    [SerializeField] private Button buttonSelectPant;
    [SerializeField] private Button buttonBuyPant;
    [SerializeField] private Text pricePantText;

    [SerializeField] private Button buttonClickShieldShop;
    [SerializeField] private Button buttonSelectShield;
    [SerializeField] private Button buttonBuyShield;
    [SerializeField] private Text priceShieldText;

    [SerializeField] private Button buttonReloadGame;

    private ShopManager shopManager;
    private Player player;

    public Button ButtonSelectHat { get => buttonSelectHat; set => buttonSelectHat = value; }
    public Button ButtonBuyHat { get => buttonBuyHat; set => buttonBuyHat = value; }
    public Button ButtonSelectPant { get => buttonSelectPant; set => buttonSelectPant = value; }
    public Button ButtonBuyPant { get => buttonBuyPant; set => buttonBuyPant = value; }
    public Button ButtonBuyShield { get => buttonBuyShield; set => buttonBuyShield = value; }
    public Text PriceShieldText { get => priceShieldText; set => priceShieldText = value; }
    public Text PricePantText { get => pricePantText; set => pricePantText = value; }
    public Text PriceHatText { get => priceHatText; set => priceHatText = value; }
    public Button ButtonSelectShield { get => buttonSelectShield; set => buttonSelectShield = value; }

    private void OnEnable()
    {
        buttonClickPlay.onClick.AddListener(OnClickPlayButton);

        buttonClickWeaponShop.onClick.AddListener(OnClickWeaponShopButton);
        buttonCloseWeaponShop.onClick.AddListener(OnCloseWeaponShop);
        buttonNextWeapon.onClick.AddListener(PressNextWeaponShop);
        buttonPreWeapon.onClick.AddListener(PressPrevWeaponShop);
        buttonEquipWeapon.onClick.AddListener(PressEquipWeaponShop);

        buttonReloadGame.onClick.AddListener(RevivePlayer);

        buttonClickHatShop.onClick.AddListener(OnClickHatShop);
        ButtonSelectHat.onClick.AddListener(OnClickSelectHatButton);

        buttonClickPantShop.onClick.AddListener(OnClickPantShop);
        ButtonSelectPant.onClick.AddListener(OnSelectPantButton);

        buttonClickShieldShop.onClick.AddListener(OnClickShieldShop);
        ButtonSelectShield.onClick.AddListener(OnSelectShieldShop);

        buttonCloseClothesShop.onClick.AddListener(OnCloseClothesShop);
        buttonClickClotheShop.onClick.AddListener(OnClickClotheShop);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private IEnumerator Countdown()
    {
        while (countDown > 0)
        {
            countDownText.text = ((int)countDown).ToString();
            circleCountDown.transform.Rotate(360f * Time.deltaTime * Vector3.back);
            countDown -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        countDownText.text = "0";
        countDown = 5f;
    }

    public void OnClickPlayButton()
    {
        Time.timeScale = 1;
        menuStartCanvas.SetActive(false);
        CameraFollow.Instance.SmoothCameraChange();
    }

    public void OnClickWeaponShopButton()
    {
        menuStartCanvas.SetActive(false);
        weaponShopCanvas.SetActive(true);
        ShopManager.Instance.SpawnWeapon();
    }

    public void OnCloseWeaponShop()
    {
        ShopManager.Instance.OnDespawnWeapon();
        menuStartCanvas.SetActive(true);
        weaponShopCanvas.SetActive(false);
    }

    public void OnLoseUI()
    {
        loseCanvas.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Countdown());
    }

    public void PressNextWeaponShop()
    {
        if (ShopManager.Instance.weaponIndex < 2)
        {
            ShopManager.Instance.OnDespawnWeapon();
        }
        ShopManager.Instance.NextWeapon();
    }

    public void PressPrevWeaponShop()
    {
        if (ShopManager.Instance.weaponIndex > 0)
        {
            ShopManager.Instance.OnDespawnWeapon();
        }
        ShopManager.Instance.PreviousWeapon();
    }

    public void PressEquipWeaponShop()
    {
        Destroy(LevelManager.Instance.player.WeponSpawn.gameObject);
        LevelManager.Instance.player.EquipWeapon();
    }

    public void OnClickClotheShop ()
    {
        menuStartCanvas.SetActive(false);
        clotheShopCanvas.SetActive(true);
        hatShopCanvas.SetActive(true);
        pantShopCanvas.SetActive(false);
        shieldShopCanvas.SetActive(false);
        ShopManager.Instance.SpawnHatShop();
        ShopManager.Instance.SpawnPantShop();
        ShopManager.Instance.SpawnShieldShop();
        //LevelManager.Instance.player.IsDance = true;
    }

    public void OnClickHatShop()
    {
        menuStartCanvas.SetActive(false);
        hatShopCanvas.SetActive(true);
        pantShopCanvas.SetActive(false);
        shieldShopCanvas.SetActive(false);
    }

    public void OnCloseClothesShop()
    {
        menuStartCanvas.SetActive(true);
        hatShopCanvas.SetActive(false);
        clotheShopCanvas.SetActive(false);
        shieldShopCanvas.SetActive(false);
        //ShopManager.Instance.OnDespawnHatShop();
    }

    public void OnClickSelectHatButton()
    {
        ShopManager.Instance.OnClickEquipHatButton();
    }

    public void OnClickPantShop()
    {
        menuStartCanvas.SetActive(false);
        hatShopCanvas.SetActive(false);
        shieldShopCanvas.SetActive(false);
        pantShopCanvas.SetActive(true);
        //ShopManager.Instance.SpawnPantShop();
    }

    public void OnSelectPantButton ()
    {
        ShopManager.Instance.OnClickEquipPantButton();
    }

    public void OnClickShieldShop()
    {
        menuStartCanvas.SetActive(false);
        hatShopCanvas.SetActive(false);
        pantShopCanvas.SetActive(false);
        shieldShopCanvas.SetActive(true);
    }

    public void OnSelectShieldShop()
    {
        ShopManager.Instance.OnClickEquipShieldButton();
    }

    public void RevivePlayer()
    {
        //SceneManager.LoadScene(CacheString.SCENE_NAME);
        LevelManager.Instance.player.OnRevive();
        loseCanvas.SetActive(false);
    }

    
}
