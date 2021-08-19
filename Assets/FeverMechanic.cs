using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverMechanic : MonoBehaviour
{
    public Slider feverMeter;
    public float maxFever = 100;
    public bool ifAutoFever = true;

    public float feverRegenRate;
    public float feverDrainRate;
    private float currentFever;
    private int feverIndex;

    private enum FeverState
    {
        neutral,
        fever
    }

    private FeverState thisState;


    void Start()
    {
        feverMeter.maxValue = maxFever;
    }

    void Update()
    {
        feverMeter.value = currentFever;

        if (Input.GetKeyDown(KeyCode.Mouse1) && currentFever >= maxFever)
            ChangeFeverState(true);

    }

    private void FixedUpdate()
    {
        switch (thisState)
        {
            case FeverState.neutral:
                if (feverIndex >= 60 / feverRegenRate)
                    AddFever(1);

                break;

            case FeverState.fever:
                if (feverIndex >= 60 / feverDrainRate)
                    DecreaseFever(1);

                break;
        }

        feverIndex++;

    }

    public void AddFever(float _number)
    {
        currentFever += _number;

        if (currentFever >= maxFever)
            currentFever = maxFever;

        if (currentFever >= maxFever && ifAutoFever == true)
            ChangeFeverState(true);
    }

    public void DecreaseFever(float _number)
    {
        currentFever -= _number;

        if (currentFever <= 0)
            ChangeFeverState(false);
    }

    private void ChangeFeverState(bool _state)
    {
        Player_Movement_3 _player = GameObject.Find("Player").GetComponent<Player_Movement_3>();

        switch (_state)
        {
            case true:
                _player.isFever(true);

                thisState = FeverState.fever;
                break;
            case false:
                _player.isFever(false);

                thisState = FeverState.neutral;
                break;

        }
    }
}
