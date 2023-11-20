using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GasScript : MonoBehaviour
{
    public bool isTriggerDamage;
    [SerializeField] private GasMaskScript gasMaskScript;

    public static event Action<bool> triggerChanged; // ��������� ��������� ������� triggerChanged, ���������� �������� bool ���������� isTriggerDamage;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggerDamage = true;
            triggerChanged?.Invoke(isTriggerDamage); // ���� � bool ���������� isTriggerDamage ������� triggerChanged ���� ����������, �� �������� ��������. 
            if (gasMaskScript.isGasMaskEnabled)
            {
            }
            else
            {
                gasMaskScript.GasActive();
                StartCoroutine(gasMaskScript.TikDamage());
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTriggerDamage = false;
            triggerChanged?.Invoke(isTriggerDamage); // ���� � bool ���������� isTriggerDamage ������� triggerChanged ���� ����������, �� �������� ��������. 
            gasMaskScript.GasStop();
        }
    }
}
