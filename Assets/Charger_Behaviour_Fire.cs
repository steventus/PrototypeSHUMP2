using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger_Behaviour_Fire : Behaviour_Fire
{
    public Color parryableColor;

    public void FireParryable(bool _state)
    {
        Behaviour_Bullet_Enemy _parryableBullet = base.Fire().GetComponent<Behaviour_Bullet_Enemy>();
        _parryableBullet.canBeParried = true;
        _parryableBullet.gameObject.GetComponentInChildren<SpriteRenderer>().color = parryableColor;
    }

    public void Behaviour1()
    {
        FireParryable(true);
        ArcFire(20, 4);
            
    }

    public void Behaviour2()
    {        
        ArcFire(60, 8);

    }
}
