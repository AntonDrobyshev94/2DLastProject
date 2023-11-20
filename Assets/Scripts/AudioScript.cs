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
    /// ����� ������� ����� ��������, ������� ��������� � ���� int �������� indexNumber �� ������� PlayerInput � ��������� � AudioSource
    /// ���� AudioClipShoot �� ������� ������ � ���������. ����� ���������� ��� ���������������.
    /// </summary>
    /// <param name="indexNumber"></param>
    public void PlayShootSound(int indexNumber)
    {
        audioSoundsArray[3].clip = audioClipsShoot[indexNumber];
        audioSoundsArray[3].Play();
    }

    /// <summary>
    /// ����� ������� ����� �����������, ������� � ������, ���� AudioSource ��� �� ������� ��������� � ���� int �������� indexNumber �� ������� 
    /// PlayerInput � ��������� � AudioSource ���� AudioClipRecgarge�� ������� ������ � ������������. ����� ���������� ��� ���������������. 
    /// ������� ���������� ��� ��� ������������ ���������������.
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
    /// ����� ��������� ����� �����������, ������� ��������� � ���� int �������� indexNumber �� ������� 
    /// PlayerInput � ��������� � AudioSource ���� AudioClipRecharge �� ������� ������ � ������������. 
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
