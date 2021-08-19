using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser_Enemy : Entity_Enemy
{
    

    public override void Update()
    {
        thisBody.velocity = transform.up * speed;
    }
}
