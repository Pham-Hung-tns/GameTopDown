using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [Header("Config")]
    [SerializeField] private PlayerCreate[] playerCreates;

    [Header("UI and Stats")]
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI Story;
    [SerializeField] private TextMeshProUGUI maxHealthStat;
    [SerializeField] private TextMeshProUGUI maxEnergyStat;
    [SerializeField] private TextMeshProUGUI maxArmorStat;

    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinUI;
    [SerializeField] private TextMeshProUGUI unlockCharacterText;
    [SerializeField] private TextMeshProUGUI upgradeCharacterText;
    private SelectCharacter currentPlayer;
    private bool playerSelected;

    [Header("Button")]
    [SerializeField] Button unlockButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] Button chooseButton;
    [SerializeField] GameObject moveButton;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        coinUI.text = CoinManager.Instance.totalCoins.ToString();
        CreateCharactersInScene();
    }

    private void CreateCharactersInScene()
    {
        foreach (PlayerCreate character in playerCreates)
        {
            PlayerMove player = Instantiate(character.Character,
                character.initialPosition.position,
                Quaternion.identity,
                character.initialPosition);

            // player cant not move
            player.enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }


    // Select character
    public void EnableMovement()
    {
        if (playerSelected) return;

        GameManager.Instance.playerPrefab = currentPlayer.PlayerConfig;
        playerPanel.SetActive(false);
        currentPlayer.GetComponent<PlayerMove>().enabled = true;
        currentPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerSelected = true;
        moveButton.SetActive(true);
    }

    // show stat of current player
    public void ShowStats(SelectCharacter player)
    {
        currentPlayer = player;

        playerPanel.SetActive(true);
        icon.sprite = currentPlayer.PlayerConfig.Icon;
        playerName.text = currentPlayer.PlayerConfig.Name;
        currentLevel.text = $"Level {currentPlayer.PlayerConfig.Level}";

        if (!currentPlayer.PlayerConfig.unlock)
        {
            upgradeButton.gameObject.SetActive(false);
            chooseButton.interactable = false;
            unlockButton.gameObject.SetActive(true);
            unlockCharacterText.text = $"Unlock\n({currentPlayer.PlayerConfig.unlockCost.ToString()})";
        }
        else
        {
            upgradeButton.gameObject.SetActive(true);
            chooseButton.interactable = true;
            unlockButton.gameObject.SetActive(false);
            upgradeCharacterText.text = $"Upgrade\n({currentPlayer.PlayerConfig.upgradeCost.ToString()})";
        }
        ResetStat();
    }


    public void UnLockCharacter()
    {
        if (CoinManager.Instance.totalCoins >= currentPlayer.PlayerConfig.unlockCost)
        {
            unlockButton.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(true);
            upgradeCharacterText.text = $"Upgrade\n({currentPlayer.PlayerConfig.upgradeCost.ToString()})";
            chooseButton.interactable = true;
            ResetStat();
            CoinManager.Instance.RemoveCoin(currentPlayer.PlayerConfig.unlockCost);
            //// cap nhat so luong coin
            coinUI.text = CoinManager.Instance.totalCoins.ToString();
            // mo khoa nhan vat
            currentPlayer.PlayerConfig.unlock = true;
        }
    }

    public void UpgradeCharacter()
    {
        if (CoinManager.Instance.totalCoins >= currentPlayer.PlayerConfig.upgradeCost)
        {
            CoinManager.Instance.RemoveCoin(currentPlayer.PlayerConfig.upgradeCost);
            ////cap nhat so luong coin
            coinUI.text = CoinManager.Instance.totalCoins.ToString();
            UpgradeCharacterStats();
        }
    }

    public void UpgradeCharacterStats()
    {
        PlayerConfig config = currentPlayer.PlayerConfig;
        config.Level++;
        config.MaxHealth++;
        config.MaxEnergy += 10;
        config.MaxArmor++;
        config.upgradeCost = Mathf.RoundToInt(config.upgradeCost + config.upgradeCost * (config.upgradeCostPercent / 100f));
        upgradeCharacterText.text = $"Upgrade\n({config.upgradeCost.ToString()})";
        ResetStat();
    }
    public void ResetStat()
    {
        currentLevel.text = $"Level {currentPlayer.PlayerConfig.Level.ToString()}";
        maxHealthStat.text = $"{currentPlayer.PlayerConfig.MaxHealth.ToString()}";
        maxEnergyStat.text = $"{currentPlayer.PlayerConfig.MaxEnergy.ToString()}";
        maxArmorStat.text = $"{currentPlayer.PlayerConfig.MaxArmor.ToString()}";
    }

    public void Back()
    {
        playerPanel.SetActive(false);
    }
    private void Update()
    {
        //coinUI.text = CoinManager.Instance.totalCoins.ToString();
    }
}

[Serializable]
public class PlayerCreate
{
    public PlayerMove Character;
    public Transform initialPosition;
}

