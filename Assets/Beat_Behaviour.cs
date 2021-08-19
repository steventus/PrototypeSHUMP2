using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat_Behaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TriggerBeat()
    {
        StartCoroutine(OnBeat());
    }

    private IEnumerator OnBeat()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return null;

    }

}
