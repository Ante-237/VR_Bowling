using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class DuckPinSetup : MonoBehaviour
{
    [SerializeField] private GameObject duckPinPrefab;
    [SerializeField] private Transform duckPinSpawnStartPoint;
    [SerializeField] private Transform duckPinSpawnParent;
    [SerializeField][Range(0, 1f)] private float distanceBtwDuckPin = 0.305f;


    private const int numberOfDuckPin = 10;
    private GameObject duckPinInstance;
    private Vector3 currentUsePosition;

    private bool secondLaneStart = false;
    private bool thirdLaneStart = false;
    private bool fourthLaneStart = false;

    public void Start()
    {
        currentUsePosition = duckPinSpawnStartPoint.position;
        for(int i = 0; i < numberOfDuckPin; i++)
        {
            // first actual position
            if(i == 0)
            {
                duckPinInstance = Instantiate(duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                continue;
            }

            // first lane
            if( i > 0 && i < 4)
            {
                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                continue;
            }

            if( i > 3 && i < 7)
            {

                if(secondLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - ((distanceBtwDuckPin * 4) - (distanceBtwDuckPin * 0.5f)), currentUsePosition.y, currentUsePosition.z - distanceBtwDuckPin);
                    secondLaneStart = true;
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                continue;
            }

            if( i > 6 && i < 9)
            {
                if(thirdLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - ((distanceBtwDuckPin * 3)) + (distanceBtwDuckPin * 0.5f), currentUsePosition.y, currentUsePosition.z - (distanceBtwDuckPin));
                    thirdLaneStart = true;                
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                continue;
            }

            if(i > 8)
            {
                if (fourthLaneStart == false)
                {
                    currentUsePosition = new Vector3(currentUsePosition.x - (distanceBtwDuckPin * 2) + (distanceBtwDuckPin * 0.5f), currentUsePosition.y, currentUsePosition.z - (distanceBtwDuckPin));
                    fourthLaneStart = true;
                }

                currentUsePosition = new Vector3(currentUsePosition.x + distanceBtwDuckPin, currentUsePosition.y, currentUsePosition.z);
                duckPinInstance = Instantiate(duckPinPrefab, currentUsePosition, Quaternion.identity, duckPinSpawnParent);
                continue;
            }

        }


    }


}
