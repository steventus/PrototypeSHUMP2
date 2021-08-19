using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float comboMeter;
    private float oldComboState; //0, 1,2,3,4 for D, C, B, A, S respectively
    public List<float> comboThresholds = new List<float>(4) { 10,20,40,80 }; //Start at D. C,B,A,S thresholds respectively


    //Interface with Wave Manager
    public List<GameObject> enemiesAlive;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        



    }

    public void UpdateEnemiesAlive(GameObject _obj)
    {
        if (enemiesAlive.Contains(_obj))
            enemiesAlive.Remove(_obj);

        else
        {
            Debug.Log("Unknown enemy detected");
            return;
        }

        if (enemiesAlive.Count == 0)
        {
            WaveManager.instance.NextWave();
        }


    }

    public void ClearRemainingEnemies()
    {
        foreach (GameObject _obj in enemiesAlive)
        {
            _obj.SetActive(false);
            enemiesAlive.Remove(_obj);
        }

        WaveManager.instance.NextWave();
    }

}
