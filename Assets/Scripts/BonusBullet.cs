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
    /// На старте сцены находим объект ArrowCounterText и PlayerAdventurer и записываем в переменные GameObject
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
    /// Проверка коллизии с тегом Player, при которой в скрипт BulletsChecking значению currentbullets прибавляется значение бонусных патронов. В скрипт 
    /// BulletsCounter значению bullets прибавляется значение bullets для вывода на экран. При этом бонусный объект исчезает с эфектом particle.
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
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " патронов от штурмовой винтовки";
            }
            if (randomSpriteNumber == 1)
            {
                bulletsChecking.currentShotgunBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " патронов от дробовика";
            }
            if (randomSpriteNumber == 2)
            {
                bulletsChecking.currentSniperBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " патронов от снайперской винтовки";
            }
            if (randomSpriteNumber == 3)
            {
                bulletsChecking.currentMachinegunBullets += bullets;
                bulletsCounter.bullets += bullets;
                bulletBonusTextInformation.text = "+ " + randomBulletsNumber + " патронов от минигана";
            }

            if(playerInput.gunNumber == randomSpriteNumber) // апдейтим информацию на канвасе только если выбранное оружие совпадает с патронами,
                                                            // которые подбираем
            {
                bulletsCounter.UpdateText();
            }

            audioScript.BonusBullet();
            particleBulletsObject.SetActive(true);
        }
    }
}
