using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Weapon initialWeapon;
    [SerializeField] Transform weaponPos;

    private Weapon currentWeapon;
    private PlayerMove playerMove;
    private PlayerControls actions;
    private PlayerEnergy playerEnergy;
    private float flip;
    private bool useFire;

    private Coroutine weaponCoroutine;
    private ItemText weaponNameText;

    private int weaponIndex; // 0 - 1
    private Weapon[] equippedWeapons = new Weapon[2];
    private void Awake()
    {
        actions = new PlayerControls();
        playerMove = GetComponent<PlayerMove>();
        playerEnergy = GetComponent<PlayerEnergy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        actions.Interaction.ChangeItem.performed += ctx => ChangeWeapon();
    }

    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position,
            Quaternion.identity, weaponPos);
        equippedWeapons[weaponIndex] = currentWeapon;
        ShowCurrentWeaponName();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapons[0] == null)
        {
            CreateWeapon(weapon);
            return;
        }

        if (equippedWeapons[1] == null)
        {
            weaponIndex++;
            equippedWeapons[0].gameObject.SetActive(false);
            CreateWeapon(weapon);
            return;
        }

        // Destroy current weapon
        currentWeapon.DestroyWeapon();
        equippedWeapons[weaponIndex] = null;

        // Create new weapon
        CreateWeapon(weapon);
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
    }


    //private void StopShooting()
    //{
    //    if (!action.Weapon.Shoot.ReadValue<float>().Equals(false))
    //    {
    //        Debug.Log(action.Weapon.Shoot.ReadValue<float>());
    //        return;
    //    }
    //    Debug.Log(action.Weapon.Shoot.ReadValue<float>());

    //    CancelInvoke("ShootWeapon");
    //}

    public void StartShooting()
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

    private void Update()
    {
        if (playerMove.MoveDirection != Vector2.zero)
        {
            RotateToPlayer(playerMove.MoveDirection);
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
        if(currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Gun && playerEnergy.CanUseEnergy)
            return true;
        if(currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Melee)
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
    public void RotateToPlayer(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dir.x > 0f) // Facing Right
        {
            weaponPos.localScale = Vector3.one;
            currentWeapon.transform.localScale = Vector3.one;
        }
        else // Facing Left
        {
            weaponPos.localScale = new Vector3(-1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(-1, -1, 1);
        }

        currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, angle);

    }
    private void OnEnable()
    {
        actions.Enable();
        actions.Weapon.Shoot.performed += _ => StartShooting(); 
        
    }
    private void OnDisable()
    {
        actions.Disable();
        actions.Weapon.Shoot.performed -= _ => StartShooting();
        
    }
}
