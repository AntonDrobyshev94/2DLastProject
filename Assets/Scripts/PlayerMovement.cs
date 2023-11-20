using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] float jumpForce;
    [SerializeField] private float secondJumpForce;
    [SerializeField] private bool isGrounded = false;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float jumpOffset;
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rgb;
    public bool isJump;
    public bool isSecondJump;
    private int jumpCount;
    [SerializeField, Range (1,3)] private int jumps;
    [SerializeField] private float speed;
    [SerializeField] private float speedRun;
    public bool isSpeedRun;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        isJump = false;
        jumpCount = jumps;
    }

    /// <summary>
    /// �����, ������� ������ ������������� ���� ����������� ������� groundCollider � �������� � � Vector3 overlapCirclePosition, � 
    /// ����� ����������� �������� bool ���������� isGround � ����������� �� ����, �������� �� ��������� ��������� (OverlapCircle) ������� 
    /// �� ����� Ground.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
        if (!isGrounded)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
            isSecondJump = false;
            jumpCount = jumps;
        }
    }

    /// <summary>
    /// ��������� ����� ��������, ������� ��������� float �������� ����������� direction � bool �������� ���������� isJumpButtonPressed �� ������� 
    /// PlayerInput. ���� �������� isJumpButtonPressed - true - ����������� ����� Jump. ���� float ���������� direction ���������� �� ����� ��� 
    /// 0.01 (�� ������), �� ���������� ����� HorizontalMovement �� ��������� direction �� ������� PlayerInput.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="isJumpButtonPressed"></param>
    public void Move(Vector2 moveVector, bool isJumpButtonPressed, bool isSpeedRun)
    {
        if (isJumpButtonPressed)
        {
            Jump();
        }
            
        if (Mathf.Abs(moveVector.x) > 0.01f)
        {
            if(isSpeedRun)
            {
                HorizontalRun(moveVector);
            }
            else
            {
                HorizontalMovement(moveVector);
            }
        }
            
    }

    /// <summary>
    /// �����, ������� � ������ isGrounded=true ������ �������� ������� Rigidbody2D �� ��������� (��� y) ����� ���������� ������ ������� � 
    /// �����, ��������� � ���������� jumpForce (������������� � ����������). Bool ���������� isJump �������� �������� ���������.
    /// </summary>
    private void Jump() 
    {
        if (jumpCount > 0)
        {
            rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);
        }
        jumpCount -=1;
        if (jumpCount ==0 && !isGrounded)
        {
            isSecondJump = true;
            rgb.velocity = new Vector2(rgb.velocity.x, secondJumpForce);
        }
    }

    /// <summary>
    /// �����, ������� ��������� ���������� direction ���� float � ������ �������� ������� Rigidbody2D �� ����������� � ����� �������� �����������, � ����������� �� ������ AnimationCurve �� ��� �. 
    /// </summary>
    /// <param name="direction"></param>
    private void HorizontalMovement(Vector2 moveVector)
    {
        rgb.velocity = new Vector2(curve.Evaluate(moveVector.x)*speed, rgb.velocity.y);
    }

    private void HorizontalRun(Vector2 moveVector)
    {
        rgb.velocity = new Vector2(curve.Evaluate(moveVector.x) * speedRun, rgb.velocity.y);
    }
}
