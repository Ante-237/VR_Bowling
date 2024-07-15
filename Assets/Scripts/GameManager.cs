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
        settings.ScoreBoardList.Add(0);
    
    }

    private bool roundUp = true;
    private int ballsStanding = 0;

    public void CheckGameStatus()
    {
        if (!roundUp)
        {
            settings.CurrentRound += 1;
            settings.currentShots = 0;
            
        }

  

        if(settings.CurrentRound >= 0 && settings.CurrentRound < 10)
        {
            if(settings.currentShots < 2)
            {

                // ensure round remains the same
                roundUp = true;

                // runtimely coroutine base on teh content in here considering all a ball was touch
                foreach(var ball in settings.loadedDuckPins)
                {
                    if(ball.TryGetComponent<DuckPin>(out DuckPin duckPin))
                    {
                        ballsStanding += duckPin.standstate == STANDING.NO ? 1 : 0;
                    }
                }


                // what if this is the first show and  not all balls are take out, what if all balls are take out for a strike.
                // double to next shot, or push to next round.
                if(settings.currentShots == 0)
                {
                    if(ballsStanding < 10)
                    {
                        settings.currentShots++;
                        settings.CurrentScore += ballsStanding;
                        Debug.Log("Balls Standing Round 0 : " + ballsStanding);
                    }
                }
                else
                {
                    // a strike complete // call the other stuff and update the UI
                    StartCoroutine(ShowScoreStrick());
                    Debug.Log("Balls Standing Round 0 : " + ballsStanding);
                    settings.CurrentScore += 20;
                    settings.currentShots = 2;
                   
                    roundUp = false;
                }

                if(settings.currentShots == 1)
                {
                    if(ballsStanding < 10)
                    {
                        settings.CurrentScore += ballsStanding;
                        Debug.Log("Balls Standing Round 0 : " + ballsStanding);
                    }
                    else
                    {
                        settings.currentShots = 0;
                        roundUp = false;
                    }
                }

                // add the final score to the current score
                settings.ScoreBoardList.Add(settings.ScoreBoardList[settings.CurrentRound] + settings.CurrentScore);
                UpdateScoreBoard(settings.CurrentRound, settings.ScoreBoardList[settings.CurrentRound + 1]);
                settings.CurrentScore = 0;


            }
            else
            {
                roundUp = false;
                settings.currentShots = 0;
                duckPinSetup.RunArrangements();
                // reset the duck pins here
            }
        }
        else
        {
            // run coroutine to show final game score
            StartCoroutine(ShowHighScore());
        }
    }

    // update score board
    IEnumerator UpdateScoreBoard(int index, int Score)
    {
        CurrentScoreList[index].text = "----\n" + "| " + settings.ScoreBoardList[index] + " |";
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
