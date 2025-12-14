using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public const string IDLE_STATE = "idle";
    public const string WANDER_STATE = "wander";
    public const string ATTACK_STATE = "attack";

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Rigidbody2D rb;

    [Header("Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private float rangeCanDetect;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private Transform detectpos;

    private State currentState;
    private Transform player;
    //private Room currentRoom;
    private string currentAnim;

    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Transform Player { get => player; set => player = value; }
    //public Room CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public float RangeCanDetect { get => rangeCanDetect; set => rangeCanDetect = value; }
    public SpriteRenderer Spr { get => spr; set => spr = value; }

    protected virtual void Start()
    {
        //EnemyHealth.OnChangeState -= EnemyDead;
        //EnemyHealth.OnChangeState += EnemyDead;
    }
    protected virtual void Update()
    {
        if (currentState != null)
        {
            Debug.Log(currentState);
            currentState.OnUpdate();
        }
    }
    public void EnemyDead()
    {
        //ChangeState(null);
    }
    public void ChangeState(State newState)
    {
        if (newState == null)
            return;
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    public virtual void ChangeDirection(Vector3 newPosition)
    {
        
    }

    public bool DetectPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(detectpos.position, RangeCanDetect, whatIsPlayer);
        if(hit != null)
        {
            player = hit.transform;
            return DetectObstace();
        }
        player = null;
        return false;
    }
    // detect obstace which is between enemy and player;
    public bool DetectObstace()
    {
        if((player.transform.position - detectpos.position).magnitude > distanceToPlayer)
        {
            return false;
        }
        Vector3 direction = (player.transform.position - detectpos.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(detectpos.position,direction, distanceToPlayer, whatIsObstacle);
        if(hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Gizmos
    public virtual void OnDrawGizmos()
    {
        //Detect Player
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectpos.position, RangeCanDetect);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(detectpos.position, Vector2.down * distanceToPlayer);
    }
}
