using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class PlayerRegisSC : MonoBehaviour
{
    [SerializeField] InputField usernameField;
    [SerializeField] JumpManager mn;

    [HideInInspector] string playerName;
    private void Start() => mn = GameObject.Find("RunningManager").GetComponent<JumpManager>();
    public void OnSubmitLocal()
    {
        playerName = usernameField.text;
        PlayerPrefs.SetString("PName", playerName);
        SettingNewPlayer();
    }
    public void OnLogByGG() 
    {
        //Get player name from GG
        PlayerPrefs.SetString("PName", playerName);
        SettingNewPlayer();
    }
    public void OnLogByFB() 
    {
        //Get player name from Facebook
        PlayerPrefs.SetString("PName", playerName);
        SettingNewPlayer();
    }
    private void SettingNewPlayer()
    {
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.SetInt("PTotalScore", 0);
        PlayerPrefs.SetInt("PHighestScore", 0);
        PlayerPrefs.SetInt("PHighestLevel", 0);
        PlayerPrefs.SetInt("soundState", 1);
        PlayerPrefs.SetInt("sfxState", 1);
        mn.LoadUser();
        gameObject.SetActive(false);
        
    }
}
