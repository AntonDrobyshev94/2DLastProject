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
    /// Проверка нахождения врага на земле
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
    }

    /// <summary>
    /// При условии, что игрок в коллизии триггера, запускает корутину случайного действия. 
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
            enemyTransform.localRotation = Quaternion.Euler(0, 0, 0); //возвращает Scale после метода стрельбы
            gun.SetActive(false); //убирает оружие после метода стрельбы
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
    /// Корутина атаки в ближнем бою. Запускает цикл и отрабатывает его с условием isMeleeStart =true
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleeAttackCorutine()
    {
        while(isMeleeStart)
        {
            if (timerAction <= 0) //Стадия уворота
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

            if (timerAction > 0 && !startAttack) //Стадия приближения к игроку (запускает метод движения Move и метод детекции места нахождения игрока
                                                 //DetectedPlayerPosition). 
            {
                if(speed == 0) //Дополнительное условие, служащее костылем (иногда корутина StopMoveAttack() не отрабатывает условие до конца в следствие
                               //остановки всех корутин и скорость становится равной 0). Определяет позицию игрока и устанавливает значение скорости.
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

                if (distToPlayer <= 0.3f && damageDeallerEnemy.isAttack && !startAttack) //при условии дистанции до игрока <= 0.3 и startAttack=false
                                                                                         //запускает корутину для остановки врага для атаки
                {
                    StartCoroutine(StopMoveAttack()); // корутина атаки
                }

                if(isGroundColliderExit) //В случае если isGroundColliderExit = true (переменная, означающая, что под врагом заканчивается коллайдер земли и
                                         //враг вошел в триггер EnemyJump) - отрабатывает метод прыжка JumpEnemy;
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

                if (isGroundColliderExit) //В случае если isGroundColliderExit = true (переменная, означающая, что под врагом заканчивается коллайдер земли и
                                          //враг вошел в триггер EnemyJump) - отрабатывает метод прыжка JumpEnemy;
                {
                    JumpEnemy();
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield break;
    }

    /// <summary>
    /// Корутина уворота. Запускается каждые 6 секунд в Melee корутине по окончанию таймера timerAtcion. В зависимости от рандома выбирает либо уворот
    /// налево (при randomDodge =1), либо уворот направо (при randomDodge =2). При условии что объект находится на земле isGrounded происходит прыжок.
    /// Корутина отрабатывает цикл раз в 0.2 секунды до возвращения значения timerAction>0.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DodgeCorutine()
    {
        randomDodge = Random.Range(1, 3);
        while(timerAction<=0)
        {
            if(randomDodge == 1)
            {
                rgb.velocity = Vector2.right * -startSpeed; //является методом движения, применяемым в данной корутине, т.к. метод Move() не подходит;

                spriteRenderer.flipX = true;

                if (isGrounded)
                {
                    JumpEnemy();
                }

                if (isGroundColliderExit) //В случае если isGroundColliderExit = true (переменная, означающая, что под врагом заканчивается коллайдер земли и
                                          //враг вошел в триггер EnemyJump) - отрабатывает метод прыжка JumpEnemy;
                {
                    JumpEnemy();
                }
                yield return new WaitForSeconds(0.2f);
            }

            if(randomDodge == 2)
            {
                rgb.velocity = Vector2.right * startSpeed; //является методом движения, применяемым в данной корутине, т.к. метод Move() не подходит;

                spriteRenderer.flipX = false;

                if (isGroundColliderExit) //В случае если isGroundColliderExit = true (переменная, означающая, что под врагом заканчивается коллайдер земли и
                                          //враг вошел в триггер EnemyJump) - отрабатывает метод прыжка JumpEnemy;
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
    /// Метод вхождения в триггер объекта с тегом Player. Данный тригер запускает атаку врага на игрока.
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
    /// Метод начала провакации (при позиции игрока по X > позиции врага по Х - враг идет направо и наоборот). Метод движения записан в корутинах 
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
    /// Метод прыжка физического объекта (врага)
    /// </summary>
    public void JumpEnemy()
    {
        rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);
    }

    /// <summary>
    /// Метод движения физического объекта (врага)
    /// </summary>
    private void Move()
    {
        rgb.velocity = Vector2.right * speed;
    }

    /// <summary>
    /// Метод направления движения врага вперед (направо). Если текущая скорость speed принимает значение < 0, то объекту задано направление налево и его 
    /// необходимо развернуть (speed*=-1). Если направление направо - ничего не меняем. Если спрайт рендерер принимает значение flip по X = true (смотрит налево), 
    /// то его необходимо принять за false (развернуть на право). Если смотрит направо - ничего не меняем.
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
    /// Метод движения врага назад (налево). Если текущая скорость speed принимает значение > 0, то объекту задано направление направо и его необходимо развернуть. 
    /// (speed*=-1). Если направление налево - ничего не меняем. Если спрайт рендерер принимает значение flip по X = false (смотрит направо), то его необходимо 
    /// принять за true (развернуть налево). Если смотрит налево - ничего не меняем.
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
    /// Корутина, служащая для приостановки движения атакующего врага.
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