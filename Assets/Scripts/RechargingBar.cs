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
    /// �����, ������� ��������� ������� �������� �������� (������ Health) � ������������ ���������� �������� (������ Health). � ����������� �� �������� �������� �������� �������� ���������� healthBar 
    /// (��������). �� ��������� �������� ��������� ��������� (�������� 1). �������� 0.5 �������� ���������� ��������, ������ �������� �� 
    /// ������������� (��� 100 �� ��� 50 ��������). ������� �� 100 ���������� ��� ������������ ���������� �������� ������ 100 ��������.
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
