using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private EnemyVitality enemyVitality;
    [SerializeField] private EnemyWeapon enemyWeapon;
    [SerializeField] private EnemyConfig enemyConfig;

    // List of states configured in the Inspector
    [SerializeField] private List<AIState> states = new List<AIState>();

    public AIState currentState;
    private Transform player;
    //private Room currentRoom;
    
    private string currentAnim;
    private float currentTime = 0f;
    private float timeLimit = 0f;
    private Vector3 patrolPosition = Vector3.zero;
    private bool canAttack = false;

    
    public EnemyWeapon EnemyWeapon { get => enemyWeapon; set => enemyWeapon = value; }
    public EnemyConfig EnemyConfig { get => enemyConfig; set => enemyConfig = value; }
    public Vector3 PatrolPosition { get => patrolPosition; set => patrolPosition = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public float TimeLimit { get => timeLimit; set => timeLimit = value; }
    public Transform Player { get => player; set => player = value; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }

    private void Awake()
    {
        enemyVitality.Initialized(enemyConfig);
    }
    private void Start()
    {
        // Initialize all states assigned via the Inspector
        if (states != null)
        {
            for (int i = 0; i < states.Count; i++)
            {
                var s = states[i];
                if (s != null)
                    s.Initialize(this);
            }
        }

        // Ensure current state is initialized even if not in the list
        if (currentState != null)
            currentState.Initialize(this);

        // Ensure initial state's enter lifecycle is invoked if a state was set in the inspector
        if (currentState != null)
        {
            currentState.EnterState();
            CurrentTime = 0f;
        }
        if(enemyConfig != null && enemyConfig.initialWeapon != null)
            enemyWeapon.CreateWeapon(enemyConfig.initialWeapon);
    }

    private void Update()
    {
        currentState?.UpdateState();
        CurrentTime += Time.deltaTime;

        // check if see player to rotate weapon
        //if (LevelManager.Instance.SelectedPlayer == null) { return; }
        //Vector3 dir = LevelManager.Instance.SelectedPlayer.transform.position - transform.position;
        //RotateWeapon(dir);
    }

    public void ChangeToState(AIState nextState)
    {
        var previous = currentState;
        if (nextState == previous)
            return;

        // Exit previous if any
        previous?.ExitState();

        // If no next provided, stay in previous state (remain)
        if (nextState == null)
        {
            currentState = previous;
            return;
        }

        // Assign and enter new state
        currentState = nextState;
        CurrentTime = 0f; // reset state timer
        currentState.EnterState();
        Debug.Log("Current State: " + currentState);
    }

    public void ChangeAnim(string animatorName)
    {
        if (currentAnim != animatorName)
        {
            animator.ResetTrigger(animatorName);
            currentAnim = animatorName;
            animator.SetTrigger(currentAnim);
        }
    }
    // Move enemy to new direction, flip sprite and rotate weapon
    public void ChangeDirection(Vector3 newPosition)
    {
        Vector3 dir = newPosition - rigidBody2D.transform.position;
        if (dir.x < 0)
        {
            Spr.flipX = true;
        }
        else
        {
            Spr.flipX = false;
        }
        if (enemyConfig.initialWeapon != null)
            EnemyWeapon.RotateWeaponToPlayer(dir);
    }

    protected override void OnAttack(bool canAttack)
    {
        base.OnAttack(canAttack);
    }

    protected override void OnSkill(bool canUseSkill)
    {
        base.OnSkill(canUseSkill);
    }

    override protected void OnMove(Vector2 input)
    {
        base.OnMove(input);
    }

    // Gizmos
    public void OnDrawGizmos()
    {
        //Detect Player
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyConfig.rangeCanDetectPlayer);
    }
}
