using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Behaviour_2 : Base_Entity
{
    public float speed;
    public float dashSpeed;
    public float dashDecayMultiplier;
    public float dashThreshold;
    public float attackRange;
    public LayerMask enemyLayer;

    private Rigidbody2D thisBody;
    private Vector3 moveDir;
    private Vector3 dashDir;
    private float finalDashSpeed;

    private bool readyToPerfect = true;
    [SerializeField] private int currentPrimary = 0;

    public float assaultRate;

    public AudioSource thisSoundSorce;

    private enum State
    {
        neutral,
        dashing,
        attacking,
        dead
    }

    private State thisState;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        thisBody = GetComponent<Rigidbody2D>();

        finalDashSpeed = dashSpeed;
        thisState = State.neutral;


        //Temporary
        thisSoundSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (thisState)
        {
            case State.neutral:

                #region Movement
                float _inputx = Input.GetAxis("Horizontal");
                float _inputy = Input.GetAxis("Vertical");
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




                //Receive input for attack
                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (SelectClosestBullet() == null)
                        return;

                    //TryGetComponent Behaviour Bullet Enemy
                    else if (SelectClosestBullet().TryGetComponent(out Behaviour_Bullet_Enemy _target))
                        _target.Riposted();

                    //TryGetComponent Behaviour Laser Enemy
                    //set bool to true
                }

                //If Inputgetkeyup and bool true, then take damage and reset bool to false


                break;

            case State.dashing: //Ignore movement during rolling
                dashSpeed -= dashSpeed * dashDecayMultiplier * Time.deltaTime;

                
                if (dashSpeed < dashThreshold)
                    thisState = State.neutral;

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

    private GameObject SelectClosestBullet()
    {
        Collider2D[] _hitinfo = Physics2D.OverlapCircleAll(transform.position, attackRange,enemyLayer);
        float _closestDistance = Mathf.Infinity;
        GameObject _selectedTarget = null;

        Debug.Log(_hitinfo.Length);
        foreach (Collider2D _obj in _hitinfo)
        {
            Debug.Log(_obj.name);
            float _distance = (_obj.transform.position - transform.position).magnitude; 

            if (_distance <= _closestDistance)
            {
                _closestDistance = _distance;
                _selectedTarget = _obj.gameObject;
            }
        }
        Debug.Log("Closest Enemy is: " + _selectedTarget);
        return _selectedTarget;
    }

    public override void TakeDamage(float _damage)
    {
        if (thisState == State.dashing)
            return;

        base.TakeDamage(_damage);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }



}
