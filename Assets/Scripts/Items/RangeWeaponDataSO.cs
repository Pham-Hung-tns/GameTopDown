using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Items/Range Weapon")]
public class RangeWeaponDataSO : WeaponDataSO
{

    [Header("Projectile Info")]
    public GameObject projectilePrefab; // Viên đạn
    public float projectileSpeed = 15f;
    public float projectileLifetime = 5f;

    [Header("Shooting Pattern")]
    public int projectileCount = 1; // Số lượng đạn bắn ra 1 lúc (Shotgun = 5)
    [Range(0, 360)] public float spreadAngle = 0f; // Góc tản đạn (Shotgun = 45 độ)

    [Header("Charge Mechanics")]
    public bool canCharge; // Có cần giữ chuột để tụ lực không?
    public float maxChargeTime = 2f; // Thời gian tụ lực tối đa
    public float minChargeDamageMultiplier = 0.5f; // Sát thương khi chưa tụ lực
    public float maxChargeDamageMultiplier = 2.0f; // Sát thương khi tụ lực full
}
