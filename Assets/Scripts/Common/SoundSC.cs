using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource theme;

    private int pMusic; //This variable handle communicate with PlayerPrefs
    private int pSFX; //This variable handle communicate with PlayerPrefs
    private bool isAllowSound;
    private bool isAllowSFX;
    private void Start()
    {
        pMusic = PlayerPrefs.GetInt("");
        pSFX = PlayerPrefs.GetInt("");
        CheckPlayerMusic();
        CheckPlayerSFX();
    }

    private void CheckPlayerMusic()
    {
        if (pMusic == 0) isAllowSound = false;
        else if (pMusic == 1)
        {
            isAllowSound = true;
            PlayTheme();
        }

    }
    private void CheckPlayerSFX()
    {
        if (pSFX == 0) isAllowSFX = false;
        else if (pSFX == 1)
        {
            isAllowSFX = true;
            PlaySFX();
        }
    }

    public void UpdateMusic(bool isAllow)
    {
        if (isAllow == false)
        {
            MuteTheme();
        }
        else PlayTheme();
    }
    public void UpdateSFX(bool isAllow)
    {
        if (isAllow == false)
        {
            MuteSFX();
        }
        else PlaySFX();
    }

    public void PlayTheme() => theme.Play();
    public void MuteTheme() => theme.Pause();
    public void PlaySFX() => isAllowSFX = true;
    public void MuteSFX() => isAllowSFX = false;
}
