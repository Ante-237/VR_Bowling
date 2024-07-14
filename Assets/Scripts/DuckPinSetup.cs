using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPinSetup : MonoBehaviour
{
    [SerializeField] private GameObject duckPinPrefab;
    [SerializeField] private Transform duckPinSpawnStartPoint;
    [SerializeField] private Transform duckPinSpawnParent;
    [SerializeField][Range(0, 50f)] private float distanceBtwDuckPin = 30.5f;


    private const int numberOfDuckPin = 10;
    private GameObject duckPinInstance; 

    public void Start()
    {
        for(int i = 0; i < numberOfDuckPin; i++)
        {
            duckPinInstance = Instantiate(duckPinPrefab);
        }


    }


}
