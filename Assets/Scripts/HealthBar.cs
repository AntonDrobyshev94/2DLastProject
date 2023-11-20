using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image heartBar;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    /// <summary>
    /// ћетод, который принимает текущее значение здоровь€ (скрипт Health) и максимальное количество здоровь€ (скрипт Health). ¬ зависимости от текущего значени€ здоровь€ мен€етс€ заполнение healthBar 
    /// (картинки). ѕо умолчанию картинка полностью заполнена (значение 1). «начение 0.5 означает количество здоровь€, равное половине от 
    /// максимального (при 100 ’ѕ это 50 здоровь€). ƒеление на 100 необходимо при максимальном количестве здоровь€ равном 100 единицам.
    /// </summary>

    public void HealthBarUpdate(float maxHealth, float currentHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        heartBar.fillAmount = currentHealth / maxHealth;
    }
}