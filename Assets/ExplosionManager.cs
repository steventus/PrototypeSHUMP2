using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager instance = null;

    public GameObject baseExplosion;
    public float numOfExplode, numOfSmokeExplode, numOfSmallExplode;

    public float numberToInstantiate;

    //Explosion Animation States
    //const string bulletFire;
    const string explosion1 = "Explosion1";
    const string explosion2 = "Explosion2";
    const string smokeExplosion1 = "SmokeExplode1";
    const string smokeExplosion2 = "SmokeExplode2";
    const string smallExplosion1 = "SmallExplode1";
    const string smallExplosion2 = "SmallExplode2";
    const string smallExplosion3 = "SmallExplode3";

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        //Instantiate and disable bullets
        for (int index = 1; index < numberToInstantiate; index++)
        {
            GameObject _bulletClone = Instantiate(baseExplosion, transform.position, Quaternion.identity);
            _bulletClone.transform.SetParent(gameObject.transform);
            //_bulletClone.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            _bulletClone.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Explode(transform.position, 0.1f);
    }

    public void SpawnVFX(Vector2 _position, string _VFXAnimationState)
    {

    }


    public void Explode(Vector2 _position, float _explosionRadius)
    {
        //Choose top explosion
        GameObject _clone = transform.GetChild(1).gameObject;

        //"Instantiate" by moving to point
        _clone.transform.position = ChooseRandomLocation(_position, _explosionRadius);

        //Activate
        _clone.gameObject.SetActive(true);
        PlayExplosion(_clone.GetComponent<Animator>(), (int)Random.Range(1,4));

        //Move to last of hierarchy
        _clone.transform.SetAsLastSibling();
    }

    

    public Vector3 ChooseRandomLocation(Vector2 _center, float _radius)
    {
        return (_center + (Random.insideUnitCircle) * _radius);
    }

    public void PlayExplosion(Animator _explosion, float _type)
    {
        string _desiredState = "";

        switch (_type)
        {
            case 1: //Normal Explosion
                int _index = Random.Range(0, 2); //0 or 1
                switch (_index)
                {
                    case 0:
                        _desiredState = explosion1;
                        break;

                    case 1:
                        _desiredState = explosion2;
                        break;
                }

                break;

            case 2: //Smokey Explosion
                int _index2 = Random.Range(0, 2); //0 or 1
                switch (_index2)
                {
                    case 0:
                        _desiredState = smokeExplosion1;
                        break;

                    case 1:
                        _desiredState = smokeExplosion2;
                        break;
                }
                break;

            case 3: //Small Explosion
                int _index3 = Random.Range(0, 3); //0 or 1 or 2
                switch (_index3)
                {
                    case 0:
                        _desiredState = smallExplosion1;
                        break;

                    case 1:
                        _desiredState = smallExplosion2;
                        break;

                    case 2:
                        _desiredState = smallExplosion3;
                        break;

                }
                break;
        }


        AnimatorPlayState(_explosion, _desiredState);
    }

    public void AnimatorPlayState(Animator _animator, string _state)
    {
        _animator.Play(_state);
    }

}
