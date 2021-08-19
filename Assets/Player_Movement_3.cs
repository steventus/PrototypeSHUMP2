using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Movement_3 : Base_Entity
{
    public int speed;

    private Rigidbody2D thisBody;
    private Vector3 moveDir;

    public float assaultRate;
    [HideInInspector] public enum State
    {
        neutral,
        dead
    }

    private bool isInvulnerable = false;
    [HideInInspector] public State thisState;


    private enum FeverState
    {
        neutral,
        fever
    }

    private FeverState thisFeverState;


    private Player_EngineBehaviour thisEngineAnimator;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        thisBody = GetComponent<Rigidbody2D>();
        thisEngineAnimator = GetComponent<Player_EngineBehaviour>();

        thisFeverState = FeverState.neutral;

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
                    GetComponent<Behaviour_Fire>().Fire();

                    if (thisFeverState == FeverState.fever)
                    foreach (Behaviour_Fire _secondaries in GetComponentsInChildren<Behaviour_Fire>())
                        _secondaries.Fire();
                }
                #endregion
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

            case State.dead:
                thisBody.velocity = new Vector2(0, 0);
                break;

        }

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

    public void isFever(bool _state)
    {
        switch (_state)
        {
            case true:
                thisFeverState = FeverState.fever;
                break;

            case false:
                thisFeverState = FeverState.neutral;
                break;
        }
    }
}
