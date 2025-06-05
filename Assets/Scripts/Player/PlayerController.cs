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

    #region states
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    #endregion

    #region flip variables
    [SerializeField] private float rotationSpeed = 20.0f;
    private Vector3 rotationSpeedV;
    private bool isFacingRight = true;
    private Quaternion targetRotation = Quaternion.identity;
    private bool rotate;
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
    }

    public void Start()
    {
        StateMachine.Initialize(IdleState);
        rotationSpeedV = new Vector3(0, rotationSpeed, 0);
    }

    public void Update()
    {
        Debug.Log(StateMachine.CurrentState);
        StateMachine.CurrentState.Update();
        Input.Update();
        if (!isFacingRight && Rigidbody.linearVelocity.x > 0f || isFacingRight && Rigidbody.linearVelocity.x < 0)
        {
            //Debug.Log("flip_con");
            Flip();
        }
    }
    public void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();

        if (rotate)
            Rotate();
    }
    public bool IsGrounded()
    {
        // Metoda OverlapSphere zwraca tablice colliderow, jesli znajdzie jakikolwiek należący do warstwy groundLayer,
        // to postać jest uziemiona.
        return Physics.OverlapSphere(groundCheck.position, 0.3f, groundLayer).Length > 0;
    }

    public void Flip()
    {
        //Debug.Log("flip");
        targetRotation = isFacingRight ? Quaternion.AngleAxis(180f, transform.up) : Quaternion.AngleAxis(0f, transform.up);
        isFacingRight = !isFacingRight;
        rotate = true;
    }

    public void Rotate()
    {

        //Debug.Log("rotate");
        //Debug.Log("transformRotation: " + transform.rotation);
        //Debug.Log("targetRotation: " + targetRotation);

        if (Mathf.Abs(transform.eulerAngles.y - targetRotation.eulerAngles.y) <= 1f)
        {
            //Debug.Log("if");
            transform.rotation = targetRotation;
            rotate = false;
        }
        else if (!isFacingRight)
        {
            //Debug.Log("if else");

            Quaternion deltaRotation = Quaternion.Euler(rotationSpeedV);
            Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
        }
        else
        {
            //Debug.Log("else");

            Quaternion deltaRotation = Quaternion.Euler(-rotationSpeedV);
            Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
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
        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, jumpForce);
    }
}
