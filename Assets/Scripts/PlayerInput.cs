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

    public static event Action rKeyPressed; // ������� ������� RKeyPressed.

    private void Awake()
    {
        isPauseGame = false;
        playerMovement = GetComponent<PlayerMovement>();
        bulletsCounter = GameObject.FindObjectOfType<BulletsCounter>(); // ������� ������ �� �������� BulletCounter � �������� ��������� ������� �������
        sceneChanger = GameObject.FindObjectOfType<SceneChanger>();
        RapidityOfFire();
        bulletsChecking = GetComponent<BulletsChecking>();
        gasMaskScript = GameObject.FindObjectOfType<GasMaskScript>();
    }

    /// <summary>
    /// ������ ���� ��������� ���� ����������� float � bool �������� horizontalDirection (����������� ��������) � isJumpButtonPressed (������) ��������������, � ����� ���� ������� E (����� �����) � ������ ���� (������� � ������� �����).
    /// ����������� �������� � ������ ����� ����������� float ���������� horizontalDirection. � ������, ���� �������� �� ����� 0, ���������� �������� �������� (bool ���������� isRunning = true). ��� ������������� �������� horizontalDirection, 
    /// ������ isRunning = true ������ ������������� ����� ������ ����������� ��������, ������� ������ ���������� x Scale ������ �� ������������� (������ ������ ������� ������), � ��� ������������� - �� ������������� �������� Scale (������ ������ ������� �����).
    /// ��� �������������� ����������� horizontalDirection > 0 ������ ������ (bullet) �������� �������� false ��������� flip �� X (������ ������� �������), ��� < 0 ����������� true (������ ������� ������). 
    /// �����, ������ ����� ������ ���� ������������ ����� ��������, ��������� � ������� PlayerMovement, ������� ��������� �������� ����������� (direction) � bool ���������� isJumpButtonPressed �� ������� �������.
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
                nextFire = Time.time + 1f / speedFire; // ����� �� ���������� �������� (��� ����� �����������)
                shooter.Shoot();
                bulletsCounter.UpdateText(); //�������� ��������� �� ����� ���������� �������� 
                bulletsChecking.CheckShoot();// ��������� ���������� �������� � ��������
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // ������ ������ ���� ������
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
                bulletsCounter.UpdateText(); //�������� ��������� �� ����� ���������� ���������� ��������
                RapidityOfFire();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // ������ ������ ���� �����
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
                bulletsCounter.UpdateText(); //�������� ��������� �� ����� ���������� ���������� ��������
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
    /// ����� ���������� ���������������� ������ � ������������ � ��� ��������
    /// </summary>
    private void RapidityOfFire()
    {
        if (gunNumber == 0) // ������ - FireAssaultRifle
        {
            speedFire = rapidityOfFireAssaultRifle;
            shooter.bulletObject = shooter.bulletsArray[0];
        }
        else if (gunNumber == 1) // ������ - Shotgun
        {
            speedFire = rapidityOfFireShotgun;
            shooter.bulletObject = shooter.bulletsArray[1];
        }
        else if (gunNumber == 2) // ������ - SniperRifle
        {
            speedFire = rapidityOfFireSniperRifle;
            shooter.bulletObject = shooter.bulletsArray[2];
        }
        else if (gunNumber == 3) // ������ - Machinegun
        {
            speedFire = rapidityOfFireMachinegun;
            shooter.bulletObject = shooter.bulletsArray[3];
        }
    }

    /// <summary>
    /// �������� �������� ����� (�������� ������ � ���, ��� ����� �������� ������� �����).
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
