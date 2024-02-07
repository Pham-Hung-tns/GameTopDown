using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb2D;
    Animator animator;
    private PlayerControls actions;
    private Vector2 moveDirection;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashWaitTime;
    [SerializeField] float transperency;
    private SpriteRenderer spriteRenderer;
    private bool usingDash;
    private bool cooldown;
    private float currentSpeed;
    public Vector3 lastPos;

    public Vector2 MoveDirection => moveDirection;
    public bool Flip => spriteRenderer.flipX;
    void Awake()
    {
        actions = new PlayerControls();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        currentSpeed = moveSpeed;
        actions.Movement.Skill.performed += context => Dash();
    }


    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + moveDirection.normalized * (currentSpeed * Time.fixedDeltaTime));
        animator.SetBool("moveMotion", moveDirection != Vector2.zero);

    }

    private void Update()
    {
        ClaimInput();
        RotationPlayer();
        if(cooldown)
        {
            UIManager.Instance.CoolDownSkill(dashWaitTime);
        }
    }

    public void ClaimInput()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>();
      
    }

    public void FacingRightDirection()
    {
        spriteRenderer.flipX = false;
    }
    private void Dash()
    {
        if (usingDash)
            return;

        usingDash = true;
        StartCoroutine(IEDash());
    }

    private IEnumerator IEDash()
    {
        ModifyColorForDash(transperency);
        currentSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        currentSpeed = moveSpeed;
        ModifyColorForDash(1f);
        cooldown = true;
        StartCoroutine(ControlDash());
    }
    IEnumerator ControlDash()
    {
        yield return new WaitForSeconds(dashWaitTime);
        usingDash = false;
        cooldown = false;
    }
    private void ModifyColorForDash(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }

    private void RotationPlayer()
    {
        if (moveDirection.x >= 0.01f)
            spriteRenderer.flipX = false;
        else if (moveDirection.x < 0f)
            spriteRenderer.flipX = true;
    }
}
