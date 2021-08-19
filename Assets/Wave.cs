using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //List of Objects to spawn
    public List<GameObject> objectToSpawn;
    public List<float> timeToSpawn;

    //Wave Properties
    public bool timerBased = false;
    public float durationOfWave = 0f;
}
