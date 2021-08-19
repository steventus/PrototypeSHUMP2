using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Fire : MonoBehaviour
{
    public GameObject bullet;

    public Transform bulletSpawnPoint;

    public float numberToInstantiate = 30f;

    public float fireRate = 1f;
    public float secondaryFireRate = 1f;
    public bool loopFire = false;
    [HideInInspector] public float previousTimeFired=0f;

    private AudioSource thisSoundSource;
    public AudioClip normalFireSound;

    // Start is called before the first frame update
    void Start()
    {

        //BulletManager.instance.RegisterBullet(bullet, numberToInstantiate);

        //Instantiate and disable bullets
        for (int index = 1; index < numberToInstantiate; index++)
        {
            GameObject _bulletClone = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            _bulletClone.transform.SetParent(gameObject.transform);
            //_bulletClone.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            _bulletClone.SetActive(false);
        }

        thisSoundSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (loopFire && (Time.time > previousTimeFired + 1 / fireRate))            
        {
            previousTimeFired = Time.time;
            Fire();
        }
    }

    public void UpdateLoopFire()
    {
        loopFire = true;
    }

    public virtual GameObject Fire()
    {

        


        //Enable top bullet
        GameObject _bulletClone = transform.GetChild(1).gameObject;

        if (normalFireSound != null)
            thisSoundSource.PlayOneShot(normalFireSound, 0.2f);



        if (_bulletClone.TryGetComponent(out TestRhythmBullet _bullet))
            _bullet.ResetCount();

        //"Instantiate" by moving to bullet spawn point
        _bulletClone.transform.position = bulletSpawnPoint.position;

        //Reset rotation to forward if not forward
        float _angleToRotate = Vector3.SignedAngle(_bulletClone.transform.up, _bulletClone.transform.parent.up, Vector3.forward);
        _bulletClone.transform.Rotate(Vector3.forward, _angleToRotate);

        //Reset Color
        _bulletClone.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        //ActivateBullet
        _bulletClone.gameObject.SetActive(true);
        _bulletClone.SetActive(true);
        _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();


        //Move bullet to last of hierarchy
        _bulletClone.transform.SetAsLastSibling();


        return _bulletClone;
    }

    public void ChargeFire(float _scale)
    {
        //Enable top bullet
        GameObject _bulletClone = transform.GetChild(1).gameObject;



        //"Instantiate" by moving to bullet spawn point
        _bulletClone.transform.position = bulletSpawnPoint.position;

        //Reset rotation to forward if not forward
        float _angleToRotate = Vector3.SignedAngle(_bulletClone.transform.up, _bulletClone.transform.parent.up, Vector3.forward);
        _bulletClone.transform.Rotate(Vector3.forward, _angleToRotate);

        //ActivateBullet
        _bulletClone.gameObject.SetActive(true);
        _bulletClone.SetActive(true);
        _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();


        //Move bullet to last of hierarchy
        _bulletClone.transform.SetAsLastSibling();



    }


    public void ArcFire(float _arcAngle, float _numberOfBullets)
    {
        float _deltaAngle = _arcAngle / (_numberOfBullets - 1);
        float _numberToInstantiate = (_numberOfBullets - 1) / 2; //Number to instantiate on each side (left and right) from center of bullet.

        //Instantiate on right
        for (int index = 0; index < _numberToInstantiate; index++)
        {
            //Enable top bullet
            GameObject _bulletClone = transform.GetChild(1).gameObject;

            

            //"Instantiate" by moving to bullet spawn point
            _bulletClone.transform.position = bulletSpawnPoint.position;
            _bulletClone.transform.Rotate(Vector3.forward, _deltaAngle * (index + 1));
            Debug.DrawRay(_bulletClone.transform.position, -_bulletClone.transform.up, Color.red, 1);

            //Reset Color
            _bulletClone.GetComponentInChildren<SpriteRenderer>().color = Color.white;

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
            GameObject _bulletClone = transform.GetChild(1).gameObject;

            
            //"Instantiate" by moving to bullet spawn point
            _bulletClone.transform.position = bulletSpawnPoint.position;
            _bulletClone.transform.Rotate(Vector3.forward, -_deltaAngle * (index + 1));
            Debug.DrawRay(_bulletClone.transform.position, -_bulletClone.transform.up, Color.red, 1);


            //Reset Color
            _bulletClone.GetComponentInChildren<SpriteRenderer>().color = Color.white;

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
            Fire();
        }

        
            
        

    }

    public void HomeFire()
    {
        //Enable top bullet
        GameObject _bulletClone = transform.GetChild(1).gameObject;

        _bulletClone.gameObject.SetActive(true);
        _bulletClone.SetActive(true);
        _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();

        //"Instantiate" by moving to bullet spawn point
        _bulletClone.transform.position = bulletSpawnPoint.position;

        //Rotate towards player
        Vector3 _targetDir = -transform.position - FindObjectOfType<Player_Movement>().transform.position;
        float _toTurn = Vector3.SignedAngle(transform.forward, _targetDir,Vector3.up);
        Debug.Log(_toTurn);
        _bulletClone.transform.Rotate(Vector3.forward, _toTurn);

        //Move bullet to last of hierarchy
        _bulletClone.transform.SetAsLastSibling();
    }

    public void PlayerHomeFire()
    {
        //Enable top bullet
        GameObject _bulletClone = transform.GetChild(1).gameObject;

        _bulletClone.gameObject.SetActive(true);
        _bulletClone.SetActive(true);
        _bulletClone.GetComponent<Behaviour_Bullet>().Initialise();

        //"Instantiate" by moving to bullet spawn point
        _bulletClone.transform.position = bulletSpawnPoint.position;

        //Rotate towards player
        Vector3 _targetDir = -transform.position - FindObjectOfType<Player_Movement>().transform.position;
        float _toTurn = Vector3.SignedAngle(transform.forward, _targetDir, Vector3.up);
        Debug.Log(_toTurn);
        _bulletClone.transform.Rotate(Vector3.forward, _toTurn);

        //Move bullet to last of hierarchy
        _bulletClone.transform.SetAsLastSibling();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        if (FindObjectOfType<Player_Movement_3>() != null)
        {
            Vector3 _targetDir = FindObjectOfType<Player_Movement_3>().transform.position - transform.position;
            Gizmos.DrawLine(transform.position, FindObjectOfType<Player_Movement_3>().transform.position);
        }
    }
}
