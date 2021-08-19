using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance = null;

    public List<GameObject> bullets;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RegisterBullet(GameObject _bullet, float _numberToInstantiate)
    {
        //If bullets already contain _bullet, then check if the list has 100 clones. If it does, then do not register more.

        if (bullets.Contains(_bullet))
        {
            int _totalClones = 0;

            foreach (GameObject _obj in bullets)
            {
                if (_obj == _bullet)
                {
                    _totalClones++;
                }
            }

            if (_totalClones > 100f)
                return;
        } 



        for (int index = 0; index < _numberToInstantiate; index++)
        {
            GameObject _bulletClone = Instantiate(_bullet, transform.position, Quaternion.identity);
            _bulletClone.transform.SetParent(gameObject.transform);

            bullets.Add(_bulletClone);
            _bulletClone.SetActive(false);
        }
    }

    public GameObject GetBullet(GameObject _requestedBullet)
    {
        //bullets.Find(_requestedBullet);

        if (bullets.Contains(_requestedBullet))
        {            
            Debug.Log("Found");
            foreach (GameObject _obj in bullets)
                if (_obj.activeInHierarchy == false)
                {
                    _obj.SetActive(true);
                    _obj.transform.SetAsLastSibling();
                    Debug.Log("Returning " + _obj);
                    return _obj;
                }    
        }

        else
        {
            Debug.Log("Requested: " + _requestedBullet + " not found.");
        }

        return null;
    }

}
