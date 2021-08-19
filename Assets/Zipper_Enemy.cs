using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipper_Enemy : Entity_Enemy
{
    private Vector3 originalPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float radiusToMove;
    [SerializeField] private float timeToMove;

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
    }

    public override void Update()
    {
        //Replace base.Update from Entity_Enemy that uses custom path

        //Get Random Position
        Vector3 _desiredPosition = originalPosition + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0) * radiusToMove;

        if (thisState == State.Waiting)
            StartCoroutine(Moving(_desiredPosition));

    }

    private void FixedUpdate()
    {
        thisBody.AddForce((targetPosition - transform.position).normalized * speed, ForceMode2D.Force);

        if (thisBody.velocity.magnitude >= maxSpeed)
            thisBody.velocity = thisBody.velocity.normalized * maxSpeed;
    }

    protected IEnumerator BurstFire()
    {
        for (int i =0; i <= 0; i++)
        {
            Fire();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Moving(Vector3 _newPosition)
    {
        thisState = State.Moving;
        //Debug.Log("Moving");

        targetPosition = _newPosition;
        StartCoroutine(BurstFire());

        yield return new WaitForSeconds(timeToMove);  

        thisState = State.Waiting;
        //Debug.Log("Waiting");
        yield return null;
    }
}
