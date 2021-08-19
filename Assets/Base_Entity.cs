using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Entity : MonoBehaviour
{
    public float health;


    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(float _damage)
    {
        health -= _damage;

        if (health <= 0f)
        {
            Death();
        }   
    }

    protected virtual void Death()
    {
        
        gameObject.SetActive(false);
        
    }

}
