using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public InputHandler Input { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Abilities Abilities { get; private set; }

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [Header("Jump")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float lastGroundedTime;
    private float lastJumpTime;
    [SerializeField] private float jumpGroundLockoutTime = 0.1f;
    [SerializeField] public float jumpBufferTime = 0.1f;
    [HideInInspector] public float lastJumpPressedTime;
    public bool HasDoubleJumped { get; private set; } = false;

    #region states
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public DashState DashState { get; private set; }
    #endregion

    #region flip variables
    [SerializeField] private float rotationSpeed = 20.0f;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public Quaternion targetRotation = Quaternion.identity;
    private bool rotate;
    private bool rotationClose;

    #endregion

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = new InputHandler();
        Abilities = new Abilities();
        StateMachine = new StateMachine();

        IdleState = new IdleState(this);
        RunState = new RunState(this);
        JumpState = new JumpState(this);
        FallState = new FallState(this);
        DashState = new DashState(this);
    }

    public void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    public void Update()
    {
        Debug.Log(StateMachine.CurrentState);
        Debug.Log("hasDoubleJumped  " + HasDoubleJumped);
        Debug.Log("grounded  " + IsGrounded());
        //Debug.Log(IsGrounded());
        StateMachine.CurrentState.Update();
        Input.Update();

        if (!isFacingRight && Rigidbody.linearVelocity.x > 0f || isFacingRight && Rigidbody.linearVelocity.x < 0)
        {
            Flip();
        }

        rotationClose = Mathf.Abs(transform.eulerAngles.y - targetRotation.eulerAngles.y) <= 1f;

        UpdateGroundedTime();

        if (Input.JumpPressed)
        {
            lastJumpPressedTime = Time.time;
        }
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();

        if (rotate)
            Rotate();
    }

    public bool IsFalling()
    {
        return !IsGrounded() && Rigidbody.linearVelocity.y < 0;
    }

    public bool IsGrounded()
    {
        // groundcheck z buforem na sprawdzenie po skoku
        if (Time.time < lastJumpTime + jumpGroundLockoutTime)
            return false;

        return IsActuallyGrounded();
    }

    public bool IsActuallyGrounded()
    {
        // Metoda OverlapSphere zwraca tablice colliderow, jesli znajdzie jakikolwiek należący do warstwy groundLayer,
        // to postać jest uziemiona.
        return Physics.OverlapSphere(groundCheck.position, 0.1f, groundLayer).Length > 0;
    }
    public void UpdateGroundedTime()
    {
        if (IsActuallyGrounded())
            lastGroundedTime = Time.time;
    }

    public bool IsJumpAllowed()
    {
        return Time.time <= lastGroundedTime + coyoteTime;
    }

    public void ResetDoubleJump()
    {
        HasDoubleJumped = false;
    }

    public void UseDoubleJump()
    {
        HasDoubleJumped = true;
    }

    public bool CanUseDoubleJump()
    {
        return Abilities.CanDoubleJump && !HasDoubleJumped;
    }

    public void Flip()
    {
        targetRotation = isFacingRight ? Quaternion.AngleAxis(179f, transform.up) : Quaternion.AngleAxis(1f, transform.up);
        isFacingRight = !isFacingRight;
        rotate = true;
    }

    public void Rotate()
    {
        if (!isFacingRight)
        {

            if (rotationClose)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                rotate = false;
            }
            else
            {
                Rigidbody.rotation = Quaternion.RotateTowards(Rigidbody.rotation, targetRotation, rotationSpeed);
            }
        }
        else
        {
            if (rotationClose)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                rotate = false;
            }
            else
            {
                Rigidbody.rotation = Quaternion.RotateTowards(Rigidbody.rotation, targetRotation, rotationSpeed);
            }
        }
    }

    public void Move(Vector2 input)
    {
        float moveSpeed = 5f;
        Vector2 velocity = new Vector2(input.x * moveSpeed, Rigidbody.linearVelocity.y);
        Rigidbody.linearVelocity = velocity;
    }

    public void Jump()
    {
        lastJumpTime = Time.time;
        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, jumpForce);
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody.linearVelocity = velocity;
    }
}
