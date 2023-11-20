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
    /// �����, ������� ��������� ������� �������� �������� (������ Health) � ������������ ���������� �������� (������ Health). � ����������� �� �������� �������� �������� �������� ���������� healthBar 
    /// (��������). �� ��������� �������� ��������� ��������� (�������� 1). �������� 0.5 �������� ���������� ��������, ������ �������� �� 
    /// ������������� (��� 100 �� ��� 50 ��������). ������� �� 100 ���������� ��� ������������ ���������� �������� ������ 100 ��������.
    /// </summary>

    public void HealthBarUpdate(float maxHealth, float currentHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        heartBar.fillAmount = currentHealth / maxHealth;
    }
}