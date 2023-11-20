using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsShoot;
    [SerializeField] private AudioClip[] audioClipsRecharge;
    [SerializeField] private AudioClip[] audioClipEndgame;
     public AudioSource[] audioSoundsArray;
    [SerializeField] private AudioSource volumeObject;

    private void Awake()
    {
        audioSoundsArray[6].clip = audioClipsShoot[0];
    }

    /// <summary>
    /// Метод запуска звука выстрела, который принимает в себя int значение indexNumber из скрипта PlayerInput и сохраняет в AudioSource
    /// клип AudioClipShoot из массива клипов с выстрелом. Далее происходит его воспроизведение.
    /// </summary>
    /// <param name="indexNumber"></param>
    public void PlayShootSound(int indexNumber)
    {
        audioSoundsArray[3].clip = audioClipsShoot[indexNumber];
        audioSoundsArray[3].Play();
    }

    /// <summary>
    /// Метод запуска звука перезарядки, который в случае, если AudioSource еще не запущен принимает в себя int значение indexNumber из скрипта 
    /// PlayerInput и сохраняет в AudioSource клип AudioClipRecgargeиз массива клипов с перезарядкой. Далее происходит его воспроизведение. 
    /// Условие необходимо для его однократного воспроизведения.
    /// </summary>
    /// <param name="indexNumber"></param>
    public void PlayRechargeSound(int indexNumber)
    {
        if(audioSoundsArray[4].isPlaying)
        {

        }
        else
        {
            audioSoundsArray[4].clip = audioClipsRecharge[indexNumber];
            audioSoundsArray[4].Play();
        }
    }

    /// <summary>
    /// Метод остановки звука перезарядки, который принимает в себя int значение indexNumber из скрипта 
    /// PlayerInput и сохраняет в AudioSource клип AudioClipRecharge из массива клипов с перезарядкой. 
    /// </summary>
    /// <param name="inexNumber"></param>
    public void StopRechargeSound(int inexNumber)
    {
        audioSoundsArray[4].clip = audioClipsRecharge[inexNumber];
        audioSoundsArray[4].Stop();
    }

    public void EnemyShootSound()
    {
        audioSoundsArray[6].Play();
    }

    public void PlayEnemyAttackSound()
    {
        audioSoundsArray[5].Play();
    }

    public void StopEnemyAttackSound()
    {
        audioSoundsArray[5].Stop();
    }

    public void BonusCoin()
    {
        audioSoundsArray[0].Play();
    }

    public void BonusHeal()
    {
        audioSoundsArray[1].Play();
    }
    
    public void BonusBullet()
    {
        audioSoundsArray[2].Play();
    }

    public void ButtonSound()
    {
        audioSoundsArray[7].Play();
    }

    public void LooseGame()
    {
        volumeObject.clip = audioClipEndgame[1];
        volumeObject.Play();
    }

    public void WinGame()
    {
        volumeObject.clip = audioClipEndgame[2];
        volumeObject.Play();
    }
}
