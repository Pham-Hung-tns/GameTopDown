using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager: MonoBehaviour
{
    public static UIManager Instance;


    [Header("Player UI")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image ArmorBarImage;
    [SerializeField] private Image EnergyBarImage;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI energyText;

    [Header("Fade")]
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateUI();
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
}
