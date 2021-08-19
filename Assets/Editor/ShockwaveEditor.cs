using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShockwaveBehaviour))]
public class ShockwaveEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShockwaveBehaviour _target = (ShockwaveBehaviour)target;
        _target.GetComponent<SpriteRenderer>().color = _target.oldColor;



    }
}
