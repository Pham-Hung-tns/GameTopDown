using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerVitality))]
[RequireComponent(typeof(PlayerWeapon))]
//[RequireComponent(typeof(PlayerSkill))]

public class PlayerController : CharacterController
{
    [Header("Dependencies")]
    [SerializeField] private InputReader inputReader; // Kéo file SO vào đây
    [SerializeField] private PlayerConfig playerData; // Kéo file Data vào đây

    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerVitality _vitality;
    [SerializeField] private PlayerWeapon _combat;
    [SerializeField] private DetectionEnemy _detection;
    //private PlayerSkill _skill;

    private Vector2 _moveInput;

    public PlayerConfig PlayerData { get => playerData; set => playerData = value; }

    private void Awake()
    {
        //_movement = GetComponent<PlayerMovement>();
        //_vitality = GetComponent<PlayerVitality>();
        //_combat = GetComponent<PlayerWeapon>();
        //_detection = GetComponentInChildren<DetectionEnemy>();
        //_skill = GetComponent<PlayerSkill>();

        // Dependency Injection: Đẩy dữ liệu vào các module con
        _movement.Initialize(rigidBody2D,animator, Spr, PlayerData);
        _vitality.Initialize(PlayerData);
        _combat.Initialize(PlayerData, Spr, _vitality, _detection);
        // _skill.Initialize(...);
    }

    private void Start()
    {
        if(PlayerData.initialWeapon != null)
            _combat.CreateWeapon(PlayerData.initialWeapon);
    }

    private void Update()
    {
        // Controller ra lệnh cho Movement module di chuyển mỗi khung hình
        _movement.CalculateSpeed(_moveInput);
        _movement.RotationPlayer(_moveInput);

        _combat.RotateWeapon();
    }

    private void FixedUpdate()
    {
        _movement.HandleMovement(_moveInput);
    }

    // --- Event Handlers ---

    protected override void OnMove(Vector2 direction)
    {
        _moveInput = direction;
        _combat.MovementDirection = direction;
        Debug.Log(_moveInput);
    }

    protected override void OnAttack(bool canAttack)
    {
        // Logic về năng lượng sẽ tính trong PlayerWeapon. Tùy thuộc vào số năng lượng tiêu hao của từng loại vũ khí
        if (canAttack == true)
        {
            
            _combat.StartShooting();
        }
        else
        {
            _combat.StopShooting();
        }
    }

    protected override void OnSkill(bool canUseSkill)
    {
        // Logic tương tự cho Skill
    }

    private void OnEnable()
    {
        // Đăng ký nhận lệnh từ Input Reader
        inputReader.MoveEvent += OnMove;
        inputReader.AttackEvent += OnAttack;
        inputReader.SkillEvent += OnSkill;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.AttackEvent -= OnAttack;
        inputReader.SkillEvent -= OnSkill;
    }
}
