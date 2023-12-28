using System;
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
    private PlayerControls action;
    private PlayerEnergy playerEnergy;
    private float flip;
    private bool useFire;
    private void Awake()
    {
        action = new PlayerControls();
        playerMove = GetComponent<PlayerMove>();
        playerEnergy = GetComponent<PlayerEnergy>();
    }
    // Start is called before the first frame update
    void Start()
    { 
        CreateWeapon(initialWeapon);
        actions.Interactions.ChangeWeapon.performed += ctx => ChangeWeapon();
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
    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position, Quaternion.identity, weaponPos);
    }

    public bool CanUseWeapon()
    {
        if(currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Gun && playerEnergy.CanUseEnergy)
            return true;
        if(currentWeapon.WeaponData.weaponType == WeaponData.WeaponType.Melee)
            return true;

        return false;

    }
    public void RotateToPlayer(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //if(dir.x > 0f)
        //{
        //    currentWeapon.transform.localScale = Vector3.one;
        //    weaponPos.localScale = Vector3.one;
        //}
        //else

        if (playerMove.Flip)
        {
            weaponPos.localScale = new Vector3(-1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            weaponPos.localScale = new Vector3(1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }


        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);

    }
    private void OnEnable()
    {
        action.Enable();
        action.Weapon.Shoot.performed += _ => StartShooting(); 
        
    }
    private void OnDisable()
    {
        action.Disable();
        action.Weapon.Shoot.performed -= _ => StartShooting();
        
    }
}
