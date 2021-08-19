using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{

    string testString = "";
    Color color;

    [MenuItem("Window/Example")] //Attribute that calls the window.

    //Custom method to open this window - name of method doesn't seem to matter, as long as its public static void.
    public static void Initialise()
    {
        GetWindow<ExampleWindow>("Ultra Cool Stuff");
    }


    private void OnGUI()
    {
        //GUILayout has functions for many stuff
        GUILayout.Label("This is a label", EditorStyles.boldLabel);

        //EditorGUILayout generally is just for making fields that can be edited.
        //Giving this textfield a variable allows us to directly export the field output into the variable for editing anytime.
        testString = EditorGUILayout.TextField("Name", testString);

        if (GUILayout.Button("Press me"))
        {
            Debug.Log("My name is " + testString);

        }

        GUILayout.Label("Colour the selected objects", EditorStyles.boldLabel);
        GUILayout.Label("Allows us to colour selected objects immediately.");

        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("COLORISE"))
        {
            foreach (GameObject _test in Selection.gameObjects)
            {
                Renderer _renderer = _test.GetComponent<Renderer>();
                if (_renderer != null)
                {
                    _renderer.sharedMaterial.color = color;
                    Debug.Log("colored");
                }
            }

        }
    }
}
