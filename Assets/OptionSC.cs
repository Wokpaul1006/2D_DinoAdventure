using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSC : MonoBehaviour
{
    [HideInInspector] SoundSC soundMN;
    [SerializeField] Toggle musicTogg, sfxTogg, vibraTogg;
    private PauseSC pause;
    void Start()
    {
        pause = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
        soundMN = GameObject.Find("RunningManager").GetComponent<SoundSC>();
    }

    //Button function section
    public void OnClearPlayerPrefs()
    {
        //Visible confirm panel
        PlayerPrefs.DeleteAll();
    }
    public void OnAccountMN() => Application.OpenURL("https://sadekgame.wordpress.com/"); //Replace this link by account manager link
    public void OnExit() => Application.Quit();
    public void UpdateMusic() => soundMN.UpdateMusic(musicTogg.isOn);
    public void UpdateSFX() => soundMN.UpdateSFX(musicTogg.isOn);
    public void UpdateVibratiion()
    {
        if (vibraTogg.isOn == true)
        {
            //Enable Vibrate script
        }
        else
        {
            //Disable vibrate script
        }
    }
}
