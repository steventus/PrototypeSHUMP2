using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D thisGroundCollider;

    public Color oldColor;
    public Color readyColor;
    public Color attackColor;

    [SerializeField] float groundDamage = 2;
    [SerializeField] private int count;
    [SerializeField] private int bigCount;

    public AudioClip readyGroundSound;
    public AudioClip fireGroundSound;
    private AudioSource thisSoundSource;


    //Next Add collider to laser


    void Start()
    {
        player = GameObject.Find("Player");

        thisGroundCollider = GetComponent<BoxCollider2D>();
        thisGroundCollider.enabled = false;

        count = 0; //Use for 1-4 response

        oldColor = GetComponent<SpriteRenderer>().color;
        ChangeColor(oldColor);

        thisSoundSource = GetComponent<AudioSource>();
    }

    public void TriggerSmallBeat()
    {
        count++;

        switch (count)
        {          

            case 4:
                count = 0;
                TriggerBigBeat();
                break;
        }
    }

    public void TriggerBigBeat()
    {
        bigCount++;

        switch (bigCount)
        {

            case 1:
                thisSoundSource.PlayOneShot(readyGroundSound, 0.5f);
                ChangeColor(readyColor);
                break;
            case 2:
                ChangeColor(attackColor);
                GroundAttack();
                break;
        }
    }

    private void GroundAttack()
    {
        thisGroundCollider.enabled = true;

        thisSoundSource.PlayOneShot(fireGroundSound, 0.5f);

        StartCoroutine(DelayStopAttack());
    }

    private IEnumerator DelayStopAttack()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeColor(oldColor);
        StopAttack();
    }

    private void StopAttack()
    {
        bigCount = 0;
        thisGroundCollider.enabled = false;
    }

    private void ChangeColor(Color _color)
    {
        GetComponent<SpriteRenderer>().color = _color;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(collision.gameObject.name);

            ///////Update colliding object Behaviour
            //Decrease Hp
            if (collision.GetComponent<Base_Entity>() != null)
                collision.GetComponent<Base_Entity>().TakeDamage(groundDamage);


        }
    }
    void Update()
    {
        
    }


}
