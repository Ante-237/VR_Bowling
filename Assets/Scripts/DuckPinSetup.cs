using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class DuckPinSetup : MonoBehaviour
{
    [Header("SO")]
    public SettingSO settings;

    [SerializeField] private Transform duckPinSpawnStartPoint;
    [SerializeField] private Transform duckPinSpawnParent;
    [SerializeField][Range(0, 1f)] private float distanceBtwDuckPin = 0.305f;
   

    private const int numberOfDuckPin = 10;
    private GameObject duckPinInstance;
    private Vector3 currentUsePosition;

    private bool secondLaneStart = false;
    private bool thirdLaneStart = false;
    private bool fourthLaneStart = false;
    private int i = 0;
    


    public void Start()
    {
        StartCoroutine(RunArrangements(2));
    }

    public void runSetup(float timeStuff)
    {
        StartCoroutine(RunArrangements(timeStuff));
    }

    public IEnumerator RunArrangements(float waitTime)
    {

        if (settings.loadedDuckPins.Count > 0)
        {
            foreach (GameObject obj in settings.loadedDuckPins)
            {
                Destroy(obj);
            }
            settings.loadedDuckPins.Clear();
            secondLaneStart = false;
            thirdLaneStart = false;
            fourthLaneStart = false;
        }
        yield return new WaitForSeconds(waitTime);
   

        currentUsePosition = duckPinSpawnStartPoint.position;

        for (i = 0; i < numberOfDuckPin; i++)
        {
            // first actual position
            if (i == 0)
            {
                duckPinInstance = Instantiate(settings.duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                settings.loadedDuckPins.Add(duckPinInstance);
                continue;
            }

            // first lane
            if (i > 0 && i < 4)
            {
                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(settings.duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                settings.loadedDuckPins.Add(duckPinInstance);
                continue;
            }

            if (i > 3 && i < 7)
            {

                if (secondLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - ((distanceBtwDuckPin * 4) - (distanceBtwDuckPin * 0.5f)), currentUsePosition.y, currentUsePosition.z - distanceBtwDuckPin);
                    secondLaneStart = true;
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(settings.duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                settings.loadedDuckPins.Add(duckPinInstance);
                continue;
            }

            if (i > 6 && i < 9)
            {
                if (thirdLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - ((distanceBtwDuckPin * 3)) + (distanceBtwDuckPin * 0.5f), currentUsePosition.y, currentUsePosition.z - (distanceBtwDuckPin));
                    thirdLaneStart = true;
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(settings.duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                settings.loadedDuckPins.Add(duckPinInstance);
                continue;
            }

            if (i > 8)
            {
                if (fourthLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - (distanceBtwDuckPin * 2) + (distanceBtwDuckPin * 0.5f), currentUsePosition.y, currentUsePosition.z - (distanceBtwDuckPin));
                    fourthLaneStart = true;
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(settings.duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                settings.loadedDuckPins.Add(duckPinInstance);
                continue;
            }

        }
    }
}
