using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRhythmBullet : Behaviour_Bullet
{
    public float readySpeed;
    public float fireSpeed;

    private float count;
       

    public void TriggerSmallBeat()
    {
        count++;

        switch (count)
        {
            case 1:
                ChangeSpeed(0);
                break;

            case 3:
                ChangeSpeed(readySpeed);
                break;

            case 5:
                ChangeSpeed(fireSpeed);
                break;
        }
    }

    public void ResetCount()
    {
        count = 0;
    }

    private void ChangeSpeed(float _speed)
    {
        speed = _speed;
    }

    
}
