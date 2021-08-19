using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConductorClass))]
public class ConductorEditor : Editor
{
    private float EditorsongPosition;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ConductorClass _target = (ConductorClass)target;

        _target.secPerBeat = (60f / _target.songBPM);


    }
}
