using UnityEngine;
using UnityEditor;

//Attribute to create custom inspector for a particular type
[CustomEditor(typeof(Entity_Enemy))] 
public class ExampleEditor : Editor
{
    int index = 0;
    int insertAt = 0;
    int numberOfPositions = 0;

    float waitDuration = 0f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);

        Entity_Enemy _target = (Entity_Enemy)target; //Creates a reference to directly have a pointer to the entity selected.
        GUILayout.Label("Path Editor", EditorStyles.boldLabel);
        insertAt = EditorGUILayout.IntField("Insert At", insertAt);

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Default"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_position_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Add(_clone.transform);


        }

        if (GUILayout.Button("Insert Default"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_position_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Insert(insertAt,_clone.transform);


        }

        
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Halt"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_halt_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().halt = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Add(_clone.transform);
        }

        if (GUILayout.Button("Insert Halt"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_halt_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().halt = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Insert(insertAt,_clone.transform);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        waitDuration = EditorGUILayout.FloatField("Wait Duration", waitDuration);

        if (GUILayout.Button("Add Wait"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_wait_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().wait = true;
            _clone.GetComponent<PositionData>().waitDuration = waitDuration;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Add(_clone.transform);
        }

        if (GUILayout.Button("Insert Wait"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_wait_" + numberOfPositions);
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().wait = true;
            _clone.GetComponent<PositionData>().waitDuration = waitDuration;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Insert(insertAt,_clone.transform);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Fire"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_fire");
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().fire = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Add(_clone.transform);
        }

        if (GUILayout.Button("Insert Fire"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_fire");
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().fire = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Insert(insertAt,_clone.transform);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add LoopFire"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_updateloopfire");
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().loopFire = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Add(_clone.transform);
        }

        if (GUILayout.Button("Insert LoopFire"))
        {
            //Add new empty object at position of obj
            GameObject _clone = new GameObject(_target.name + "_updateloopfire");
            _clone.AddComponent<PositionData>();
            _clone.GetComponent<PositionData>().loopFire = true;
            UpdatePosition(1);

            _clone.transform.position = _target.transform.position;

            //Register empty object to list of objects
            _target.listofObjects.Insert(insertAt, _clone.transform);
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);        

        GUILayout.BeginHorizontal();

        index = EditorGUILayout.IntField("Remove Position At", index);

        if (GUILayout.Button("Remove"))
        {
            RemovePosition(index);
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Reset Positions"))
        {            
            for (int index = _target.listofObjects.Count - 1; index >= 0; index--)
            {                
                RemovePosition(index);
            }
            numberOfPositions = 0;
        }

        //Ensure changes stick around
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }


    }

    void UpdatePosition(int _index)
    {
        numberOfPositions += _index;
    }

    void RemovePosition(int _index)
    {
        Entity_Enemy _target = (Entity_Enemy)target; //Creates a reference to directly have a pointer to the entity selected.

        //Get Object
        GameObject _obj = _target.listofObjects[_index].gameObject;

        //Remove Object from list and destroy Object
        _target.listofObjects.RemoveAt(_index);
        UpdatePosition(-1);

        DestroyImmediate(_obj);
    }

}
