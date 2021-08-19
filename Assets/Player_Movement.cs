using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Movement : Base_Entity
{
    public int speed;
    public int dashSpeed;

    private Rigidbody2D thisBody;

    private bool readyToPerfect = true;
    [SerializeField] private int currentPrimary = 0;
    [SerializeField] private int currentSecondary = 0;

    public float assaultRate;
    public float shotgunRate;
    public float shotgunBullets;
    public float chargeShotRate;
    public float forwardRocketRate;
    public float homingRocketRate;
    public float sawBladesRate;
    public float atomBombCount;

    private float secondaryCharge = 0;
    private float oldSecondaryCharge;

    private Player_EngineBehaviour thisEngineAnimator;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        thisBody = GetComponent<Rigidbody2D>();
        atomBombCount = 0;
        secondaryCharge = 0;
        thisEngineAnimator = GetComponent<Player_EngineBehaviour>();
    }

    void Update()
    {
        #region Movement
        float _inputx = Input.GetAxisRaw("Horizontal");
        float _inputy = Input.GetAxisRaw("Vertical");

        float _speedMultiplier;

        switch (Input.GetKey(KeyCode.LeftShift))
        {
            case true:
                thisBody.AddForce(new Vector2(_inputx, _inputy) * dashSpeed, ForceMode2D.Impulse);
                _speedMultiplier = 1f;
                break;

            default:
                //Default here means if not pressed.
                _speedMultiplier = speed;
                break;

        }

        //Final player speed
        thisBody.velocity = new Vector2(_inputx, _inputy) * _speedMultiplier;
        #endregion

        #region PlayerEngineAnimation
        //Player Engine Animation
        if (_inputy > 0)
            thisEngineAnimator.SetEnginePower(3); //Strong

        else if (_inputy < 0)
            thisEngineAnimator.SetEnginePower(1); //Weak

        else thisEngineAnimator.SetEnginePower(2); //Normal - default
        #endregion



        #region Fire
        //if (Input.GetKeyDown(KeyCode.Mouse0) && readyToPerfect)
        //{
        //    if (ConductorClass.instance.IfPressedOnBeat())
        //    {
        //        GetComponent<Behaviour_Fire>().ArcFire(30, 3);
        //        readyToPerfect = false;
        //    }
        //}
        //
        //if (Input.GetKeyUp(KeyCode.Mouse0) && !readyToPerfect)
        //    readyToPerfect = true;

        if (Input.GetKey(KeyCode.Mouse0) && (Time.time > GetComponent<Behaviour_Fire>().previousTimeFired + 1 / GetComponent<Behaviour_Fire>().fireRate))
            {
                GetComponent<Behaviour_Fire>().previousTimeFired = Time.time;

                switch (currentPrimary)
                {
                    case 0: //Assault
                        GetComponent<Behaviour_Fire>().Fire();
                        break;
                    case 1: //Shotgun
                        GetComponent<Behaviour_Fire>().ArcFire(10, shotgunBullets);
                        break;
                    case 2:
                        Debug.Log("Charging");
                        break;

                }

            }

        

        #endregion

        if (secondaryCharge >= 100 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            secondaryCharge = 0;
            GetComponentInChildren<Player_SecondaryBehaviour>().TriggerSecondary(currentSecondary);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            AtomBomb();

        //if (Input.GetKeyDown(KeyCode.Q))
        //    ChangePrimary();

        if (Input.GetKeyDown(KeyCode.E))
            ChangeSecondary();


        if (secondaryCharge != oldSecondaryCharge)
            FindObjectOfType<UIManager>().OnChangeCombo(secondaryCharge);


        oldSecondaryCharge = secondaryCharge;
    }

    public void FixedUpdate()
    {
        



    }




    public void AddCharge(float _number)
    {
        secondaryCharge++;

        if (secondaryCharge >= 100)
            secondaryCharge = 100;
    }

    public void ChangePrimary()
    {
        currentPrimary++;

        if (currentPrimary >= 3)
            currentPrimary = 0; //reset to Assault

        switch (currentPrimary)
        {
            case 0: //Assault
                GetComponent<Behaviour_Fire>().fireRate = assaultRate;
                break;

            case 1: //Shotgun
                GetComponent<Behaviour_Fire>().fireRate = shotgunRate;
                break;

            case 2: //Charge Shot
                GetComponent<Behaviour_Fire>().fireRate = chargeShotRate;
                break;

            default:
                currentPrimary = 0;
                break;
        }
        Debug.Log("Swapped to " + currentPrimary);
    }

    public void ChangeSecondary()
    {
        currentSecondary++;

        if (currentSecondary >= 4)
            currentSecondary = 0;

        switch (currentSecondary)
        {
            case 0: //Foward Rocket Burst
                break;

            case 1: //Homing Rocket
                break;

            case 2: //Rocket Flares
                break;

            case 3: //Sawblades
                break;

            default:
                currentPrimary = 0;
                break;
        }
    }


    public void AtomBomb()
    {
        Debug.Log("Atom Bomb bay");
    }


}
