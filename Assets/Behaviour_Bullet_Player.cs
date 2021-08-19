using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Bullet_Player : Behaviour_Bullet
{

    private AudioSource thisSoundSource;
    public AudioClip hitSound;

    public void Start()
    {
        thisSoundSource = GetComponent<AudioSource>();
        
    }


    public override void Update()
    {
        base.Update();

        if (isHoming && SearchTarget(true) != null)
        {
            Vector3 targetdirection = SearchTarget(true).transform.position - transform.position;
            Vector3 currentdirection = transform.up;
            Vector3 nextdirection = Vector3.RotateTowards(currentdirection, targetdirection, homingTurnRate * Time.deltaTime, 0f);
            nextdirection.z = -10;

            transform.rotation = Quaternion.LookRotation(nextdirection.normalized, Vector3.forward);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enviroment"))
        {
            //Debug.Log("ah");
            BulletCollidedNoHit();
        }

        else if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if (!collision.TryGetComponent(out Base_Entity _test)) //Guard against bullets
                return;

            //Debug.Log(collision.gameObject.name);

            ///////Update Bullet Behaviour
            BulletCollided();


            //Additional Player Behaviour
            //FindObjectOfType<Player_Movement>().AddCharge(2);

            ///////Update colliding object Behaviour
            //Decrease Hp
            if (collision.GetComponent<Base_Entity>() != null)
                collision.GetComponent<Base_Entity>().TakeDamage(damage);
            

        }
    }

    public override void BulletCollided()
    {
        base.BulletCollided();
        thisSoundSource.PlayOneShot(hitSound, 0.2f);

    }

    public void BulletCollidedNoHit()
    {
        base.BulletCollided();
    }

}
