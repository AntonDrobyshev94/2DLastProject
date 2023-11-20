using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBullet : MonoBehaviour
{
    [SerializeField] private float bullets;
    [SerializeField] private GameObject particleBulletsObject;
    private BulletsChecking bulletsChecking;
    private BulletsCounter bulletsCounter;
    private SpriteRenderer buletImage;
    [SerializeField] private GameObject bulletCanvas;
    [SerializeField] private Text bulletBonusTextInformation;
    [SerializeField] private Sprite[] spriteBullets;
    private int randomSpriteNumber;
    private int randomBulletsNumber;
    private AudioScript audioScript;
    private PlayerInput playerInput;
    
    /// <summary>
    /// �� ������ ����� ������� ������ ArrowCounterText � PlayerAdventurer � ���������� � ���������� GameObject
    /// </summary>
    private void Awake()
    {
        bulletsCounter = GameObject.FindObjectOfType<BulletsCounter>();
        bulletsChecking = GameObject.FindObjectOfType<BulletsChecking>();
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
        audioScript = GameObject.FindObjectOfType<AudioScript>();
        randomSpriteNumber = Random.Range(0, 4);
        buletImage = GetComponent<SpriteRenderer>();
        buletImage.sprite = spriteBullets[randomSpriteNumber];

        if (randomSpriteNumber == 0)
        {
            randomBulletsNumber = Random.Range(30, 100);
            bullets = randomBulletsNumber;
        }
        else if (randomSpriteNumber == 1)
        {
            randomBulletsNumber = Random.Range(10, 30);
            bullets = randomBulletsNumber;
        }
        else if (randomSpriteNumber == 2)
        {
            randomBulletsNumber = Random.Range(7, 20);
            bullets = randomBulletsNumber;
        }
        else if (randomSpriteNumber == 3)
        {
            randomBulletsNumber = Random.Range(100, 300);
            bullets = randomBulletsNumber;
        }
    }

    /// <summary>
    /// �������� �������� � ����� Player, ��� ������� � ������ BulletsChecking �������� currentbullets ������������ �������� �������� ��������. � ������ 
    /// BulletsCounter �������� bullets ������������ �������� bullets ��� ������ �� �����. ��� ���� �������� ������ �������� � ������� particle.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buletImage.sprite = null;
            bulletCanvas.SetActive(true);
            if (randomSpriteNumber ==0)
            {
                bulletsChecking.currentAssaultBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " �������� �� ��������� ��������";
            }
            if (randomSpriteNumber == 1)
            {
                bulletsChecking.currentShotgunBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " �������� �� ���������";
            }
            if (randomSpriteNumber == 2)
            {
                bulletsChecking.currentSniperBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " �������� �� ����������� ��������";
            }
            if (randomSpriteNumber == 3)
            {
                bulletsChecking.currentMachinegunBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " �������� �� ��������";
            }

            if(playerInput.gunNumber == randomSpriteNumber) // �������� ���������� �� ������� ������ ���� ��������� ������ ��������� � ���������,
                                                            // ������� ���������
            {
                bulletsCounter.UpdateText();
            }

            audioScript.BonusBullet();
            particleBulletsObject.SetActive(true);
        }
    }
}
