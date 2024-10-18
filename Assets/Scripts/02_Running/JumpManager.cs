using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class JumpManager : MonoBehaviour
{
    //No. of Game" #01
    //Rule:
    //Player jump to avoid obstacles, ì hit obstacle, game end.

    [SerializeField] Canvas MenuCan, GameplayCan;

    //Main menu canvas & Panels
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject miscPanel;
    [SerializeField] GameObject dailyRewardPanel;
    [SerializeField] GameObject AchievementPanel;
    [SerializeField] GameObject regisPanel;
    [HideInInspector] PauseSC pause;

    //Common Zone
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    [SerializeField] Text totalScore;
    [SerializeField] Text currentScore;
    [SerializeField] Text currentLevel;
    [SerializeField] Text startCoundownTxt;
    [SerializeField] Text playerNameTxt;
    [SerializeField] GameObject coundonwPanel;


    //Sepcific Zone
    [SerializeField] GameObject ground;
    [SerializeField] List<GameObject> cloundList = new List<GameObject>();
    [SerializeField] List<GameObject> listObts = new List<GameObject>();
    [SerializeField] JumpDinoSC dino;
    [SerializeField] Transform gamezone;
    [SerializeField] Text currentTimeSurvive;
    [SerializeField] GameObject obstacleSpawner;

    private Vector3 spawnerPos;
    private int objApparance;
    private int ingameScore;
    private int timeSurvive;
    private int curLevel;
    private int nextLvlTarget;
    private float delaySpawnTime;
    private int coundownNumber;
    private int gameStage;

    #region Old Source

    void Start()
    {
        SettingStartStats();
        UpdateGameState(0);

        //#region Countdown Start
        //HandleCoundownStartText();
        //if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        //else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        //#endregion

        DecideTimeSpawn(curLevel);
        StartCoroutine(SpawnClound());
        StartCoroutine(GroundMovement());
        StartCoroutine(OnWaitToSpawnObstacle(delaySpawnTime));

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void SettingStartStats()
    {
        gameStage = -1;
        coundownNumber = 5;
        ingameScore = 0;
        timeSurvive = 0;
        curLevel = 1;
        nextLvlTarget = 10;
        currentLevel.text = 1.ToString();
        currentScore.text = 0.ToString();
        spawnerPos = obstacleSpawner.transform.position;
        CheckFirstPlay();
    }

    //#region UIs Handlers
    private void HandleCoundownStartText() => startCoundownTxt.text = coundownNumber.ToString();
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString() + "s";
    private void IncreaseUIScore() => currentScore.text = ingameScore.ToString();
    private void IncreaseUILevel() => currentLevel.text = curLevel.ToString();

    //#endregion

    //#region Gameplay Handlers
    private IEnumerator SpawnClound()
    {
        yield return new WaitForSeconds(2);
        int randY = Random.Range(-1, 5);
        Instantiate(cloundList[Random.Range(0, 2)], new Vector3(4, randY, 0), Quaternion.identity);
        StartCoroutine(SpawnClound());
    }
    private IEnumerator GroundMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameStage == 1 || gameStage == 0 || gameStage == -1)
        {
            if (ground.transform.position.x <= -8)
            {
                ground.transform.position = new Vector3(8, -4, 0);
            }
            else
            {
                ground.transform.position += Vector3.left;
            }
        }
        else if (gameStage == 2)
        {
            StopCoroutine(GroundMovement());
        }
        StartCoroutine(GroundMovement());
    }
    private void DecideTimeSpawn(int lvl)
    {
        if (lvl == 1) delaySpawnTime = 5f;
        else if (lvl > 1 && lvl <= 21) delaySpawnTime -= 0.1f;
        else lvl = Random.Range(2, 5);
    }
    //#endregion

    //#region Handle Gameplay Activities
    public void OnJump()
    {
        if (dino.isGrounded == true)
        {
            dino.isGrounded = false;
            dino.allowJump = true;
        }
    }
    private IEnumerator OnWaitToSpawnObstacle(float delay)
    {
        if(gameStage == 1)
        {
            yield return new WaitForSeconds(delaySpawnTime);
            if (curLevel < 10) objApparance = Random.Range(0, listObts.Count - 3);
            else objApparance = Random.Range(0, listObts.Count);

            if (objApparance == 10 || objApparance == 9 || objApparance == 11) Instantiate(listObts[objApparance], new Vector3(spawnerPos.x, Random.Range(-2, 2), spawnerPos.z), Quaternion.Euler(0, 180, 0));
            else Instantiate(listObts[objApparance], spawnerPos, Quaternion.identity);

            StartCoroutine(OnWaitToSpawnObstacle(delay));
        }
    }

    private void UpdtatePlayerPrefs()
    {
        //Get player prefs section
        int currenTotalScore;
        int highestScoreToCompare;
        int highestLevelToCompare;

        int newTotalScore;

        currenTotalScore = PlayerPrefs.GetInt("PTotalScore");
        highestLevelToCompare = PlayerPrefs.GetInt("PHighestLevel");
        highestScoreToCompare = PlayerPrefs.GetInt("PHighestScore");

        //Update total score
        newTotalScore = currenTotalScore + ingameScore;
        PlayerPrefs.SetInt("PTotalScore", ingameScore); //total of score that player have earn

        //Update highest score
        if (highestScoreToCompare < ingameScore) PlayerPrefs.SetInt("PHighestScore", ingameScore); //highest score that player can reach of all games

        //Update highets level
        if (highestLevelToCompare < curLevel) PlayerPrefs.SetInt("PHighestLevel", curLevel); //highest level player can reach
    }
    //#endregion
    #endregion

    public void LoadUser()
    {
        UpdtatePlayerPrefs();
        UpdateUIPlayerInfo();
    }
    public void UpdateUIPlayerInfo()
    {
        playerNameTxt.text = PlayerPrefs.GetString("PName").ToString();
        totalScore.text = PlayerPrefs.GetInt("PTotalScore").ToString();
        currentScore.text = PlayerPrefs.GetInt("PHighestScore").ToString();
        currentLevel.text = PlayerPrefs.GetInt("PHighestLevel").ToString();
    }
    private void CheckFirstPlay()
    {
        if (isFirstPlay()) { regisPanel.SetActive(true); }
        else
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

    public void OnPlayGame()
    {
        MenuCan.gameObject.SetActive(false);
        GameplayCan.gameObject.SetActive(true);
        gameStage = 0;

        HandleCoundownStartText();
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
    }

    public void UpdateGameState(int state)
    {
        gameStage = state;
        switch (gameStage)
        {
            case 1:
                DecideNextLevelTarget();
                coundonwPanel.SetActive(false);
                StartCoroutine(CountToScore());
                break;
            case 2:
                ShowPause();
                StopAllCoroutines();
                break;
        }
    }

    private void DecideNextLevelTarget() => nextLvlTarget = (curLevel * 10) + curLevel * 2;
    private void IncreaseInGameScore() => ingameScore++;
    private void IncreaseInGameLevel() => curLevel++;
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    private IEnumerator CountToScore()
    {
        yield return new WaitForSeconds(1);
        timeSurvive++;
        IncreaseInGameScore();
        IncreaseUIScore();
        if (ingameScore == nextLvlTarget)
        {
            IncreaseInGameLevel();
            DecideTimeSpawn(curLevel);
            DecideNextLevelTarget();
            IncreaseUILevel();

            UpdtatePlayerPrefs();
        }
        UpdateOnScreenSecond();
        GroundMovement();
    }

    private IEnumerator StartCoundown()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        if (coundownNumber <= 0)
        {
            UpdateGameState(1);
            StopCoroutine(StartCoundown());
        }
        StartCoroutine(StartCoundown());
    }

    public void BackToHome()
    {
        gameStage = -1;
        GameplayCan.gameObject.SetActive(false);
        MenuCan.gameObject.SetActive(true);
    }

    #region UIs Mainmenus
    public void OnOption()
    {
        if(optionPanel.activeSelf == true)
        {
            optionPanel.gameObject.SetActive(false);
        }
        else { optionPanel.gameObject.SetActive(true); }
    }
    public void OnMisc()
    {
        if (miscPanel.activeSelf == true)
        {
            miscPanel.gameObject.SetActive(false);
        }
        else { miscPanel.gameObject.SetActive(true); }
    }
    public void OnDailyReward()
    {
        if (dailyRewardPanel.activeSelf == true)
        {
            dailyRewardPanel.gameObject.SetActive(false);
        }
        else { dailyRewardPanel.gameObject.SetActive(true); }
    }
    public void OnAchivement()
    {
        if (AchievementPanel.activeSelf == true)
        {
            AchievementPanel.gameObject.SetActive(false);
        }
        else { AchievementPanel.gameObject.SetActive(true); }
    }
    #endregion
}
