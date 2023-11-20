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

    public float maximumMagazineAssaultRiffle = 30f; //максимальный размер магазина
    public float maximumMagazineShotguntRiffle = 2f;
    public float maximumMagazineSniperRifle = 1f;
    public float maximumMagazineMachinegun = 300f;

    public float rechargeTimerAssaultRiffle = 3f; //скорость перезарядки оружия
    public float rechargeTimerShotguntRiffle = 2f;
    public float rechargeTimerSniperRifle = 2f;
    public float rechargeTimerMachinegun = 9f;

    public float currentMagazineAssaultRiffle = 0f; //текущее количество патронов в магазине
    public float currentMagazineShotguntRiffle = 0f;
    public float currentMagazineSniperRifle = 0f;
    public float currentMagazineMachinegun = 0f;

    private float currentRechargeTimer; //текущая скорость перезарядки

    public bool isStartRecharging = false;

    /// <summary>
    /// На старте сцены ищется объект с именем ArrowCounterText и записывается в переменную GameObject
    /// </summary>
    private void Awake()
    {
        PlayerInput.rKeyPressed += OnRKeyPressed; // подписываемся на событие rKeyPressed
        currentAssaultBullets = startAssaultBullets;
        currentShotgunBullets = startShotgunBullets;
        currentSniperBullets = startSniperBullets;
        currentMachinegunBullets = startMachinegunBullets;

        bulletsCounter = GameObject.FindObjectOfType<BulletsCounter>(); // находим объект со скриптом BulletCounter и кешируем экземпляр данного скрипта
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
    /// Метод подсчета текущего количества пуль текущего выбранного оружия (оружие зависит от индекса массива gunNumber) - общее количество пуль и 
    /// количество пуль в магазине и их вывод на экран (скрипт BulletsCounter).
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
    /// Метод, который проверяет, есть ли патроны в магазине текущего выбранного оружия или их нет, и в зависимости от этого разрешает стрельбу или 
    /// запрещает (isHaveBullets = true / false). Также при наличии патронов запускает метод воспроизведения звуков стрельбы.
    /// </summary>
    public void IsAnyBullets()
    {
        if (playerInput.gunNumber == 0)
        {
            if (currentMagazineAssaultRiffle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 1)
        {
            if (currentMagazineShotguntRiffle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 2)
        {
            if (currentMagazineSniperRifle > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
        if (playerInput.gunNumber == 3)
        {
            if (currentMagazineMachinegun > 0)
            {
                audioScript.PlayShootSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript
                isHaveBullets = true;
            }
            else
                isHaveBullets = false;
        }
    }

    /// <summary>
    /// Корутина автоматической перезарядки магазина, которая производит проверку текущего выбранного оружия с условием количества патронов <=0.
    /// В случае совпадения условий устанавливает текущий таймер перезарядки currentRechargeTimer равным таймеру перезарядки текущего
    /// выбранного оружия (на-пример для штурмовой винтовки rechargeTimerAssaultRiffle), после чего запускает цикл с условием таймера
    /// currentRechargeTimer >0. Цикл отрабатывает каждый кадр, в котором происходит отсчет времени при помощи -=Time.Deltatime и проверка 
    /// двух условий: 
    /// 1. Проверка текущего таймера перезарядки - если таймер принимает значение <=0 (т.е. перезарядка закончилась), то запускает проверку 
    /// текущего количества патронов выбранного оружия и максимального количества патронов в магазине для текущего оружия. 
    /// Если текущее значение патронов >= максимального, то присваивает текущему количеству патронов в магазине максимальное количество, 
    /// если текущее значение патронов < максимального, то присваивает текущему количеству патронов в магазине ТЕКУЩЕЕ количество патронов 
    /// этого оружия (На-пример: currentMagazineAssaultRiffle = currentAssaultBullets);
    /// 2. Проверка на смену перезаряжаемого оружия. Если оружие, которое начало перезарядку сменилось (gunNumber поменял свое значение от 
    /// изначально проверяемого), то происходит возвращение текущего значения таймера перезарядки (на-пример: currentRechargeTimer = 
    /// rechargeTimerAssaultRiffle) и завершение цикла с последующим выходом из корутины посредством yield break;
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 0)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 1)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 2)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 3)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
    /// Метод, запускаемый выстрелом (вводом из скрипта PlayerInput), который проверяет наличие патронов в магазине в текущем выбранном оружии.
    /// Если количество патронов <= 0, то производит запуск корутины перезарядки магазина RechargeMagazine().
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
        PlayerInput.rKeyPressed -= OnRKeyPressed; // отписываемся от события после его вызова
    }

    public void OnRKeyPressed()
    {
        if (playerInput.gunNumber == 0 && currentAssaultBullets != 0 && !isStartRecharging && currentMagazineAssaultRiffle!= maximumMagazineAssaultRiffle 
            || playerInput.gunNumber == 1 && currentShotgunBullets != 0 && !isStartRecharging && currentMagazineShotguntRiffle !=maximumMagazineShotguntRiffle
            || playerInput.gunNumber ==2 && currentSniperBullets !=0 && !isStartRecharging && currentMagazineSniperRifle !=maximumMagazineSniperRifle
            || playerInput.gunNumber ==3 && currentMachinegunBullets !=0 && !isStartRecharging && currentMagazineMachinegun != maximumMagazineMachinegun) 
            //если перезарядка ВЫБРАННОГО оружия не началась и количество патронов ВЫБРАННОГО оружия не равно 0 и количество патронов в магазине
            // выбранного оружия НЕ РАВНО МАКСИМАЛЬНОМУ (т.е. перезарядка не нужна) - стартуем корутину
        {
            StartCoroutine(RechargeMagazine());
        }
        else
        {

        }
    }

   /// <summary>
   /// Корутина перезарядки (принцип действия схож с автоматической корутиной, но вызвается при условии нажатия клавиши R).
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 0)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 1)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 2)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
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
                audioScript.PlayRechargeSound(playerInput.gunNumber); // метод воспроизведения звука из скрипта AudioScript

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
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    bulletsCounter.UpdateText();
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                }
                if (playerInput.gunNumber != 3)
                {
                    currentRechargeTimer = 0;
                    audioScript.StopRechargeSound(playerInput.gunNumber); // метод остановки воспроизведения звука из скрипта AudioScript
                    isStartRecharging = false;
                    rechargingBar.RechargeImage(isStartRecharging);
                    yield break;
                }
            }
        }
        yield break;
    }
}
