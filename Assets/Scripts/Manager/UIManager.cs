using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEditor.Rendering;
using System;

public class UIManager: Singleton<UIManager>
{

    [Header("Player UI")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image ArmorBarImage;
    [SerializeField] private Image EnergyBarImage;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI energyText;

    [Header("Fade")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Level and Room Completed")]
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI completedText;

    [Header("UI Weapon")]
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private TextMeshProUGUI energyConsumptionText;
    [SerializeField] private Image weaponImage;

    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinUI;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Fire Button")]
    [SerializeField] private GameObject fireButton;
    
    [Header("Pickup Button")]
    [SerializeField] private GameObject pickupButton;

    [Header("Use Skill Button")]
    public Image useSkillButton;

    [Header("UI Boss")]
    public GameObject HealthBossPanel;
    public Image HealthUIBoss;
    protected override void Awake()
    {
        base.Awake();
    }

    

    private void Update()
    {
        UpdateUI();
        coinUI.text = CoinManager.Instance.totalCoins.ToString("0.00");

    }

    private void UpdateUI()
    {
        PlayerConfig playerConfig = GameManager.Instance.playerPrefab;
        healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount
            , playerConfig.currentHealth / playerConfig.MaxHealth
            , 10 * Time.deltaTime);
        ArmorBarImage.fillAmount = Mathf.Lerp(ArmorBarImage.fillAmount
            , playerConfig.currentArmor / playerConfig.MaxArmor
            , 10 * Time.deltaTime);
        EnergyBarImage.fillAmount = Mathf.Lerp(EnergyBarImage.fillAmount
            , playerConfig.currentEnergy / playerConfig.MaxEnergy
            , 10 * Time.deltaTime);

        healthText.text = playerConfig.currentHealth + "/" + playerConfig.MaxHealth;
        armorText.text = playerConfig.currentArmor + "/" + playerConfig.MaxArmor;
        energyText.text = playerConfig.currentEnergy + "/" + playerConfig.MaxEnergy;
    }

    public void FadeNewDungeon(float value)
    {
        StartCoroutine(Helper.IEFade(canvasGroup, value, 1.5f));
    }

    public void UpdateLevelText(string currentLevel)
    {
        currentLevelText.text = currentLevel;
    }

    public void RoomCompleted()
    {
        StartCoroutine(IERoomCompleted());
    }

    private IEnumerator IERoomCompleted()
    {
        completedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        completedText.gameObject.SetActive(false);
    }

    private void ShowUIWeapon(Weapon weapon)
    {
        if (weaponPanel.active == false)
        {
            weaponPanel.active = true;
        }
        weaponImage.sprite = weapon.WeaponData.icon;
        energyConsumptionText.text = weapon.WeaponData.energy.ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ShowPickupButton(bool isActive)
    {
        pickupButton.SetActive(isActive);
        fireButton.SetActive(!isActive);
        
    }

    public void CoolDownSkill(float cooldown)
    {
        useSkillButton.fillAmount -= 1 / cooldown * Time.deltaTime;
        if(useSkillButton.fillAmount <= 0)
        {
            useSkillButton.fillAmount = 1;
        }
    }
    private void ShowHealthUIBoss(float amount)
    {
        HealthUIBoss.fillAmount = amount;
        HealthBossPanel.SetActive(true);
    }

    
    private void OnEnable()
    {
        LevelManager.OnRoomCompleted += RoomCompleted;
        LevelManager.OnPlayerInRoomBoss += ShowHealthUIBoss;
        PlayerWeapon.OnShowUIWeaponEvent += ShowUIWeapon;
        PlayerHealth.OnPlayerDeathEvent += ShowGameOverPanel;
    }


    private void OnDisable()
    {
        LevelManager.OnRoomCompleted -= RoomCompleted;
        LevelManager.OnPlayerInRoomBoss -= ShowHealthUIBoss;
        PlayerWeapon.OnShowUIWeaponEvent -= ShowUIWeapon;
        PlayerHealth.OnPlayerDeathEvent -= ShowGameOverPanel;
    }
}
