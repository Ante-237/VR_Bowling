using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    public SettingSO settings;
    public DuckPinSetup duckPinSetup;

    [Header("Events")]
    public VoidEventChannel SpawnBallsEvent;
    public VoidEventChannel OutOfRangeEvent;

    [Header("UI")]
    [SerializeField] private GameObject StarterPanel;
    [SerializeField] private GameObject CreditPanel;
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private GameObject StrikePanel;
    [Space(10)]
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button CreditBtn;
    [SerializeField] private Button ExitBtn;
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button ExitCreditScene;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private TextMeshProUGUI CurrentScoreText;
    [SerializeField] private TextMeshProUGUI PlayerName;
    [Space(10)]
    [SerializeField] private List<TextMeshProUGUI> CurrentScoreList;
    [SerializeField] private List<Transform> BallRollingPositions;
    [SerializeField] private Transform AnchorParentBalls;

    private bool roundUp = false;
    private int ballsFallen = 0;
    private int callingAmount = 0;

    private void OnEnable()
    {
        ExitBtn.onClick.AddListener(ExitGame);
        RestartBtn.onClick.AddListener(RestartGame);
        StartBtn.onClick.AddListener(ShowScorePanel);
        CreditBtn.onClick.AddListener(ShowCreditPanel);
        ExitCreditScene.onClick.AddListener(HideCreditPanel);

        SpawnBallsEvent.OnRaiseEvent += SpawnBalls;
        OutOfRangeEvent.OnRaiseEvent += CheckGameStatus;
    }
    
    private void Start()
    {
        // game start launch test


        // set the first score to 0 to avoid null reference error when adding other score.
        fillUpList();

    }

    private void fillUpList()
    {
        for (int i = 0; i < 10; i++)
        {
            settings.ScoreBoardList.Add(0);
        }
    }

 

    public void CheckGameStatus()
    {
        if (settings.GameStart)
        {
            Debug.Log("Call Number : " + callingAmount);

            if (roundUp)
            {
                settings.CurrentRound += 1;
                settings.currentShots = 0;
                roundUp = false;
            }

            if (settings.CurrentRound >= 0 && settings.CurrentRound < 10)
            {
                if (settings.currentShots < 2)
                {

                    // ensure round remains the same
                    roundUp = false;

                    // runtimely coroutine base on teh content in here considering all a ball was touch
                    foreach (GameObject ball in settings.loadedDuckPins)
                    {
                        if (ball.TryGetComponent(out DuckPin duckPin))
                        {
                            Debug.Log("Standing State : " + (duckPin.standstate == STANDING.NO ? "fallen" : "standing"));
                            // ballsFallen += duckPin.standstate == STANDING.NO ? 1 : 0;
                            if(duckPin.standstate == STANDING.NO)
                            {
                                ballsFallen++;
                            }
                        }
                    }


                    // what if this is the first show and  not all balls are take out, what if all balls are take out for a strike.
                    // double to next shot, or push to next round.
                    if (settings.currentShots == 0)
                    {
                        if (ballsFallen < 10)
                        {
                         
                            settings.CurrentScore += ballsFallen;
                            Debug.Log("Balls Standing Round 0 : Shot 0 => : " + ballsFallen);
                        }
                        else
                        {
                            // a strike complete // call the other stuff and update the UI
                            StartCoroutine(ShowScoreStrick());
                            Debug.Log("Balls Standing Round 0 : Shot 0 => :" + ballsFallen);
                            settings.CurrentScore += 20;
                            settings.currentShots = 2;
                            roundUp = true;
                            StartCoroutine(duckPinSetup.RunArrangements(settings.TimeBtwScoreUpdate));
                        }
                    }
                    
                    if (settings.currentShots == 1)
                    {
                        if (ballsFallen < 10)
                        {
                            settings.CurrentScore += ballsFallen;
                            Debug.Log("Balls Standing Round 0 : Shot 0 => :" + ballsFallen);
                            roundUp = true;
                            StartCoroutine(duckPinSetup.RunArrangements(settings.TimeBtwGameChecks));
                            settings.currentShots = 0;
                        }
                        else
                        {
                            settings.currentShots = 0;
                            roundUp = true;
                            StartCoroutine(duckPinSetup.RunArrangements(settings.TimeBtwGameChecks));
                        }
                    }
                    // add the final score to the current score
                    settings.ScoreBoardList[settings.CurrentRound] += settings.CurrentScore;
                    StartCoroutine(UpdateScoreBoard(settings.CurrentRound, settings.ScoreBoardList[settings.CurrentRound]));
                    settings.CurrentScore = 0;
                    ballsFallen = 0;
                    settings.currentShots += 1;


                }
                else
                {
                    roundUp = true;
                    settings.currentShots = 0;
                   
                    Debug.Log("The Current Shots Value Exited the Range");
                    // reset the duck pins here
                }
            }
            else
            {
                // run coroutine to show final game score
                StartCoroutine(ShowHighScore());
            } 
        }
    }

    // update score board
    IEnumerator UpdateScoreBoard(int index, int Score)
    {
        CurrentScoreList[index].text = "----\n" + "| " + settings.ScoreBoardList[index].ToString("00") + " |";
        yield return new WaitForSeconds(settings.TimeBtwScoreUpdate);
    }

    // score strick enum
    IEnumerator ShowScoreStrick()
    {
        StrikeState(true);
        yield return new WaitForSeconds(settings.TimeBtwScoreUpdate);
        StrikeState(false);
    }

    // update high score and show current status
    IEnumerator ShowHighScore()
    {
        yield return new WaitForSeconds(settings.TimeBtwScoreUpdate);
        ScorePanel.SetActive(false);
        StarterPanel.SetActive(true);
        int localTotal = 0;

        foreach (int t in settings.ScoreBoardList)
        {
            localTotal += t;
        }

        if (localTotal > settings.HighScore)
        {
            settings.HighScore = localTotal;
            HighScoreText.text = "High Score : " + settings.HighScore;
        }
        else
        {
            HighScoreText.text = "High Score : " + settings.HighScore;
        }

        CurrentScoreText.text = "Your Score : " + localTotal;


    }


    public void SpawnBalls()
    {
        for (int i = 0; i < BallRollingPositions.Count; i++) {
            settings.loadedBowlingBalls.Add(Instantiate(settings.bowlingBalls[Random.Range(0, settings.bowlingBalls.Count)], BallRollingPositions[i].position, Quaternion.identity, AnchorParentBalls));
        }
    }

    private void StrikeState(bool state)
    {
        StrikePanel.SetActive(state);
    }


    private void SetPlayerName()
    {
        PlayerName.text = settings.playerName;
    }

    private void ShowScorePanel()
    {
        ScorePanel.SetActive(true);
        StarterPanel.SetActive(false);
        SpawnBalls();
        SetPlayerName();
        settings.GameStart = true;
    }
  
    private void ShowCreditPanel()
    {
        CreditPanel.SetActive(true);
    }

    private void HideCreditPanel()
    {
        CreditPanel.SetActive(false);
        StarterPanel.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}

public enum STANDING
{
    YES,
    NO
}
