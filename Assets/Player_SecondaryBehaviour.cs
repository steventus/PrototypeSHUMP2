using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SecondaryBehaviour : MonoBehaviour
{  
    public GameObject forwardRocket;
    public GameObject homeRocket;
    public GameObject rocketFlares;
    public GameObject sawBlades;

    public float numberOfRocketBurst;
    public Transform bulletSpawnPoint;

    private bool ifFired;

    void Start()
    {
        ifFired = false;
    }

    void Update()
    {
        
    }

    public void TriggerSecondary(int _number)
    {      


        switch (_number)
        {
            case 0: //Foward Rocket Burst
                if (!ifFired)
                    StartCoroutine(forwardRocketBurst());
                break;

            case 1: //Homing Rocket
                if (!ifFired)
                    StartCoroutine(homeRocketBurst());
                break;

            case 2: //Rocket Flares
                if (!ifFired)
                    StartCoroutine(rocketFlaresBurst());
                break;

            case 3: //Sawblades
                break;

            default:
                break;
        }
        Debug.Log("Fired secondary weapon:  " + _number);

    }

    public void Fire(GameObject _obj)
    {        

        //Enable top bullet
        GameObject _bulletClone = Instantiate(_obj, bulletSpawnPoint.position, Quaternion.identity);

        //"Instantiate" by moving to bullet spawn point
        _bulletClone.transform.position = bulletSpawnPoint.position;

        //Reset rotation to forward if not forward
        //float _angleToRotate = Vector3.SignedAngle(_bulletClone.transform.up, _bulletClone.transform.parent.up, Vector3.forward);
        //_bulletClone.transform.Rotate(Vector3.forward, _angleToRotate);

        //ActivateBullet
        _bulletClone.gameObject.SetActive(true);
        _bulletClone.SetActive(true);
        _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();

    }

    public void ArcFire(GameObject _obj, float _arcAngle, float _numberOfBullets)
    {
        float _deltaAngle = _arcAngle / (_numberOfBullets - 1);
        float _numberToInstantiate = (_numberOfBullets - 1) / 2; //Number to instantiate on each side (left and right) from center of bullet.

        //Instantiate on right
        for (int index = 0; index < _numberToInstantiate; index++)
        {
            //Enable top bullet
            GameObject _bulletClone = Instantiate(_obj, bulletSpawnPoint.position, Quaternion.identity);



            //"Instantiate" by moving to bullet spawn point
            _bulletClone.transform.position = bulletSpawnPoint.position;
            _bulletClone.transform.Rotate(Vector3.forward, _deltaAngle * (index + 1));

            //Activate Bullet
            _bulletClone.gameObject.SetActive(true);
            _bulletClone.SetActive(true);
            _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();

            //Move bullet to last of hierarchy
            _bulletClone.transform.SetAsLastSibling();
        }

        //Instantiate on left
        for (int index = 0; index < _numberToInstantiate; index++)
        {
            //Enable top bullet
            GameObject _bulletClone = Instantiate(_obj, bulletSpawnPoint.position, Quaternion.identity);


            //"Instantiate" by moving to bullet spawn point
            _bulletClone.transform.position = bulletSpawnPoint.position;
            _bulletClone.transform.Rotate(Vector3.forward, -_deltaAngle * (index + 1));

            //Activate Bullet
            _bulletClone.gameObject.SetActive(true);
            _bulletClone.SetActive(true);
            _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();


            //Move bullet to last of hierarchy
            _bulletClone.transform.SetAsLastSibling();
        }

        if (_numberOfBullets % 2 == 0) //Modulo operator, use to find dividend after dividing by 2. To check for odd or event.
        {
            //number is even            
        }

        else
        {
            //number is odd        

            //Instantiate on center of Arc
            Fire(_obj);
        }





    }

    private IEnumerator forwardRocketBurst()
    {
        ifFired = true;
        for (int i = 0; i <= numberOfRocketBurst; i++)
        {
            Fire(forwardRocket);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        ifFired = false;
    }

    private IEnumerator homeRocketBurst()
    {
        ifFired = true;
        ArcFire(homeRocket, 60, 6);

        yield return new WaitForSeconds(1f);
        ifFired = false;
    }
    private IEnumerator rocketFlaresBurst()
    {
        ifFired = true;
        ArcFire(rocketFlares, 360, 30);

        yield return new WaitForSeconds(1f);
        ifFired = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0));

    }
}
