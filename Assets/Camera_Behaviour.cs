using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Behaviour : MonoBehaviour
{
    public static Camera_Behaviour instance = null;
    private Vector3 originalPos; //Assuming camera is fixed only

    //Mimic damped oscillator
    public float cameraShakeFrequency;
    public float cameraShakeDecayMultiplier;
    public float cameraShakeDecayThreshold;

    private float cameraShakeIntensity;
    private Camera thisCam;

    //Debug
    public float debugShakeIntensity;
    private float startShake;
    

    private enum State
    {
        idle,
        shake,
    }

    private State thisState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Initialise();
    }
    void Initialise()
    {
        thisCam = Camera.main;
        thisState = State.idle;

        originalPos = thisCam.transform.position; //Assuming camera is fixed only
    }

    void Update()
    {
        switch (thisState)
        {
            case State.idle:
                thisCam.transform.position = originalPos;

                if (Input.GetKeyDown(KeyCode.B))
                    Shake(debugShakeIntensity);

                break;

            case State.shake:

                #region Shaking
                //Change x-position - ie. camera shake only happen in x-direction
                float _xPosition = cameraShakeIntensity * Mathf.Sin(Time.time * cameraShakeFrequency);

                //Update position
                thisCam.transform.position = new Vector3(originalPos.x + _xPosition, thisCam.transform.position.y, thisCam.transform.position.z);
                #endregion

                #region Damping
                //Damping
                cameraShakeIntensity -= cameraShakeDecayMultiplier * Time.deltaTime;

                if (cameraShakeIntensity <= cameraShakeDecayThreshold)
                {
                    thisState = State.idle;
                    cameraShakeIntensity = 0;

                    //Debug
                    Debug.Log("Duration: "+(Time.time - startShake));
                }
                #endregion

                break;
        }



    }

    public void Shake(float _initialIntensity)
    {
        //Initialise
        cameraShakeIntensity = _initialIntensity;
        thisState = State.shake;

        //Debug
        startShake = Time.time;
        Debug.Log(startShake);
    }

}
