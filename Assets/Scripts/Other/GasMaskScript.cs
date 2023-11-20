using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GasMaskScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private GameObject gasMask;
    [SerializeField] private GameObject gasMaskCanvas;
    [SerializeField] private GameObject gasObject;
    [SerializeField] private Sprite[] spriteArray;
    private Health playerObjectHealth;
    private GameOver gameOver;
    public bool isTriggerDamage;

    public bool isGasMaskEnabled = false;


    void Start()
    {
        GameOver.GasClickButtonEvent += ButtonEvent;// подписываемс€ на событие GasClickButtonEvent в котором вызываем метод ButtonEvent.
        GasScript.triggerChanged += BooleanChanged; // подписываемс€ на событие triggerChanged в котором принимаем значение bool переменной. 
        playerObjectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        gameOver = GameObject.FindObjectOfType<GameOver>();
    }

    public void BooleanChanged(bool isTriggerEnter)
    {
        isTriggerDamage = isTriggerEnter;
    }

    public void OnDisable()
    {
        GameOver.GasClickButtonEvent -= ButtonEvent; // отписываемс€ от событи€
        GasScript.triggerChanged -= BooleanChanged; // отписываемс€ от событи€
    }


    public void ButtonEvent()
    {
        if (isGasMaskEnabled)
        {
            gasMaskCanvas.SetActive(false);
            gasMask.SetActive(false);
            image.sprite = spriteArray[0];
            button.interactable = false;
            StartCoroutine(ButtonCoroutine());
            if (isTriggerDamage)
            {
                GasActive();
            }
        }
        else
        {
            gasMaskCanvas.SetActive(true);
            gasMask.SetActive(true);
            image.sprite = spriteArray[1];
            button.interactable = false;
            StartCoroutine(ButtonCoroutine());
            if (isTriggerDamage)
            {
                GasStop();
            }
        }
        isGasMaskEnabled = !isGasMaskEnabled;

        if (isTriggerDamage) //проверка на нахождение в триггере нанесени€ урона
        {
            StartCoroutine(TikDamage());
        }
    }

    private IEnumerator ButtonCoroutine()
    {
        yield return new WaitForSeconds(3);
        button.interactable = true;
    }
    public void GasActive()
    {
        gasObject.SetActive(true);
    }

    public void GasStop()
    {
        gasObject.SetActive(false);
    }

    /// <summary>
    /// ѕублична€ корутина, запускающа€ цикл, который раз в 4 секунды провер€ет выполнение значений в его скобках. ≈сли противогаз не был надет,
    /// то наносит урон в зависимости от damage. ≈сли был надет, то завершает цикл. ≈сли здоровье персонажа опустилось ниже значени€ 0, то 
    /// запускает корутину проигриша GameOverCoroutine()
    /// </summary>
    /// <returns></returns>
    public IEnumerator TikDamage()
    {
        while (isTriggerDamage && !isGasMaskEnabled)
        {
            yield return new WaitForSeconds(4);
            if (isTriggerDamage && !isGasMaskEnabled)
            {
                playerObjectHealth.TakeDamage(damage);
            }
            else
            {
                break;
            }

            if (playerObjectHealth.currentHealth <= 0)
            {
                StartCoroutine(gameOver.GameOverCoroutine());
                break;
            }
        }
    }
}
