using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector] public BoxCollider2D coll;
    [HideInInspector] public Rigidbody2D rb;
    

    [Foldout("Input")]
    public InputActionReference Horizontal;
    [Foldout("Input")]
    public InputActionReference Vertical;
    [Foldout("Input")]
    public InputActionReference Jump;
    [Foldout("Input")]
    public InputActionReference ReleaseJump;

    [Foldout("Movement")]
    public float Speed;
    [Foldout("Movement")]
    public float ExelerationSpeed;
    [Foldout("Movement"),CurveRange(0, 0, 1, 1)]
    public AnimationCurve ExelerationCurve;
    [Foldout("Movement")]
    public float DecelerationSpeed;
    [Foldout("Movement")]
    public float MaximumDeceleration;
    [Foldout("Movement"),CurveRange(0, 0, 1, 1)]
    public AnimationCurve DecelerationCurve;
    [Foldout("Movement")]
    public float OverrideVelocityDeceleration = .4f;

    [Foldout("Jump")]
    public float jumpForce;
    [Foldout("Jump")]
    public float JumpGravity;
    [Foldout("Jump")]
    public float FallGravity;

    [Foldout("Restrictions")]
    public bool CanFall = true;
    [Foldout("Restrictions")]
    public bool CanMove = true;
    [Foldout("Restrictions")]
    public bool CanJump = true;


    [Foldout("References")]
    public LayerMask jumpableGround;

    [Foldout("info"), ReadOnly()]
    public float ExelerationTimer;
    [Foldout("info"), ReadOnly()]
    public float DecelerationTimer;
    [Foldout("info"), ReadOnly()]
    public Vector2 input;
    [Foldout("info"), ReadOnly()]
    public Vector2 Dir;
    [Foldout("info"), ReadOnly()]
    public Vector2 OverrideVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        ReleaseJump.action.canceled += CancelJump;
    }

    void Update()
    {
        input = new Vector2(Horizontal.action.ReadValue<float>(), Vertical.action.ReadValue<float>());

        SetJumpGravity();

        //decelerates the override by the deceleration amount
        OverrideVelocity *= (1.0f - OverrideVelocityDeceleration * Time.deltaTime);

        rb.velocity = new Vector2(Dir.x * Speed * GetExeleration(input.x) * (CanMove ? 1 : 0), rb.velocity.y) + OverrideVelocity;

        if (Jump.action.ReadValue<float>() == 1 && CanJump && IsGrounded() && CanMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (!CanFall && rb.velocity.y < -.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

    }

    void CancelJump(InputAction.CallbackContext context)
    {
        // returns if the player is falling
        if(rb.velocity.y < -.2f)
        {
            return;
        }

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
    }


    private void SetJumpGravity()
    {
        if(rb.velocity.y < -.2f)
        {
            rb.gravityScale = FallGravity;
        }
        
        if(IsGrounded())
        {
            rb.gravityScale = JumpGravity;
        }
    }

    private float GetExeleration(float input)
    {

        float Exeleration;

        if (input != 0)
        {
            Dir.x = input;


            Exeleration = Mathf.Clamp01(ExelerationCurve.Evaluate(ExelerationTimer));

            ExelerationTimer += Time.deltaTime * ExelerationSpeed;

            DecelerationTimer = 0;
        }
        else
        {
            // only happens once when stopping
            if (ExelerationTimer != 0) DecelerationTimer = Mathf.Clamp(1 - ExelerationTimer,-MaximumDeceleration,float.PositiveInfinity);

            Exeleration = Mathf.Clamp01(DecelerationCurve.Evaluate(DecelerationTimer));

            DecelerationTimer += Time.deltaTime * DecelerationSpeed;

            ExelerationTimer = 0;
        }

        return Exeleration;
    }


    public void SetCanMove(bool Canmove)
    {
        CanMove = Canmove;

        CanFall = Canmove;

        if(Canmove == true)
        {
            ExelerationTimer = 0;
        }
    }

    public void Goto(Transform target)
    {
        transform.position = target.position;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size / 1.1f, 0f, Vector2.down, .1f, jumpableGround);
    }

}
