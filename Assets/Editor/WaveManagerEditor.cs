
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    public int waveNumber = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WaveManager _target = (WaveManager)target;
        if (GUILayout.Button("Create New Wave"))
        {
            GameObject _newWave = new GameObject("Wave_" + (_target.waves.Count));
            _newWave.AddComponent<Wave>();
            _target.waves.Add(_newWave.GetComponent<Wave>());
            _newWave.transform.SetParent(_target.transform);
        }

        
        GUILayout.BeginHorizontal();

        waveNumber = EditorGUILayout.IntField("Remove Position At", waveNumber);

        if (GUILayout.Button("Remove"))
        {
            RemovePosition(waveNumber);
        }

        GUILayout.EndHorizontal();

        

        //Ensure changes stick around
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }


    }

    void RemovePosition(int _index)
    {
        WaveManager _target = (WaveManager)target; //Creates a reference to directly have a pointer to the entity selected.

        //Get Object
        GameObject _obj = _target.waves[waveNumber].gameObject;

        //Remove Object from list and destroy Object
        _target.waves.RemoveAt(waveNumber);

        DestroyImmediate(_obj);
    }
}
