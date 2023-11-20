using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsCounter : MonoBehaviour
{
    private Text bulletsCounterText;
    public float bullets;
    public float bulletsMagazine;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private BulletsChecking bulletsChecking;
    [SerializeField] private Text magazineBulletsCounterText;
    [SerializeField] private Image bulletImage;
    [SerializeField] private Sprite[] spriteBulletArray;
 
    private void Start()
    {
        bulletsCounterText = GetComponent<Text>();
        bullets = bulletsChecking.currentAssaultBullets;
        bulletsCounterText.text = bullets + "";
        bulletsMagazine = bulletsChecking.currentMagazineAssaultRiffle;
        magazineBulletsCounterText.text = bulletsMagazine + "";
    }

    /// <summary>
    /// Вывод количества стрел на экран
    /// </summary>
    public void UpdateText()
    {
        if(playerInput.gunNumber==0)
        {
            bulletImage.sprite = spriteBulletArray[0];
            bullets = bulletsChecking.currentAssaultBullets;
            bulletsMagazine = bulletsChecking.currentMagazineAssaultRiffle;
            bulletsCounterText.text = bullets + "";
            magazineBulletsCounterText.text = bulletsMagazine + "";
        }
        if (playerInput.gunNumber == 1)
        {
            bulletImage.sprite = spriteBulletArray[1];
            bullets = bulletsChecking.currentShotgunBullets;
            bulletsMagazine = bulletsChecking.currentMagazineShotguntRiffle;
            bulletsCounterText.text = bullets + "";
            magazineBulletsCounterText.text = bulletsMagazine + "";
        }
        if (playerInput.gunNumber == 2)
        {
            bulletImage.sprite = spriteBulletArray[2];
            bullets = bulletsChecking.currentSniperBullets;
            bulletsMagazine = bulletsChecking.currentMagazineSniperRifle;
            bulletsCounterText.text = bullets + "";
            magazineBulletsCounterText.text = bulletsMagazine + "";
        }
        if (playerInput.gunNumber == 3)
        {
            bulletImage.sprite = spriteBulletArray[3];
            bullets = bulletsChecking.currentMachinegunBullets;
            bulletsMagazine = bulletsChecking.currentMagazineMachinegun;
            bulletsCounterText.text = bullets + "";
            magazineBulletsCounterText.text = bulletsMagazine + "";
        }
    }
}
