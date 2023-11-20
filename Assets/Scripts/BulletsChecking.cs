using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsChecking : MonoBehaviour
{
    [SerializeField] private float startAssaultBullets;
    [SerializeField] private float startShotgunBullets;
    [SerializeField] private float startSniperBullets;
    [SerializeField] private float startMachinegunBullets;
    [SerializeField] private AudioScript audioScript;

    public float currentAssaultBullets;
    public float currentShotgunBullets;
    public float currentSniperBullets;
    public float currentMachinegunBullets;

    private BulletsCounter bulletsCounter;
    private PlayerInput playerInput;
    private RechargingBar rechargingBar;
    public bool isHaveBullets;

    public float maximumMagazineAssaultRiffle = 30f; //������������ ������ ��������
    public float maximumMagazineShotguntRiffle = 2f;
    public float maximumMagazineSniperRifle = 1f;
    public float maximumMagazineMachinegun = 300f;

    public float rechargeTimerAssaultRiffle = 3f; //�������� ����������� ������
    public float rechargeTimerShotguntRiffle = 2f;
    public float rechargeTimerSniperRifle = 2f;
    public float rechargeTimerMachinegun = 9f;

    public float currentMagazineAssaultRiffle = 0f; //������� ���������� �������� � ��������
    public float currentMagazineShotguntRiffle = 0f;
    public float currentMagazineSniperRifle = 0f;
    public float currentMagazineMachinegun = 0f;

    private float currentRechargeTimer; //������� �������� �����������

    public bool isStartRecharging = false;

    /// <summary>
    /// �� ������ ����� ������ ������ � ������ ArrowCounterText � ������������ � ���������� GameObject
    /// </summary>
    private void Awake()
    {
        PlayerInput.rKeyPressed += OnRKeyPressed; // ������������� �� ������� rKeyPressed
        currentAssaultBullets = startAssaultBullets;
        currentShotgunBullets = startShotgunBullets;
        currentSniperBullets = startSniperBullets;
        currentMachinegunBullets = startMachinegunBullets;

        bulletsCounter = GameObject.FindObjectOfType<BulletsCounter>(); // ������� ������ �� �������� BulletCounter � �������� ��������� ������� �������
        rechargingBar = GetComponent<RechargingBar>();

        if (startAssaultBullets > maximumMagazineAssaultRiffle)
        {
            currentMagazineAssaultRiffle = maximumMagazineAssaultRiffle;
        }
        else if (startAssaultBullets <= maximumMagazineAssaultRiffle)
        {
            currentMagazineAssaultRiffle = startAssaultBullets;
        }

        if (startShotgunBullets > maximumMagazineShotguntRiffle)
        {
            currentMagazineShotguntRiffle = maximumMagazineShotguntRiffle;
        }
        else if (startShotgunBullets <= maximumMagazineShotguntRiffle)
        {
            currentMagazineShotguntRiffle = startShotgunBullets;
        }

        if (startSniperBullets > maximumMagazineSniperRifle)
        {
            currentMagazineSniperRifle = maximumMagazineSniperRifle;
        }
        else if (startSniperBullets <= maximumMagazineSniperRifle)
        {
            currentMagazineSniperRifle = startSniperBullets;
        }

        if (startMachinegunBullets > maximumMagazineMachinegun)
        {
            currentMagazineMachinegun = maximumMagazineMachinegun;
        }
        else if (startMachinegunBullets <= maximumMagazineMachinegun)
        {
            currentMagazineMachinegun = startMachinegunBullets;
        }

        playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// ����� �������� �������� ���������� ���� �������� ���������� ������ (������ ������� �� ������� ������� gunNumber) - ����� ���������� ���� � 
    /// ���������� ���� � �������� � �� ����� �� ����� (������ BulletsCounter).
    /// </summary>
    /// <param name="arrow"></param>
    public void BulletsCount(float bullet)
    {
        if (playerInput.gunNumber == 0)
        {
            currentAssaultBullets -= bullet;
            currentMagazineAssaultRiffle -= bullet;
        }

        if (playerInput.gunNumber == 1)
        {
            currentShotgunBullets -= bullet;
            currentMagazineShotguntRiffle -= bullet;
        }

        if (playerInput.gunNumber == 2)
        {
            currentSniperBullets -= bullet;
            currentMagazineSniperRifle -= bullet;
        }

        if (playerInput.gunNumber == 3)
        {
            currentMachinegunBullets -= bullet;
            currentMagazineMachinegun -= bullet;
        }
    }

    /// <summary>
    /// �����, ������� ���������, ���� �� ������� � �������� �������� ���������� ������ ��� �� ���, � � ����������� �� ����� ��������� �������� ��� 
    /// ��������� (isHaveBullets = true / false). ����� ��� ������� �������� ��������� ����� ��������������� ������ ��������.
    /// </summary>
    public void IsAnyBullets()
    {
        if (playerInput.gunNumber == 0)
        {
            if (currentMagazineAssaultRiffle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 1)
        {
            if (currentMagazineShotguntRiffle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 2)
        {
            if (currentMagazineSniperRifle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 3)
        {
            if (currentMagazineMachinegun > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
    }

    /// <summary>
    /// �������� �������������� ����������� ��������, ������� ���������� �������� �������� ���������� ������ � �������� ���������� �������� <=0.
    /// � ������ ���������� ������� ������������� ������� ������ ����������� currentRechargeTimer ������ ������� ����������� ��������
    /// ���������� ������ (��-������ ��� ��������� �������� rechargeTimerAssaultRiffle), ����� ���� ��������� ���� � �������� �������
    /// currentRechargeTimer >0. ���� ������������ ������ ����, � ������� ���������� ������ ������� ��� ������ -=Time.Deltatime � �������� 
    /// ���� �������: 
    /// 1. �������� �������� ������� ����������� - ���� ������ ��������� �������� <=0 (�.�. ����������� �����������), �� ��������� �������� 
    /// �������� ���������� �������� ���������� ������ � ������������� ���������� �������� � �������� ��� �������� ������. 
    /// ���� ������� �������� �������� >= �������������, �� ����������� �������� ���������� �������� � �������� ������������ ����������, 
    /// ���� ������� �������� �������� < �������������, �� ����������� �������� ���������� �������� � �������� ������� ���������� �������� 
    /// ����� ������ (��-������: currentMagazineAssaultRiffle = currentAssaultBullets);
    /// 2. �������� �� ����� ��������������� ������. ���� ������, ������� ������ ����������� ��������� (gunNumber ������� ���� �������� �� 
    /// ���������� ������������), �� ���������� ����������� �������� �������� ������� ����������� (��-������: currentRechargeTimer = 
    /// rechargeTimerAssaultRiffle) � ���������� ����� � ����������� ������� �� �������� ����������� yield break;
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoRechargeMagazine()
    {
        if (currentMagazineAssaultRiffle <= 0 && playerInput.gunNumber == 0 && currentAssaultBullets != 0)
        {
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerAssaultRiffle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeAssaultBarUpdate(currentRechargeTimer, rechargeTimerAssaultRiffle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerAssaultRiffle)
                {
                    if (currentAssaultBullets >= maximumMagazineAssaultRiffle)
                    {
                        currentMagazineAssaultRiffle = maximumMagazineAssaultRiffle;
                    }
                    else if (currentAssaultBullets < maximumMagazineAssaultRiffle)
                    {
                        currentMagazineAssaultRiffle = currentAssaultBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 0)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (currentMagazineShotguntRiffle <= 0 && playerInput.gunNumber == 1 && currentShotgunBullets != 0)
        {
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerShotguntRiffle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeShotgunBarUpdate(currentRechargeTimer, rechargeTimerShotguntRiffle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerShotguntRiffle)
                {
                    if (currentShotgunBullets >= maximumMagazineShotguntRiffle)
                    {
                        currentMagazineShotguntRiffle = maximumMagazineShotguntRiffle;
                    }
                    else if (currentShotgunBullets < maximumMagazineShotguntRiffle)
                    {
                        currentMagazineShotguntRiffle = currentShotgunBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 1)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (currentMagazineSniperRifle <= 0 && playerInput.gunNumber == 2 && currentSniperBullets != 0)
        {
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerSniperRifle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeSniperBarUpdate(currentRechargeTimer, rechargeTimerSniperRifle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerSniperRifle)
                {
                    if (currentSniperBullets >= maximumMagazineSniperRifle)
                    {
                        currentMagazineSniperRifle = maximumMagazineSniperRifle;
                    }
                    else if (currentSniperBullets < maximumMagazineSniperRifle)
                    {
                        currentMagazineSniperRifle = currentSniperBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 2)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (currentMagazineMachinegun <= 0 && playerInput.gunNumber == 3 && currentMachinegunBullets != 0)
        {
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerMachinegun)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeMachinegunBarUpdate(currentRechargeTimer, rechargeTimerMachinegun);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerMachinegun)
                {
                    if (currentMachinegunBullets >= maximumMagazineMachinegun)
                    {
                        currentMagazineMachinegun = maximumMagazineMachinegun;
                    }
                    else if (currentMachinegunBullets < maximumMagazineMachinegun)
                    {
                        currentMagazineMachinegun = currentMachinegunBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 3)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        isStartRecharging = false;
        yield break;
    }

    /// <summary>
    /// �����, ����������� ��������� (������ �� ������� PlayerInput), ������� ��������� ������� �������� � �������� � ������� ��������� ������.
    /// ���� ���������� �������� <= 0, �� ���������� ������ �������� ����������� �������� RechargeMagazine().
    /// </summary>
    public void CheckShoot()
    {
        if (currentMagazineAssaultRiffle <= 0 && playerInput.gunNumber == 0)
        {
            isStartRecharging = true;
            StartCoroutine(AutoRechargeMagazine());
        }
        if (currentMagazineShotguntRiffle <= 0 && playerInput.gunNumber == 1)
        {
            isStartRecharging = true;
            StartCoroutine(AutoRechargeMagazine());
        }
        if (currentMagazineSniperRifle <= 0 && playerInput.gunNumber == 2)
        {
            isStartRecharging = true;
            StartCoroutine(AutoRechargeMagazine());
        }
        if (currentMagazineMachinegun <= 0 && playerInput.gunNumber == 3)
        {
            isStartRecharging = true;
            StartCoroutine(AutoRechargeMagazine());
        }
    }

    public void RechargeGunDisable()
    {
        PlayerInput.rKeyPressed -= OnRKeyPressed; // ������������ �� ������� ����� ��� ������
    }

    public void OnRKeyPressed()
    {
        if (playerInput.gunNumber == 0 && currentAssaultBullets != 0 && !isStartRecharging && currentMagazineAssaultRiffle!= maximumMagazineAssaultRiffle 
            || playerInput.gunNumber == 1 && currentShotgunBullets != 0 && !isStartRecharging && currentMagazineShotguntRiffle !=maximumMagazineShotguntRiffle
            || playerInput.gunNumber ==2 && currentSniperBullets !=0 && !isStartRecharging && currentMagazineSniperRifle !=maximumMagazineSniperRifle
            || playerInput.gunNumber ==3 && currentMachinegunBullets !=0 && !isStartRecharging && currentMagazineMachinegun != maximumMagazineMachinegun) 
            //���� ����������� ���������� ������ �� �������� � ���������� �������� ���������� ������ �� ����� 0 � ���������� �������� � ��������
            // ���������� ������ �� ����� ������������� (�.�. ����������� �� �����) - �������� ��������
        {
            StartCoroutine(RechargeMagazine());
        }
        else
        {

        }
    }

   /// <summary>
   /// �������� ����������� (������� �������� ���� � �������������� ���������, �� ��������� ��� ������� ������� ������� R).
   /// </summary>
   /// <returns></returns>
    private IEnumerator RechargeMagazine()
    {
        if (playerInput.gunNumber == 0)
        {
            isStartRecharging = true;
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerAssaultRiffle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeAssaultBarUpdate(currentRechargeTimer, rechargeTimerAssaultRiffle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerAssaultRiffle)
                {
                    if (currentAssaultBullets >= maximumMagazineAssaultRiffle)
                    {
                        currentMagazineAssaultRiffle = maximumMagazineAssaultRiffle;
                    }
                    else if (currentAssaultBullets < maximumMagazineAssaultRiffle)
                    {
                        currentMagazineAssaultRiffle = currentAssaultBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 0)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (playerInput.gunNumber == 1)
        {
            isStartRecharging = true;
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerShotguntRiffle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeShotgunBarUpdate(currentRechargeTimer, rechargeTimerShotguntRiffle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerShotguntRiffle)
                {
                    if (currentShotgunBullets >= maximumMagazineShotguntRiffle)
                    {
                        currentMagazineShotguntRiffle = maximumMagazineShotguntRiffle;
                    }
                    else if (currentShotgunBullets < maximumMagazineShotguntRiffle)
                    {
                        currentMagazineShotguntRiffle = currentShotgunBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 1)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (playerInput.gunNumber == 2)
        {
            isStartRecharging = true;
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerSniperRifle)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeSniperBarUpdate(currentRechargeTimer, rechargeTimerSniperRifle);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerSniperRifle)
                {
                    if (currentSniperBullets >= maximumMagazineSniperRifle)
                    {
                        currentMagazineSniperRifle = maximumMagazineSniperRifle;
                    }
                    else if (currentSniperBullets < maximumMagazineSniperRifle)
                    {
                        currentMagazineSniperRifle = currentSniperBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 2)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        if (playerInput.gunNumber == 3)
        {
            isStartRecharging = true;
            currentRechargeTimer = 0;
            while (currentRechargeTimer < rechargeTimerMachinegun)
            {
                yield return null;
                currentRechargeTimer += Time.deltaTime;
                rechargingBar.RechargeMachinegunBarUpdate(currentRechargeTimer, rechargeTimerMachinegun);
                audioScript.PlayRechargeSound(playerInput.gunNumber); // ����� ��������������� ����� �� ������� AudioScript

                if (currentRechargeTimer >= rechargeTimerMachinegun)
                {
                    if (currentMachinegunBullets >= maximumMagazineMachinegun)
                    {
                        currentMagazineMachinegun = maximumMagazineMachinegun;
                    }
                    else if (currentMachinegunBullets < maximumMagazineMachinegun)
                    {
                        currentMagazineMachinegun = currentMachinegunBullets;
                    }
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 3)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // ����� ��������� ��������������� ����� �� ������� AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        yield break;
    }
}
