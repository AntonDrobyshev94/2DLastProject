using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject playerDeadPanel;
    private AudioScript audioScript;
    private Health playerObjectHealth;
    [SerializeField] private float damage;
    public bool isTriggerDamage;
    private bool isDead;
    private bool gasMaskEnabledBoolean;
    public delegate void MyDelegate();
    public static event MyDelegate GasClickButtonEvent;

    private void Awake()
    {
        playerObjectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        audioScript = GameObject.FindAnyObjectByType<AudioScript>();
    } 

    /// <summary>
    /// ������� ��������� ������� � ����� Player, ������� ������������� �� ����������. ���� ��� ������ �������, �� ���������� ����� ������ (damage > 250) 
    /// � ������� �������� ��������� GameOverCorutine(), ��� ����� � ���������� ������������ isAFall = true. ��� ����, ���� gasMaskEnabledBoolean=true (�.�. 
    /// ���� ����������), �� ��������� �����-������� GasClickButtonEvent �� ������� gasScript, ������� ��������� ����������. 
    /// ���� isAFall = false, �� ��� ������� ����, isTriggerDamage ��������� �������� true. ������������ �������� �� ������ ����������. ���� ����������
    /// gasMaskEnabledBoolean = true (�����), �� ������ �� ������, ���� false (�.�. �� �����), ���������� ������� ���� �� �������� damage, ��� ���� ����������
    /// �������� TikDamage. ����� ����������� ����� GasActive, ������� �������� ������ ��� ����������� �������� ����.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gasMaskEnabledBoolean)
            {
                GasClickButtonEvent();
                playerObjectHealth.TakeDamage(damage);
                StartCoroutine(GameOverCoroutine());
            }
            else
            {
                isTriggerDamage = true;
                playerObjectHealth.TakeDamage(damage);
                StartCoroutine(GameOverCoroutine());
            }

        }
    }


    public IEnumerator GameOverCoroutine()
    {
        if(!isDead)
        {
            isDead = true;
            yield return new WaitForSeconds(2);
            audioScript.LooseGame();
            playerDeadPanel.SetActive(true);
        }
    }
}
