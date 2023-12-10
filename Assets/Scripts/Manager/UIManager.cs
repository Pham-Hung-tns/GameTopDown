using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerConfig playerConfig;

    [Header("Player UI")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image ArmorBarImage;
    [SerializeField] private Image EnergyBarImage;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI energyText;

    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
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

}
