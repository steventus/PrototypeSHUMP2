using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Instance
    public static WaveManager instance = null;

    //Global Wave Properties
    public List<Wave> waves;
    private int waveNumber = 0;

    //Current Wave Properties
    [SerializeField] private List<GameObject> toBeSpawned;
    [SerializeField] private List<float> timeToSpawn;
    [SerializeField] private float currentEnemiesAlive = 0f;
    private bool endByTimer = false;
    private float waveDuration = 0f;

    //Internal time to clock and track time to spawn enemies;
    [SerializeField] private float waveTimer = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        //Deactivate everything first
        foreach (Wave _wave in waves)
        {
            foreach (GameObject _obj in _wave.objectToSpawn)
            {
                _obj.SetActive(false);
            }
        }





        InitialiseWave(waves[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //Activate them based on beatmap
        if (toBeSpawned.Count > 0 && waveTimer > timeToSpawn[0])
        {
            //Activate object
            toBeSpawned[0].SetActive(true);

            //Update GameManager
            GameManager.instance.enemiesAlive.Add(toBeSpawned[0]);

            //Remove the obj
            timeToSpawn.RemoveAt(0);
            toBeSpawned.RemoveAt(0);
        }


        //////////End wave either on timer or when all enemies die
        if (endByTimer && waveTimer >= waveDuration)
        {
            Debug.Log("Next wave initiating via timer");
            GameManager.instance.ClearRemainingEnemies();
        }

        /////////Continously update waveTimer;
        waveTimer += Time.deltaTime;
    }

    public void InitialiseWave(Wave _currentwave)
    {
        //Reset
        toBeSpawned.Clear();
        timeToSpawn.Clear();

        //Extract data from current wave
        foreach (GameObject _obj in _currentwave.objectToSpawn)
        {
            toBeSpawned.Add(_obj);
            _obj.SetActive(false);
        }

        foreach (float _time in _currentwave.timeToSpawn)
            timeToSpawn.Add(_time);

        UpdateLocalWaveProperties(toBeSpawned.Count, _currentwave.timerBased, _currentwave.durationOfWave);

        waves.Remove(_currentwave);

    }

    public void UpdateLocalWaveProperties(float _currentEnemiesAlive, bool _endByTimer, float _waveDuration)
    {
        currentEnemiesAlive = _currentEnemiesAlive;
        endByTimer = _endByTimer;
        waveDuration = _waveDuration;
    }

    

    public void ResetWave()
    {

    }

    public void NextWave()
    {
        if (endByTimer && waveTimer <= waveDuration)
        {
            Debug.Log("Prompt end wave too early");
            return;
        }


        waveNumber++;

        //Reset local wave Properties
        
        endByTimer = false;
        waveDuration = 0f;

        waveTimer = 0;
        InitialiseWave(waves[0]);
    }
        


}
