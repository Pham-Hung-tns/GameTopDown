using System;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : CharacterWeapon
{
    public static event Action<Weapon> OnShowUIWeaponEvent;
    [SerializeField] Weapon initialWeapon;

    [SerializeField] private PlayerConfig playerConfig;

    private PlayerMove playerMove;
    private PlayerControls actions;
    private PlayerEnergy playerEnergy;

    private Coroutine weaponCoroutine;
    private ItemText weaponNameText;

    private DetectionEnemy detection;
    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerControls();
        playerMove = GetComponent<PlayerMove>();
        playerEnergy = GetComponent<PlayerEnergy>();
        detection = GetComponentInChildren<DetectionEnemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //CreateWeapon(initialWeapon);
    }

    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position,
            Quaternion.identity, weaponPos);
        equippedWeapons[weaponIndex] = currentWeapon;
        equippedWeapons[weaponIndex].Character = this;
        ShowCurrentWeaponName();
        OnShowUIWeaponEvent?.Invoke(currentWeapon);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapons[0] == null)
        {
            CreateWeapon(weapon);
            ResetWeaponForChange();
            return;
        }

        if (equippedWeapons[1] == null)
        {
            weaponIndex++;
            equippedWeapons[0].gameObject.SetActive(false);
            CreateWeapon(weapon);
            ResetWeaponForChange();
            return;
        }

        // Destroy current weapon
        currentWeapon.DestroyWeapon();
        equippedWeapons[weaponIndex] = null;

        // Create new weapon
        CreateWeapon(weapon);
        ResetWeaponForChange();
    }

    private void ChangeWeapon()
    {
        if (equippedWeapons[1] == null) return;
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            equippedWeapons[i].gameObject.SetActive(false);
        }

        weaponIndex = 1 - weaponIndex;
        currentWeapon = equippedWeapons[weaponIndex];
        currentWeapon.gameObject.SetActive(true);
        ResetWeaponForChange();
        ShowCurrentWeaponName();
        OnShowUIWeaponEvent?.Invoke(currentWeapon);
    }


    private void StopShooting()
    {
        if (actions.Weapon.Shoot.ReadValue<float>().Equals(0f))
        {
            CancelInvoke(nameof(Shoot));
            return;
        }
    }

    public void StartShooting()
    {
        InvokeRepeating(nameof(Shoot), 0f, currentWeapon.WeaponData.timeBetweenAttacks);
    }
    public void Shoot()
    {
        if (currentWeapon == null)
            return;
        if (CanUseWeapon())
        {
            currentWeapon.UseWeapon();
            playerEnergy.UseEnergy(currentWeapon.WeaponData.energy);
        }
        else
            return;
    }
    public float GetDamageCritical()
    {
        float damage = currentWeapon.WeaponData.damage;
        float per = UnityEngine.Random.Range(0, 100f);
        if (per <= playerConfig.CriticalChance)
        {
            damage = Mathf.RoundToInt(damage + (playerConfig.CriticalDamage / 100) * damage);
            return damage;
        }
        return damage;
    }
    private void Update()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        if (playerMove.MoveDirection != Vector2.zero && currentWeapon != null)
        {
            RotateWeapon(playerMove.MoveDirection);
        }

        if (detection != null && detection.EnemyTarget != null)
        {
            Vector3 dirToEnemy = detection.EnemyTarget.transform.position - transform.position;
            RotateWeapon(dirToEnemy);
        }
    }
    private void ShowCurrentWeaponName()
    {
        if (weaponCoroutine != null)
        {
            StopCoroutine(weaponCoroutine);
        }

        if (weaponNameText != null && weaponNameText.gameObject.activeInHierarchy)
        {
            Destroy(weaponNameText.gameObject);
        }

        weaponCoroutine = StartCoroutine(IEShowName());
    }

    private IEnumerator IEShowName()
    {
        Vector3 textPos = transform.position + Vector3.up;
        Color weaponNameColor = GameManager.Instance.
            ChooseColorForWeapon(currentWeapon.WeaponData);
        weaponNameText = ItemTextManager.Instance
            .ShowName(currentWeapon.WeaponData.name, weaponNameColor,
                textPos);
        weaponNameText.transform.SetParent(transform);
        yield return new WaitForSeconds(2f);
        Destroy(weaponNameText.gameObject);
    }
    public bool CanUseWeapon()
    {
        if (currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Gun && playerEnergy.CanUseEnergy)
            return true;
        if (currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Melee)
            return true;

        return false;

    }
    private void ResetWeaponForChange()
    {
        Transform weaponTransform = currentWeapon.transform;
        weaponTransform.rotation = Quaternion.identity;
        weaponTransform.localScale = Vector3.one;
        weaponPos.rotation = Quaternion.identity;
        weaponPos.localScale = Vector3.one;
        playerMove.FacingRightDirection();
    }

    private void OnEnable()
    {
        actions.Enable();
        actions.Interaction.ChangeItem.performed += ctx => ChangeWeapon();
        actions.Weapon.Shoot.performed += _ => StartShooting();
        actions.Weapon.Shoot.canceled += _ => StopShooting();

    }
    private void OnDisable()
    {
        actions.Disable();
        actions.Interaction.ChangeItem.performed += ctx => ChangeWeapon();
        actions.Weapon.Shoot.performed -= _ => StartShooting();
        actions.Weapon.Shoot.canceled -= _ => StopShooting();
    }
}
