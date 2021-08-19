using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Bullet_Enemy : Behaviour_Bullet
{
    public bool canBeParried;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enviroment"))
        {
            //Debug.Log("ah");
            BulletCollided();
        }

        else if (collision.TryGetComponent(out Player_Movement_3 _player))
        {           

            //Debug.Log(collision.gameObject.name);

            ///////Update Bullet Behaviour
            BulletCollided();

            ///////Update colliding object Behaviour
            //Decrease Hp
            if (collision.GetComponent<Base_Entity>() != null)
                collision.GetComponent<Base_Entity>().TakeDamage(damage);


        }
    }

    public void Riposted()
    {
        BulletCollided();
        Debug.Log("Wowza");
    }


}
