using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    [HideInInspector] PauseSC pause;
    [SerializeField] SceneSC sceneMN = new SceneSC();
    [SerializeField] List<GameObject> subPanel = new List<GameObject>();
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject regisPanel;

    //Common Zone
    //[SerializeField] Image playerAvt;
    [SerializeField] Text playerNameText;
    [SerializeField] Text totalScoreText;
    [SerializeField] Text highestLevelText;
    [SerializeField] Text highestScoreText;

    [HideInInspector] int butActionState;
    //0 = show List game; 1 = show Menu
    [HideInInspector] sbyte menuSubState;
    //-1 = invisible; 0 = actions; 1 = setting; 2 = achivement; 3 = patrolReward
    [HideInInspector] sbyte gameStage;
    private void Start()
    {
        SettingStart();
        LoadUser();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HandlePanelVisible();
        }
    }
    private void SettingStart()
    {
        gameStage = -1;
        CheckFirstPlay();
    }

    #region Common Section
    private void ShowPanel(int panelOder, bool show) => subPanel[panelOder].SetActive(show);
    public void LoadUser()
    {
        LoadAvatar();
        LoadName();
        LoadHighestScore();
        LoadHighestLevel();
        LoadTotalScore();
    }
    private void LoadAvatar()
    {
        //Load PlayerPrefs for this
    }
    private void LoadName() => playerNameText.text = PlayerPrefs.GetString("PName");
    private void LoadTotalScore() => totalScoreText.text = PlayerPrefs.GetInt("PTotalScore").ToString();
    private void LoadHighestLevel() => highestLevelText.text = PlayerPrefs.GetInt("PHighestLevel").ToString();
    private void LoadHighestScore() => highestScoreText.text = PlayerPrefs.GetInt("PHighestScore").ToString();
    #endregion

    #region Inside Menu Switch Panels
    public void OnShowActions()
    {
        menuSubState = 0;
        HandlePanelVisible();
    }
    public void OnShowSetting()
    {
        menuSubState = 1;
        HandlePanelVisible();
    }
    public void OnShowAchiveMent()
    {
        menuSubState = 2;
        HandlePanelVisible();
    }
    public void OnShowPatrolReward()
    {
        menuSubState = 3;
        HandlePanelVisible();
    }
    private void HandlePanelVisible()
    {
        if(menuSubState != -1)
        {
            subPanel[menuSubState].SetActive(true);
            for (int i = 0; i < menuSubState; i++)
            {
                if (i != menuSubState)
                {
                    subPanel[i].SetActive(false);
                    butActionState = -1;
                }
            }
        }else if(menuSubState == -1)
        {
            Debug.Log("in case close panel");
            for(int i = 0; i < subPanel.Count; i++)
            {
                subPanel[i].SetActive(false);
                menuSubState = -1;
            }
        }
        
    }
    #endregion

    #region Change Scene
    public void ExitGame() => Application.Quit();
    #endregion

    #region Player Data Handle
    void CheckFirstPlay()
    {
        if (isFirstPlay())
        {
            regisPanel.SetActive(true);
        }else 
        {
            LoadUser();
            regisPanel.SetActive(false);
        }
    }
    private bool isFirstPlay()
    {
        if (PlayerPrefs.GetInt("HasPlayed", 0) == 0) { return true; }
        else { return false; }
    }
    public void ClearPlayerPrefs() => PlayerPrefs.DeleteAll();
    #endregion

    public void SetGameStage()
    {
        
    }
}
