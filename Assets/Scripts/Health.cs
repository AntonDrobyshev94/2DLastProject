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

    //�������, �� ������� ����� �������, � ������� �� ����������
    private FinalScores finalScore;
    private PointsCounter pointsCounter;
    //private DeathMessage deadTextObject;
    //private GameOver gameOverObject;

    /// <summary>
    /// �����, ������� ��� ������� ����� ����������� ���������� currentHealth (������� �������� ��������) ���������� maxHealth (������������ �������� ��������). maxHealth ������������� � ����������.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        finalScore = GameObject.FindAnyObjectByType<FinalScores>(); // ������� ������ Finish, ����������� ��������� FinalScores � ��������� ��������� ������ FinalScores
        deathMessage = GameObject.FindObjectOfType<DeathMessage>(); //
        gameOver = GameObject.FindObjectOfType<GameOver>(); // 
        healthBar = GetComponent<HealthBar>();

        if (gameObject.CompareTag("Damageable"))
        {
            coinPoint = GetComponent<Transform>();
            pointsCounter = GameObject.FindAnyObjectByType<PointsCounter>();

            finalScore.maxCoinScore += 1; // ����� ���������� +1 � ������������� ���������� ����� (�� ������� ����� ��� ������ �������� ������).
        }
    }

    /// <summary>
    /// ��������� �����, ������� ���������� � ������� damageDealler  - ��������� � ���� float 
    /// �������� damage, ������� ���������� �� �������� �������� �������� currentHealth. ��� ������ ������������ ������ ���������� �������� 
    /// ������ CheckIsAlive, � ����� ������������ �������� hitTime (��������� �����).
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject damagePointsTextObject = Instantiate(damagePointTextObject, damageCanvasPoint.position, Quaternion.identity); // ������� ������ (�����) �� ������� ������� damageCanvas (���������� ����������� �����)
        damagePointsTextObject.transform.SetParent(damageCanvasPoint.transform); //������������� ������������ ������ ��� ���������� �������
        damagePointsTextObject.transform.localScale = new Vector2(1, 1); // ��������� ����� ������� = 1 (��������� ������ ��� ������)
        damagePointsTextObject.GetComponent<Text>().text = "- " + damage + " �����"; // ����������� � ������ Text, ����� ���� ���������� � ���������� text ������� � ����������� �������� damage 
        StartCoroutine(HitTime());
        CheckIsAlive();
        if(healthBar !=null)
        healthBar.HealthBarUpdate(maxHealth, currentHealth);
    }

    /// <summary>
    /// ��������� �����, ������� ����������� ��� ���������� ������ TakeDamage (��� ��������� �����). ����� ��������� ������� 
    /// �������� �������� currentHealth �� �������� > ��� < 0 � ����������� ���������� isDead �������� true ��� false ��������������. 
    /// ��� �������� isDead true, � ����� ���������� ����������� ������� ����� ������, ������������ �������� Rigidbody2D Constraints, 
    /// � �����, ��� ������������������ ����������� ����� ������, ���������� ���������� ������� � ��������� ��������, ��������� 
    /// ��������� � ������ deathTextCoroutine �� ������� deathMessage.
    /// </summary>
    private void CheckIsAlive()
    {
        if (currentHealth > 0)
            isDead = false;
        else
        {
            isDead = true;
            finalScore.currentKilledEnemies += 1;
            if (gameObject.CompareTag("Damageable")) //���� ��� �������, �� ������� ����� ������ - Damageable - �� ��������� �������� ObjectDestroy. 
            {
                Destroy(capsuleCollider);
                rgb.constraints = RigidbodyConstraints2D.FreezeAll;
                enemyGun.SetActive(false);
                pointsCounter.PointsCount(enemyCost);
                enemyCanvas.SetActive(true);
                StartCoroutine(ObjectDestroy());
                GameObject currentCoin = Instantiate(coinObject, coinPoint.position, Quaternion.identity);
                finalScore.maxCoinScore -= 1; // �������� 1 �� ������������� ���������� ����� (�� ������� ����� ��� ������ �������� ������ � ������ ������ �������� +1 � ������������� ���������� ����� ����� Awake).
            }
            else if (gameObject.CompareTag("Player")) //���� ��� �������, �� ������� ����� ������ - Player - �� - �� ��������� �������� DeathTextCoroutine �� ������� DeathMessage (������ ������ ����� � ��������� � ��� ������ ��������� �� ������) � �������� GameOverCorutine �� ������� GameOver (������ ������ ���������).
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
    /// ��������� ��������, ��� ������������ ������� ��������� �������� bool ���������� isHit �� ���� ������� ��������� �������� true. �������� ����������� � �������� PlayerAnimation � SkeletonAnimation.
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
