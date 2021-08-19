using System.Collections;
using UnityEngine;

public class Behaviour_Bullet : MonoBehaviour
{

    public float damage;
    public float speed;
    public bool isHoming;
    public float homingTurnRate;
    public float lifeTimeDuration = Mathf.Infinity;
    public bool ifTrueDestroy = false;
    private float nextLifeTime;

    private Rigidbody2D thisBody;
    private bool collided = false;

    [SerializeField] private Quaternion forwardFacing;

    private GameObject currentHomingTarget = null;

    private void Awake()
    {

        forwardFacing = transform.rotation;
    }

    void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    public void Initialise()
    {
        collided = false;

        thisBody = GetComponent<Rigidbody2D>();
        thisBody.velocity = transform.up * speed;

        nextLifeTime = Time.time + lifeTimeDuration;
    }


    public virtual void Update()
    {
        if (collided)
            return;

        thisBody.velocity = transform.up * speed;

        if (Time.time >= nextLifeTime)
            DeactivateBullet();
    }

    protected GameObject SearchTarget(bool _isPlayerBullet)
    {
        if (currentHomingTarget != null)
            return currentHomingTarget;

        switch (_isPlayerBullet)
        {
            case true:
                Entity_Enemy[] _possibleTargets = FindObjectsOfType<Entity_Enemy>();
                if (_possibleTargets.Length == 0)
                    return null;

                else 
                { 
                    currentHomingTarget = _possibleTargets[Random.Range(0, _possibleTargets.Length)].gameObject;
                    return currentHomingTarget;

                } 

            case false:
                currentHomingTarget = FindObjectOfType<Player_Movement>().gameObject;
                return currentHomingTarget;

        }

    }


    public virtual void BulletCollided()
    {
        collided = true;


        foreach (Animator _childAnimator in GetComponentsInChildren<Animator>())
        {
            AnimatorUpdate(_childAnimator, "Exploded");
        
            
            thisBody.velocity = new Vector2(0, 0);
            StartCoroutine(DelayDeactive(0.2f));
        }
    }

    void AnimatorUpdate(Animator _animator, string _trigger)
    {
        _animator.SetTrigger(_trigger);

    }

    void DeactivateBullet()
    {
        transform.rotation = forwardFacing; //Reset Rotation

        if (ifTrueDestroy)
            Destroy(gameObject);

        else gameObject.SetActive(false);
    }

    IEnumerator DelayDeactive(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        DeactivateBullet();
    }
}
