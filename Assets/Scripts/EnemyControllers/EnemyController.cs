using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform groundColliderTransform;
    public Rigidbody2D rgb;
    [SerializeField] private DamageDeallerEnemy damageDeallerEnemy;
    [SerializeField] private EnemyShooter enemyShooter;
    [SerializeField] private Health health;
    [SerializeField] private GameObject enemyAttacktrigger;
    [SerializeField] private GameObject gun;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private bool isCollisionPlayer;
    [SerializeField] private bool isActionActive;
    [SerializeField] private bool startAttack;
    public bool isGrounded;
    public bool isShooting;
    public bool isGroundColliderExit;
    [SerializeField] private int randomActionNumber;
    [SerializeField] private int randomDodge;
    [SerializeField] private float jumpForce = 4;
    [SerializeField] private float speed;
    [SerializeField] private float startSpeed;
    [SerializeField] private float jumpOffset;

    private bool isMeleeStart;
    private float timerAction = 4;
    private float timerCorutine = 0f;
    private float distToPlayer;
    private bool isStopMove;
    private int randomDodgeVariation;

    private Health healthPlayerComponent;

    private void Awake()
    {
        healthPlayerComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); 
        speed = Random.RandomRange(1f, 2f);
        startSpeed = speed;
        isMeleeStart = false;
        isShooting = false;
    }

    /// <summary>
    /// �������� ���������� ����� �� �����
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
    }

    /// <summary>
    /// ��� �������, ��� ����� � �������� ��������, ��������� �������� ���������� ��������. 
    /// </summary>
    private void Update()
    {
        if (isCollisionPlayer && !healthPlayerComponent.isDead && !health.isDead)
        {
            isStopMove = false;
            timerAction -= Time.deltaTime;
            timerCorutine -= Time.deltaTime;
            if (!isActionActive)
            {
                enemyAttacktrigger.SetActive(true);
                StartCoroutine(RandomActionCorutine());
            }
        }
        else
        {
            if(!isStopMove)
            {
                timerCorutine = 10;
                isShooting = false;
                isActionActive = false;
                isMeleeStart = false;
                startAttack = false;
                StopAllCoroutines();
                enemyAttacktrigger.SetActive(false);
                isStopMove = true;
            }

        }

        if (timerCorutine < 0)
        {
            timerCorutine = 10;
            isActionActive = false;
            isMeleeStart = false;
            isShooting = false;
            startAttack = false;
            gun.SetActive(false);
            enemyTransform.localRotation = Quaternion.Euler(0, 0, 0);
            StopAllCoroutines();
        }

        if (isShooting)
        {
            spriteRenderer.flipX = false;
            if (playerTransform.position.x > enemyTransform.position.x)
            {
                enemyTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            else if (playerTransform.position.x < enemyTransform.position.x)
            {
                enemyTransform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private IEnumerator RandomActionCorutine()
    {
        isActionActive = true;
        randomActionNumber = Random.Range(1, 4);
        if(randomActionNumber >= 2)
        {
            enemyTransform.localRotation = Quaternion.Euler(0, 0, 0); //���������� Scale ����� ������ ��������
            gun.SetActive(false); //������� ������ ����� ������ ��������
            isMeleeStart = true;
            timerAction = 7;
            StartCoroutine(MeleeAttackCorutine());
        }

        if(randomActionNumber == 1)
        {
            gun.SetActive(true);
            isShooting = true;
            StartCoroutine(enemyShooter.ShootPlayerPosition());
        }
        yield break;
    }

    /// <summary>
    /// �������� ����� � ������� ���. ��������� ���� � ������������ ��� � �������� isMeleeStart =true
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleeAttackCorutine()
    {
        while(isMeleeStart)
        {
            if (timerAction <= 0) //������ �������
            {
                randomDodgeVariation = Random.Range(1, 4);
                if(randomDodgeVariation ==1)
                {
                    timerAction = 6;
                }
                else if(randomDodgeVariation >=2)
                {
                    StartCoroutine(DodgeCorutine());
                    yield return new WaitForSeconds(1);
                    StopCoroutine(DodgeCorutine());
                    timerAction = 6;
                }
            }

            if (timerAction > 0 && !startAttack) //������ ����������� � ������ (��������� ����� �������� Move � ����� �������� ����� ���������� ������
                                                 //DetectedPlayerPosition). 
            {
                if(speed == 0) //�������������� �������, �������� �������� (������ �������� StopMoveAttack() �� ������������ ������� �� ����� � ���������
                               //��������� ���� ������� � �������� ���������� ������ 0). ���������� ������� ������ � ������������� �������� ��������.
                {
                    if (playerTransform.position.x > enemyTransform.position.x)
                    {
                        speed = -startSpeed;
                    }
                    else if (playerTransform.position.x < enemyTransform.position.x)
                    {
                        speed = startSpeed;
                    }
                }

                Move();
                DetectedPlayerPosition();
                distToPlayer = Vector2.Distance(playerTransform.position, transform.position);

                if (distToPlayer <= 0.3f && damageDeallerEnemy.isAttack && !startAttack) //��� ������� ��������� �� ������ <= 0.3 � startAttack=false
                                                                                         //��������� �������� ��� ��������� ����� ��� �����
                {
                    StartCoroutine(StopMoveAttack()); // �������� �����
                }

                if(isGroundColliderExit) //� ������ ���� isGroundColliderExit = true (����������, ����������, ��� ��� ������ ������������� ��������� ����� �
                                         //���� ����� � ������� EnemyJump) - ������������ ����� ������ JumpEnemy;
                {
                    JumpEnemy();
                }

                yield return new WaitForSeconds(0.2f);
            }
            if (timerAction > 0 && startAttack) // 
            {
                Move();
                if (rgb.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                if (rgb.velocity.x < 0)
                {
                    spriteRenderer.flipX = true;
                }

                if (isGroundColliderExit) //� ������ ���� isGroundColliderExit = true (����������, ����������, ��� ��� ������ ������������� ��������� ����� �
                                          //���� ����� � ������� EnemyJump) - ������������ ����� ������ JumpEnemy;
                {
                    JumpEnemy();
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield break;
    }

    /// <summary>
    /// �������� �������. ����������� ������ 6 ������ � Melee �������� �� ��������� ������� timerAtcion. � ����������� �� ������� �������� ���� ������
    /// ������ (��� randomDodge =1), ���� ������ ������� (��� randomDodge =2). ��� ������� ��� ������ ��������� �� ����� isGrounded ���������� ������.
    /// �������� ������������ ���� ��� � 0.2 ������� �� ����������� �������� timerAction>0.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DodgeCorutine()
    {
        randomDodge = Random.Range(1, 3);
        while(timerAction<=0)
        {
            if(randomDodge == 1)
            {
                rgb.velocity = Vector2.right * -startSpeed; //�������� ������� ��������, ����������� � ������ ��������, �.�. ����� Move() �� ��������;

                spriteRenderer.flipX = true;

                if (isGrounded)
                {
                    JumpEnemy();
                }

                if (isGroundColliderExit) //� ������ ���� isGroundColliderExit = true (����������, ����������, ��� ��� ������ ������������� ��������� ����� �
                                          //���� ����� � ������� EnemyJump) - ������������ ����� ������ JumpEnemy;
                {
                    JumpEnemy();
                }
                yield return new WaitForSeconds(0.2f);
            }

            if(randomDodge == 2)
            {
                rgb.velocity = Vector2.right * startSpeed; //�������� ������� ��������, ����������� � ������ ��������, �.�. ����� Move() �� ��������;

                spriteRenderer.flipX = false;

                if (isGroundColliderExit) //� ������ ���� isGroundColliderExit = true (����������, ����������, ��� ��� ������ ������������� ��������� ����� �
                                          //���� ����� � ������� EnemyJump) - ������������ ����� ������ JumpEnemy;
                {
                    JumpEnemy();
                }

                if (isGrounded)
                {
                    JumpEnemy();
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    /// <summary>
    /// ����� ��������� � ������� ������� � ����� Player. ������ ������ ��������� ����� ����� �� ������.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isCollisionPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollisionPlayer = false;
        }
    }

    /// <summary>
    /// ����� ������ ���������� (��� ������� ������ �� X > ������� ����� �� � - ���� ���� ������� � ��������). ����� �������� ������� � ��������� 
    /// </summary>
    public void DetectedPlayerPosition()
    {
        if (playerTransform.position.x > enemyTransform.position.x)
        {
            MoveForwardCondition();
            spriteRenderer.flipX = false;
        }
        else if (playerTransform.position.x < enemyTransform.position.x)
        {
            MoveBackCondition();
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// ����� ������ ����������� ������� (�����)
    /// </summary>
    public void JumpEnemy()
    {
        rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);
    }

    /// <summary>
    /// ����� �������� ����������� ������� (�����)
    /// </summary>
    private void Move()
    {
        rgb.velocity = Vector2.right * speed;
    }

    /// <summary>
    /// ����� ����������� �������� ����� ������ (�������). ���� ������� �������� speed ��������� �������� < 0, �� ������� ������ ����������� ������ � ��� 
    /// ���������� ���������� (speed*=-1). ���� ����������� ������� - ������ �� ������. ���� ������ �������� ��������� �������� flip �� X = true (������� ������), 
    /// �� ��� ���������� ������� �� false (���������� �� �����). ���� ������� ������� - ������ �� ������.
    /// </summary>
    private void MoveForwardCondition()
    {
        if (speed < 0)
        {
            speed *= -1;
        }
        else if (speed > 0)
        {
            
        }
    }

    /// <summary>
    /// ����� �������� ����� ����� (������). ���� ������� �������� speed ��������� �������� > 0, �� ������� ������ ����������� ������� � ��� ���������� ����������. 
    /// (speed*=-1). ���� ����������� ������ - ������ �� ������. ���� ������ �������� ��������� �������� flip �� X = false (������� �������), �� ��� ���������� 
    /// ������� �� true (���������� ������). ���� ������� ������ - ������ �� ������.
    /// </summary>
    private void MoveBackCondition()
    {
        if (speed < 0)
        {
        }
        else if (speed > 0)
        {
            speed *= -1;
        }
    }

    /// <summary>
    /// ��������, �������� ��� ������������ �������� ���������� �����.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopMoveAttack()
    {
        startAttack = true;
        speed = 0f;
        yield return new WaitForSeconds(1f);
        if (playerTransform.position.x > enemyTransform.position.x)
        {
            speed = -startSpeed;
        }
        else if (playerTransform.position.x < enemyTransform.position.x)
        {
            speed = startSpeed;
        }
        yield return new WaitForSeconds(1f);
        startAttack = false;
    }
}