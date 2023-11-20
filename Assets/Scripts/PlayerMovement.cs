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
    /// ћетод, который каждый фиксированный кадр отслеживает позицию groundCollider и передает еЄ в Vector3 overlapCirclePosition, а 
    /// далее присваивает значение bool переменной isGround в зависимости от того, касаетс€ ли созданный коллайдер (OverlapCircle) объекта 
    /// со слоем Ground.
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
    /// ѕубличный метод движени€, который принимает float значение направлени€ direction и bool значение переменной isJumpButtonPressed из скрипта 
    /// PlayerInput. ≈сли значение isJumpButtonPressed - true - срабатывает метод Jump. ≈сли float переменна€ direction измен€етс€ на более чем 
    /// 0.01 (по модулю), то вызываетс€ метод HorizontalMovement со значением direction из скрипта PlayerInput.
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
    /// ћетод, который в случае isGrounded=true задает движение объекту Rigidbody2D по вертикали (оси y) путем присвоени€ нового вектора с 
    /// силой, указанной в переменной jumpForce (настраиваетс€ в инспекторе). Bool переменна€ isJump передает значение аниматору.
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
    /// ћетод, который принимает переменную direction типа float и задает движение объекту Rigidbody2D по горизонтали с новым вектором направлени€, в зависимости от кривой AnimationCurve по оси х. 
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
