using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Common Settings")]
    [SerializeField] private Transform damageCanvasPoint;
    [SerializeField] private GameObject damagePointTextObject;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isHit;
    public bool isDead;

    [Header("Player Settings")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerRotate playerRotate;
    [SerializeField] private GunController gunController;
    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject boxColliderObject;
    private DeathMessage deathMessage;
    private GameOver gameOver;
    private HealthBar healthBar;

    [Header("Enemy Settings")]
    [SerializeField] private Rigidbody2D rgb;
    [SerializeField] private EnemyAnimator enemyAnimator;
    [SerializeField] private GameObject coinObject;
    [SerializeField] private GameObject enemyGun;
    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private int enemyCost;
    private Transform coinPoint;

    //объекты, на которых висят скрипты, к которым мы обращаемся
    private FinalScores finalScore;
    private PointsCounter pointsCounter;
    //private DeathMessage deadTextObject;
    //private GameOver gameOverObject;

    /// <summary>
    /// Метод, который при запуске сцены присваивает переменной currentHealth (текущее значение здоровья) переменную maxHealth (максимальное значение здоровья). maxHealth настраивается в инспекторе.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        finalScore = GameObject.FindAnyObjectByType<FinalScores>(); // Находим объект Finish, присваиваем компонент FinalScores и сохраняем экземпляр класса FinalScores
        deathMessage = GameObject.FindObjectOfType<DeathMessage>(); //
        gameOver = GameObject.FindObjectOfType<GameOver>(); // 
        healthBar = GetComponent<HealthBar>();

        if (gameObject.CompareTag("Damageable"))
        {
            coinPoint = GetComponent<Transform>();
            pointsCounter = GameObject.FindAnyObjectByType<PointsCounter>();

            finalScore.maxCoinScore += 1; // сцене прибавляет +1 к максимальному количеству монет (из каждого врага при смерти выпадает монета).
        }
    }

    /// <summary>
    /// Публичный метод, который вызывается в скрипте damageDealler  - принимает в себя float 
    /// значение damage, которое отнимается от текущего значения здоровья currentHealth. При каждом срабатывании метода происходит проверка 
    /// метода CheckIsAlive, а также срабатывание корутины hitTime (нанесение урона).
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject damagePointsTextObject = Instantiate(damagePointTextObject, damageCanvasPoint.position, Quaternion.identity); // создаем объект (текст) на позиции канваса damageCanvas (визуальное отображение урона)
        damagePointsTextObject.transform.SetParent(damageCanvasPoint.transform); //устанавливаем родительский объект для созданного объекта
        damagePointsTextObject.transform.localScale = new Vector2(1, 1); // уменьшаем скейл объекта = 1 (уменьшаем объект под канвас)
        damagePointsTextObject.GetComponent<Text>().text = "- " + damage + " урона"; // присваиваем к классу Text, после чего обращаемся к компоненту text объекта и присваиваем значение damage 
        StartCoroutine(HitTime());
        CheckIsAlive();
        if(healthBar !=null)
        healthBar.HealthBarUpdate(maxHealth, currentHealth);
    }

    /// <summary>
    /// Приватный метод, который срабатывает при реализации метода TakeDamage (при нанесении урона). Метод проверяет текущее 
    /// значение здоровья currentHealth на значение > или < 0 и присваивает переменной isDead значение true или false соответственно. 
    /// При значении isDead true, с целью исключения перемещение объекта после смерти, замораживает значения Rigidbody2D Constraints, 
    /// а также, для беспрепятственного прохождения через объект, уничтожает коллайдеры объекта и запускает корутину, выводяюую 
    /// сообщение о смерти deathTextCoroutine из скрипта deathMessage.
    /// </summary>
    private void CheckIsAlive()
    {
        if (currentHealth > 0)
            isDead = false;
        else
        {
            isDead = true;
            finalScore.currentKilledEnemies += 1;
            if (gameObject.CompareTag("Damageable")) //Если тэг объекта, на котором висит скрипт - Damageable - то запускает корутину ObjectDestroy. 
            {
                Destroy(capsuleCollider);
                rgb.constraints = RigidbodyConstraints2D.FreezeAll;
                enemyGun.SetActive(false);
                pointsCounter.PointsCount(enemyCost);
                enemyCanvas.SetActive(true);
                StartCoroutine(ObjectDestroy());
                GameObject currentCoin = Instantiate(coinObject, coinPoint.position, Quaternion.identity);
                finalScore.maxCoinScore -= 1; // убавляет 1 от максимального количества монет (из каждого врага при смерти выпадает монета и данная монета прибавит +1 к максимальному количеству через метод Awake).
            }
            else if (gameObject.CompareTag("Player")) //Если тэг объекта, на котором висит скрипт - Player - то - то запускает корутину DeathTextCoroutine из скрипта DeathMessage (данный объект игрок и сообщение о его смерти выведется по центру) и корутину GameOverCorutine из скрипта GameOver (запуск экрана поражения).
            {
                Destroy(capsuleCollider);
                playerInput.enabled = false;
                playerMovement.enabled = false;
                playerRotate.enabled = false;
                gunController.enabled = false;
                gunObject.SetActive(false);
                boxColliderObject.SetActive(true);
                StartCoroutine(deathMessage.DeathTextCoroutine());
                StartCoroutine(gameOver.GameOverCoroutine());
            }
        }
    }

    /// <summary>
    /// Приватная корутина, при срабатывании которой публичное значение bool переменной isHit на долю секунды принимает значение true. Значение принимается в скриптах PlayerAnimation и SkeletonAnimation.
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitTime()
    {
        yield return null;
        if(enemyAnimator!=null)
        {
            enemyAnimator.IsHurtTrigger();
        }
            
        if (playerAnimator != null)
        {
            playerAnimator.IsHurtTrigger();
        }
    }

    private IEnumerator ObjectDestroy()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
