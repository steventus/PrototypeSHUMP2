using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Movement_3 : Base_Entity
{
    public int speed;
    public float dashSpeed;
    public float dashDecayMultiplier;
    public float dashThreshold;

    private Rigidbody2D thisBody;
    private Vector3 moveDir;
    private Vector3 dashDir;
    private float finalDashSpeed;

    private bool readyToPerfect = true;
    [SerializeField] private int currentPrimary = 0;
    [SerializeField] private int currentSecondary = 0;

    public float assaultRate;
    [HideInInspector] public enum State
    {
        neutral,
        dashing,
        afterparry,
        dead
    }

    private bool isInvulnerable = false;
    [HideInInspector] public State thisState;

    private Player_EngineBehaviour thisEngineAnimator;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        thisBody = GetComponent<Rigidbody2D>();
        thisEngineAnimator = GetComponent<Player_EngineBehaviour>();

        finalDashSpeed = dashSpeed;

        ReturnToNeutral();
    }

    void Update()
    {
        switch (thisState)
        {
            case State.neutral:

                #region Movement
                float _inputx = Input.GetAxisRaw("Horizontal");
                float _inputy = Input.GetAxisRaw("Vertical");
                moveDir = new Vector2(_inputx, _inputy) * speed;


                //Receive input for rolling
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dashDir = moveDir;

                    if (dashDir.magnitude > 0)
                    {
                        dashSpeed = finalDashSpeed;
                        thisState = State.dashing;
                    }
                }
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
                if (Input.GetKey(KeyCode.Mouse0) && (Time.time > GetComponent<Behaviour_Fire>().previousTimeFired + 1 / GetComponent<Behaviour_Fire>().fireRate))
                {
                    GetComponent<Behaviour_Fire>().previousTimeFired = Time.time;

                    switch (currentPrimary)
                    {
                        case 0: //Assault
                            GetComponent<Behaviour_Fire>().Fire();
                            foreach (Behaviour_Fire _secondaries in GetComponentsInChildren<Behaviour_Fire>())
                                _secondaries.Fire();

                            break;
                    }

                }
                #endregion
                break;

            case State.dashing:
                dashSpeed -= dashSpeed * dashDecayMultiplier * Time.deltaTime;


                if (dashSpeed < dashThreshold)
                    ReturnToNeutral();
                break;


            case State.afterparry:
                break;
                    
            case State.dead:
                break;

        }
                
    }

    private void FixedUpdate()
    {
        switch (thisState)
        {
            case State.neutral:

                thisBody.velocity = moveDir; //Set to FixedUpdate for consistent physics update
                break;

            case State.dashing:
                thisBody.velocity = dashDir.normalized * dashSpeed;
                break;

            case State.dead:
                thisBody.velocity = new Vector2(0, 0);
                break;

        }

    }



    private void ReturnToNeutral()
    {
        thisState = State.neutral;
        isInvulnerable = false;
    }

    

    public void ChangeInvulnerable (bool _state)
    {
        isInvulnerable = _state;
    }

    public void AtomBomb()
    {
        Debug.Log("Atom Bomb bay");
    }

    public override void TakeDamage(float _damage)
    {
        if (isInvulnerable)
            return;

        base.TakeDamage(_damage);
    }


}
