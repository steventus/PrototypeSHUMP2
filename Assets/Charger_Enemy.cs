using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger_Enemy : Entity_Enemy
{
    private Vector3 playerPosition;
    private bool startFire;
    private float index;
    [SerializeField] private float fireRate;

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float radiusToMove;
    [SerializeField] private float timeToMove;

    protected override void Start()
    {
        base.Start();
        playerPosition = GameObject.Find("Player").transform.position;
        startFire = true;
        index = 0;
    }

    public override void Update()
    {
        //Replace base.Update from Entity_Enemy that uses custom path
        if (GameObject.Find("Player") != null)
            playerPosition = GameObject.Find("Player").transform.position;



        Debug.DrawLine(transform.position, targetPosition);

        if (thisState == State.Waiting)
            StartCoroutine(Moving());

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        thisBody.AddForce((targetPosition - transform.position) * speed, ForceMode2D.Force);

        if (thisBody.velocity.magnitude >= maxSpeed)
            thisBody.velocity = thisBody.velocity.normalized * maxSpeed;


        if (startFire)
            StartCoroutine(BurstFire());

    }

    protected IEnumerator BurstFire()
    {

        startFire = false;

        switch (thisFeverState)
        {
            case FeverState.fever:
                GetComponent<Charger_Behaviour_Fire>().Behaviour1();
                yield return new WaitForSeconds(1 / (2 * fireRate));
                GetComponent<Charger_Behaviour_Fire>().Behaviour2();
                yield return new WaitForSeconds(1 / (2 * fireRate));
                break;

            case FeverState.neutral:
                GetComponent<Charger_Behaviour_Fire>().Behaviour1();
                yield return new WaitForSeconds(1 / fireRate);
                break;
        }

        startFire = true;


    }

    private IEnumerator Moving()
    {
        thisState = State.Moving;
        //Debug.Log("Moving");



        //Get Random Position
        Vector3 _desiredPosition = playerPosition + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y + distanceToPlayer, 0) * radiusToMove; //Go near player and above them.
        targetPosition = _desiredPosition;
        yield return new WaitForSeconds(timeToMove);

        thisState = State.Waiting;
        //Debug.Log("Waiting");
        yield return null;
    }

    private IEnumerator FireLoop()
    {
        Debug.Log("Test");
        startFire = true;
        while (startFire)
        {
            yield return new WaitForSeconds(1 / fireRate);
            ArcFire(20,5);
        }
        yield return null;
    }

}
