using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaserBehaviour : MonoBehaviour
{

    private GameObject player;
    private LineRenderer thisLaser;
    private EdgeCollider2D thisLaserCollider;

    [SerializeField] float laserDamage = 2;
    [SerializeField] private int count;
    [SerializeField] private int smallCount;

    public AudioClip readyLaserSound;
    public AudioClip fireLaserSound;
    private AudioSource thisSoundSource;


    //Next Add collider to laser


    void Start()
    {
        player = GameObject.Find("Player");

        thisLaser = GetComponent<LineRenderer>();

        thisLaserCollider = GetComponent<EdgeCollider2D>();
        thisLaserCollider.enabled = false;
        thisLaserCollider.offset = -transform.position; //Set to negative of transform position, so that it uses world position (ie. set anchor to zero).

        count = 0; //Use for 1-2 response

        thisSoundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        thisLaserCollider.offset = -transform.position; //Set to negative of transform position, so that it uses world position (ie. set anchor to zero).

        switch (count)
        {
            case 1:
                ReadyLaser();
                break;

            case 2:
                ReadyLaser();
                break;
            case 3:                
                FireLaser();
                break;
            case 5:
                count = 0; //So laser stops following player immediately when firing laser
                break;

        }
    }

    public void TriggerSmallBeat()
    {
        count++;
        

        switch (count)
        {
            case 1:
                thisSoundSource.PlayOneShot(readyLaserSound, 0.5f);
                break;


            case 2:
                thisSoundSource.PlayOneShot(readyLaserSound, 0.5f);
                break;
        }
    }

    public void TriggerBigBeat()
    {        

    }

    private void ReadyLaser()
    {
        Vector3 _direction = player.transform.position - transform.position;
        Vector3[] _positions = new Vector3[2] { transform.position, player.transform.position + _direction };
        thisLaser.SetPositions(_positions);
        ChangeLaserWidth(0.1f);
    }

    private void FireLaser()
    {
        count++; //So laser stops following player immediately when firing laser

        Vector3 _direction = player.transform.position - transform.position;
        Vector3[] _positions = new Vector3[2] { transform.position, player.transform.position + _direction };
        thisLaser.SetPositions(_positions);
        ChangeLaserWidth(1f);

        //thisLaserCollider.points = new Vector2[2] { new Vector2(_positions[0].x, _positions[0].y), new Vector2(_positions[1].x, _positions[1].y) };
        //Convert array of Vector3 to Vector2

        List<Vector2> _2DPositions = new List<Vector2>();
        for (int index = 0; index <= _positions.Length-1; index++)
        {
            _2DPositions.Add(_positions[index]);
        }

        thisLaserCollider.SetPoints(_2DPositions);
        thisLaserCollider.enabled = true;

        StartCoroutine(DelayStopLaser());
    }

    private void ChangeLaserWidth(float _width)
    {
        thisLaser.startWidth = _width;
        thisLaser.endWidth = thisLaser.startWidth;
    }


    public IEnumerator DelayStopLaser()
    {
        yield return new WaitForSeconds(0.1f);
        StopLaser();

    }

    private void StopLaser()
    {
        ChangeLaserWidth(0);

        thisLaserCollider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(collision.gameObject.name);

            ///////Update colliding object Behaviour
            //Decrease Hp
            if (collision.GetComponent<Base_Entity>() != null)
                collision.GetComponent<Base_Entity>().TakeDamage(laserDamage);


        }
    }

}
