using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float positionUpdateRate = 0.05f;
    private float lastPositionUpdateTime;
    private int timeToWait = 3;

    public Transform[] spawnPoint;

    public List<CarController> cars = new List<CarController>();

    private void Awake()
    {
        instance = this;
    }   

    private void Update()
    {
        if(Time.time - lastPositionUpdateTime > positionUpdateRate)
        {
            lastPositionUpdateTime = Time.time;

            UpdateCarRacePositions();
            
        }
        Invoke("StartCountdown", 5.0f);
    }

    void UpdateCarRacePositions()
    {
        cars.Sort(SortPosition);
        for(int x = 0; x < cars.Count; x++)
        {
            cars[x].racePosition = cars.Count - x;
        }
    }

    void StartCountdown()
    {
        if (timeToWait != 0)
        {
            timeToWait--;
        }
        else
        {
            Debug.Log("Allow movement");
        }
    }

    int SortPosition ( CarController a, CarController b)
    {
        if (a.zonePassed > b.zonePassed)
            return 1;
        else if (b.zonePassed > a.zonePassed)
            return -1;

        float aDist = Vector3.Distance(a.transform.position, a.curTrackZone.transform.position);
        float bDist = Vector3.Distance(b.transform.position, b.curTrackZone.transform.position);

        return aDist > bDist ? 1 : -1;
    }
}
