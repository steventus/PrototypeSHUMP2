using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_EngineBehaviour : MonoBehaviour
{
    public Animator playerEngine; //Must be attached seperately

    //AnimationStates
    const string weakEngine = "PlayerWeakEngine";
    const string normalEngine = "PlayerEngine";
    const string strongEngine = "PlayerStrongEngine";

    public string oldState;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetEnginePower(int _number)
    {
        switch (_number)
        {
            case 1:
                ChangeAnimationState(weakEngine);
                break;

            case 2:
                ChangeAnimationState(normalEngine);
                break;

            case 3:
                ChangeAnimationState(strongEngine);
                break;
        }
    }

    private void ChangeAnimationState(string _state)
    {
        if (oldState == _state)
            return;

        playerEngine.Play(_state);

        oldState = _state;
    }

}
