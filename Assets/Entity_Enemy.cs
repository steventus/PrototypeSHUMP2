using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Enemy : Base_Entity
{

    public float speed;
    public float maxSpeed;

    public bool moveOnAwake = false;

    public float explosionRadius = 1;

    protected Rigidbody2D thisBody;

    

    [SerializeField] private int objectIndex = 0;
    protected enum State  {
        Moving,
        Waiting,
        Halting,
        Fire,
        LoopFire,
    }

    [SerializeField] protected State thisState;

    //Movement
    public bool loopRoute = false;
    [SerializeField] private Vector3 desiredPosition;
    private Vector3 initialPosition;
    [SerializeField]private List<Vector3> listOfPositions;
    public List<Transform> listofObjects;


    //Sprites
    public Sprite deathSprite;
    public float deathAnimationDuration;

    public Sprite hitSprite;
    private Sprite oldSprite;
    public float spriteHitDuration;

    //Camera
    public float cameraShakeIntensity;

    //Audio
    private AudioSource thisSoundSource;
    public AudioClip deathSound;


    protected virtual void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
        thisSoundSource = GetComponent<AudioSource>();
        InitialisePositions();

        if (moveOnAwake)
            thisState = State.Moving;

        else thisState = State.Waiting;


        oldSprite = GetComponent<SpriteRenderer>().sprite;

    }

    void InitialisePositions()
    {
        foreach (Transform _position in listofObjects)
        {
            listOfPositions.Add(_position.position);
            _position.gameObject.SetActive(false);
        }

        if (listOfPositions.Count > 0)
        {
            //Add last empty position object just to ensure any data on last position data gets run
            GameObject _clone = new GameObject("cap");
            _clone.AddComponent<PositionData>();
            _clone.transform.position = listOfPositions[listOfPositions.Count - 1];
            this.listofObjects.Add(_clone.transform);

            UpdateNextDesiredPosition();

            //First Initialise target first object Index
            objectIndex = 0;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (thisState)
        {
            default:
                StopMove();
                break;

            case State.Moving:
                if ((desiredPosition - initialPosition).magnitude < 0.2f)
                {
                    //Snap to desired position if below threshold value
                    //transform.position = desiredPosition;

                    //Check if remaining list of positions still has positions to go through
                    if (listOfPositions.Count > 0)
                    {
                        UpdateNextDesiredPosition();
                    }

                    //Ran out of positions to go through
                    else
                    {
                        StopMove();
                        break;
                    }
                }

                //////////////Movement

                //if not, continue moving
                thisBody.AddForce((desiredPosition - initialPosition).normalized * speed, ForceMode2D.Force);

                if (thisBody.velocity.magnitude >= maxSpeed)
                    thisBody.velocity = thisBody.velocity.normalized * maxSpeed;
                break;

            case State.Waiting:
                thisBody.velocity = new Vector2(0, 0);
                break;


            case State.Fire:
                Debug.Log("fire");
                Fire();
                thisState = State.Moving;
                break;

            case State.LoopFire:
                GetComponent<Behaviour_Fire>().UpdateLoopFire();                
                thisState = State.Moving;
                break;
        }
        
        //UpdatePosition
        initialPosition = transform.position;

        ////Debug Tools
        ///

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Arc Fire Angle: " + 30 + ". Number: " + 3 + ".");
            ArcFire(30, 3);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Arc Fire Angle: " + 30 + ". Number: " + 2 + ".");
            ArcFire(30, 2);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Home.");
            HomeFire();
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("TestFiring");
        //    Fire();        
        //
        //}
    }


    void UpdateNextDesiredPosition()
    {        
        desiredPosition = listOfPositions[0];

        //Update object Index until loop
        objectIndex++;
        if (objectIndex > listOfPositions.Count - 1)
            objectIndex = 0;

        //Queue to Wait or Halt or Fire
        PositionData _data = listofObjects[objectIndex].GetComponent<PositionData>();
        if (_data.wait)
            Wait(_data.waitDuration);

        else if (_data.halt)
            StopMove();

        else if (_data.fire)
            thisState = State.Fire;

        else if (_data.loopFire)
            thisState = State.LoopFire;
          
        
        
        //If loop enabled, latest position is added to back of loop.
        if (loopRoute)
            listOfPositions.Add(desiredPosition);

        //If remove after enabled, remove this latest position
        

        //Remove most recent position
        listOfPositions.RemoveAt(0);
     
    }

    public override void TakeDamage(float _damage)
    {
        if (health >= 0) //If not dead yet
            StartCoroutine(HitSpriteChange(spriteHitDuration));


        base.TakeDamage(_damage);

    }

    private IEnumerator HitSpriteChange(float _duration)
    {
        SpriteRenderer _thisSpriteRenderer = GetComponent<SpriteRenderer>();

        _thisSpriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(_duration);
        _thisSpriteRenderer.sprite = oldSprite;
    }

    protected override void Death()
    {
        GameManager.instance.UpdateEnemiesAlive(gameObject);


        AudioManager.instance.PlaySound(deathSound,false,0.2f,gameObject);
        GetComponent<BoxCollider2D>().enabled = false; //Disable Colliders

        Camera_Behaviour.instance.Shake(0.1f);

        StartCoroutine(DeathAnimation()); //base.Death is located here
    }

    private IEnumerator DeathAnimation()
    {
        for (int _i = 0; _i <= 30* deathAnimationDuration; _i++)
        {
            
            ExplosionManager.instance.Explode(transform.position, explosionRadius);
            


            GetComponent<SpriteRenderer>().sprite = oldSprite;
            yield return new WaitForFixedUpdate();
            GetComponent<SpriteRenderer>().sprite = deathSprite;
            yield return new WaitForFixedUpdate();
        }

        base.Death();



        yield return null;
    }

    public void Move(Vector3 _position)
    {
        thisState = State.Moving;

        desiredPosition = _position;
    }

    public void Fire()
    {
        GetComponent<Behaviour_Fire>().Fire();
    }

    public void ArcFire(float _angle, float _number)
    {
        GetComponent<Behaviour_Fire>().ArcFire(_angle, _number);
    }

    public void HomeFire()
    {
        GetComponent<Behaviour_Fire>().HomeFire();
    }

    public void Wait(float _duration)
    {
        thisState = State.Waiting;

        StopMove();
        StartCoroutine(waitDuration(_duration));
    }

    public void StopMove()
    {
        thisState = State.Halting;

        if (thisBody.velocity.magnitude > 0)
            Debug.Log(gameObject.name + " is halted.");
        thisBody.velocity = new Vector2(0, 0);
    }

    IEnumerator waitDuration(float _duration)
    {        
        yield return new WaitForSeconds(_duration);
        UpdateNextDesiredPosition();

        //Move after waiting;
        thisState = State.Moving; 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player_Movement_3 _player))
        {
            _player.TakeDamage(10);
            Death();
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Transform _position in listofObjects)
        {
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_position.position, 0.1f);
        }
    }


}
