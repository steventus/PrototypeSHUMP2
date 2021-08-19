using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryBulletBehaviour : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (Time.time > GetComponent<Behaviour_Fire>().previousTimeFired + 1 / GetComponent<Behaviour_Fire>().fireRate))
        {
            GetComponent<Behaviour_Fire>().previousTimeFired = Time.time;
            GetComponent<Behaviour_Fire>().Fire();
        }
    }
}
