using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargingBar : MonoBehaviour
{
    [SerializeField] private Image rechargeBar;


    private void Awake()
    {
        rechargeBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// ћетод, который принимает текущее значение здоровь€ (скрипт Health) и максимальное количество здоровь€ (скрипт Health). ¬ зависимости от текущего значени€ здоровь€ мен€етс€ заполнение healthBar 
    /// (картинки). ѕо умолчанию картинка полностью заполнена (значение 1). «начение 0.5 означает количество здоровь€, равное половине от 
    /// максимального (при 100 ’ѕ это 50 здоровь€). ƒеление на 100 необходимо при максимальном количестве здоровь€ равном 100 единицам.
    /// </summary>

    public void RechargeAssaultBarUpdate(float currentRechargeTimer, float rechargeTimerAssaultRiffle)
    {
        rechargeBar.gameObject.SetActive(true);
        rechargeBar.fillAmount = currentRechargeTimer / rechargeTimerAssaultRiffle;
    }

    public void RechargeShotgunBarUpdate(float currentRechargeTimer, float rechargeTimerShotguntRiffle)
    {
        rechargeBar.gameObject.SetActive(true);
        rechargeBar.fillAmount = currentRechargeTimer / rechargeTimerShotguntRiffle;
    }

    public void RechargeSniperBarUpdate(float currentRechargeTimer, float rechargeTimerSniperRifle)
    {
        rechargeBar.gameObject.SetActive(true);
        rechargeBar.fillAmount = currentRechargeTimer / rechargeTimerSniperRifle;
    }

    public void RechargeMachinegunBarUpdate(float currentRechargeTimer, float rechargeTimerMachinegun)
    {
        rechargeBar.gameObject.SetActive(true);
        rechargeBar.fillAmount = currentRechargeTimer / rechargeTimerMachinegun;
    }

    public void RechargeImage(bool isStartRecharging)
    {
        rechargeBar.gameObject.SetActive(isStartRecharging);
    }
}
