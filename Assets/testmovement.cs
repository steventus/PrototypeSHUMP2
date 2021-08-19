using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovement : MonoBehaviour
{

    public int speed;
    public int dashSpeed;

    private Rigidbody2D thisBody;


    void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #region Movement
        float _inputx = Input.GetAxisRaw("Horizontal");
        float _inputy = Input.GetAxisRaw("Vertical");

        float _speedMultiplier;

        switch (Input.GetKey(KeyCode.LeftShift))
        {
            case true:
                thisBody.AddForce(new Vector2(_inputx, _inputy) * dashSpeed, ForceMode2D.Impulse);
                _speedMultiplier = 1f;
                break;

            default:
                //Default here means if not pressed.
                _speedMultiplier = speed;
                break;

        }

        //Final player speed
        thisBody.velocity = new Vector2(_inputx, _inputy) * _speedMultiplier;
        #endregion
    }
}
