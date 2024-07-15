using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingSO")]
public class SettingSO : ScriptableObject
{


    [Header("Data")]
    public List<GameObject> loadedDuckPins;
    public List<GameObject> loadedBowlingBalls;

    [Space(5)]
    public int HighScore = 0;
    public int balls = 0;
    public int CurrentScore = 0;
    public int currentShots = 0;
    public int TotalShots = 0;
    public int CurrentRound = 0;
    public float TimeBtwScoreUpdate = 2.0f;

    [Space(5)]
    public List<int> ScoreBoardList = new List<int>(10);

    [Space(5)]
    public string playerName = "Player 1";

    [Header("Prefabs")]
    public List<GameObject> bowlingBalls;
    public GameObject duckPinPrefab;

   

    public void OnValidate()
    {

        //if(loadedDuckPins.Count > 9)
        //{
        //    loadedDuckPins.Clear();
        //}
    }

    public void OnEnable()
    {
        // add all default setup here 
        loadedDuckPins.Clear();
    }

}
