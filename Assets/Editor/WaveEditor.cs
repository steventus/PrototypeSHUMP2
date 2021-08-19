
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wave))]
public class WaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Wave _target = (Wave)target;

        //Ensures timeToSpawn list size is always equal to objectToSpawn list
        float index = _target.objectToSpawn.Count;
        if (_target.timeToSpawn.Count <= index -1)
        {
            _target.timeToSpawn.Add(_target.timeToSpawn[_target.timeToSpawn.Count-1]);
        }

        GUILayout.Space(20);
        GUILayout.Label("Wave Enemies TimeToSpawn", EditorStyles.boldLabel);

        
        for (int i = 0; i <= _target.objectToSpawn.Count - 1; i++)
        {
            _target.timeToSpawn[i] = EditorGUILayout.FloatField(_target.objectToSpawn[i].name, _target.timeToSpawn[i]);

        }

        

        






        //Ensure changes stick around
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }
    }
}
