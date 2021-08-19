using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public Slider worldSpaceComboMeter;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        worldSpaceComboMeter.maxValue = 100;
        worldSpaceComboMeter.value = 0;
    }

    void Update()
    {
        if (FindObjectOfType<Player_Movement>()!=null)
            GetComponent<RectTransform>().position = FindObjectOfType<Player_Movement>().transform.position;
    }

    public void OnChangeCombo(float _number)
    {
        worldSpaceComboMeter.value = _number;
    }

}
