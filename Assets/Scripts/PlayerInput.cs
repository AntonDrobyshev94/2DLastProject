using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRendererBullet;
    [SerializeField] private SpriteRenderer gun;
    private BulletsChecking bulletsChecking;
    private PlayerMovement playerMovement;
    [SerializeField] private Shooter shooter;
    private BulletsCounter bulletsCounter;
    public bool isRunning;
    private Vector2 moveVector;
    private bool isJumpButtonPressed;
    private bool isSpeedRun;
    [SerializeField, Range(1, 10)] private float rapidityOfFireAssaultRifle=8;
    [SerializeField, Range(0.5f, 2)] private float rapidityOfFireShotgun=0.5f;
    [SerializeField, Range(0.5f, 1)] private float rapidityOfFireSniperRifle=1.5f;
    [SerializeField, Range(10, 15)] private float rapidityOfFireMachinegun=15;

    private float speedFire=1f;
    private float nextFire = 0f;
    public int gunNumber;
    public bool isPauseGame;

    [SerializeField] private Sprite[] gunsArray;
    private SceneChanger sceneChanger;
    public bool gateCallRequest;
    public bool rechargeGun;
    private GasMaskScript gasMaskScript;

    public static event Action rKeyPressed; // создаем событие RKeyPressed.

    private void Awake()
    {
        isPauseGame = false;
        playerMovement = GetComponent<PlayerMovement>();
        bulletsCounter = GameObject.FindObjectOfType<BulletsCounter>(); // находим объект со скриптом BulletCounter и кешируем экземпл€р данного скрипта
        sceneChanger = GameObject.FindObjectOfType<SceneChanger>();
        RapidityOfFire();
        bulletsChecking = GetComponent<BulletsChecking>();
        gasMaskScript = GameObject.FindObjectOfType<GasMaskScript>();
    }

    /// <summary>
    ///  аждый кадр провер€ет ввод присвоенных float и bool значений horizontalDirection (направление движени€) и isJumpButtonPressed (прыжок) соответственно, а также ввод клавиши E (супер атака) и клавиш мыши (выстрел и обычна€ атака).
    /// Ќаправление движени€ в каждом кадре провер€етс€ float параметром horizontalDirection. ¬ случае, если значение не равно 0, включаетс€ анимаци€ движени€ (bool переменна€ isRunning = true). ѕри положительном значении horizontalDirection, 
    /// помимо isRunning = true игроку присваиваетс€ новый вектор направлени€ движени€, который мен€ет координату x Scale игрока на положительную (спрайт игрока смотрит вправо), а при отрицательном - на отрицательное значение Scale (спрайт игрока смотрит влево).
    /// ѕри горизонтальном направлении horizontalDirection > 0 рендер стрелы (bullet) получает значение false параметра flip по X (стрела смотрит направо), при < 0 присваивает true (стрела смотрит налево). 
    /// “акже, данный метод каждый кадр обрабатывает метод движени€, описанный в скрипте PlayerMovement, который принимает значени€ направлени€ (direction) и bool переменную isJumpButtonPressed из данного скрипта.
    /// </summary>
    private void Update()
    {
        if(!isPauseGame)
        {
            moveVector.x = Input.GetAxis(GlobalStringVariables.HORIZONTAL_AXIS);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSpeedRun = true;
            }
            else
            {
                isSpeedRun = false;
            }

            isJumpButtonPressed = Input.GetButtonDown(GlobalStringVariables.JUMP);
            if (moveVector.x == 0)
            {
                isRunning = false;
            }

            if (moveVector.x > 0)
            {
                isRunning = true;
            }
            if (moveVector.x < 0)
            {
                isRunning = true;
            }

            if (Input.GetButton(GlobalStringVariables.FIRE_1) && Time.time > nextFire && !bulletsChecking.isStartRecharging)
            {
                bulletsChecking.IsAnyBullets();
                nextFire = Time.time + 1f / speedFire; // врем€ до следующего выстрела (без учета перезар€дки)
                shooter.Shoot();
                bulletsCounter.UpdateText(); //апдейтим выводимое на экран количество патронов 
                bulletsChecking.CheckShoot();// провер€ем количество патронов в магазине
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // —кролл колеса мыши вперед
            {
                gunNumber++;

                if (gunNumber > 3)
                {
                    gunNumber = 0;
                    gun.sprite = gunsArray[gunNumber];
                }
                else
                {
                    gun.sprite = gunsArray[gunNumber];
                }
                bulletsCounter.UpdateText(); //апдейтим выводимое на экран актуальное количество патронов
                RapidityOfFire();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // —кролл колеса мыши назад
            {
                gunNumber--;
                if (gunNumber < 0)
                {
                    gunNumber = gunsArray.Length - 1;
                    gun.sprite = gunsArray[gunNumber];
                }
                else
                {
                    gun.sprite = gunsArray[gunNumber];
                }
                bulletsCounter.UpdateText(); //апдейтим выводимое на экран актуальное количество патронов
                RapidityOfFire();
            }

            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(GateCoroutine());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                gasMaskScript.ButtonEvent();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rKeyPressed != null)
                {
                    rKeyPressed();
                }
            }
            playerMovement.Move(moveVector, isJumpButtonPressed, isSpeedRun);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneChanger.PauseGameButton();
            isPauseGame = !isPauseGame;
        }    
    }

    /// <summary>
    /// ћетод присвоени€ скорострельности оружию в соответствии с его индексом
    /// </summary>
    private void RapidityOfFire()
    {
        if (gunNumber == 0) // оружие - FireAssaultRifle
        {
            speedFire = rapidityOfFireAssaultRifle;
            shooter.bulletObject = shooter.bulletsArray[0];
        }
        else if (gunNumber == 1) // оружие - Shotgun
        {
            speedFire = rapidityOfFireShotgun;
            shooter.bulletObject = shooter.bulletsArray[1];
        }
        else if (gunNumber == 2) // оружие - SniperRifle
        {
            speedFire = rapidityOfFireSniperRifle;
            shooter.bulletObject = shooter.bulletsArray[2];
        }
        else if (gunNumber == 3) // оружие - Machinegun
        {
            speedFire = rapidityOfFireMachinegun;
            shooter.bulletObject = shooter.bulletsArray[3];
        }
    }

    /// <summary>
    ///  орутина открыти€ двери (посылает сигнал о том, что игрок пытаетс€ открыть дверь).
    /// </summary>
    /// <returns></returns>
    private IEnumerator GateCoroutine()
    {
        gateCallRequest = true;
        yield return new WaitForSeconds(1);
        gateCallRequest = false;
        yield break;
    }
}
