using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GasScript : MonoBehaviour
{
    public bool isTriggerDamage;
    [SerializeField] private GasMaskScript gasMaskScript;

    public static event Action<bool> triggerChanged; // публичное статичное событие triggerChanged, передающее значение bool переменной isTriggerDamage;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggerDamage = true;
            triggerChanged?.Invoke(isTriggerDamage); // если у bool переменной isTriggerDamage события triggerChanged есть подписчики, то передает значение. 
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
            triggerChanged?.Invoke(isTriggerDamage); // если у bool переменной isTriggerDamage события triggerChanged есть подписчики, то передает значение. 
            gasMaskScript.GasStop();
        }
    }
}
