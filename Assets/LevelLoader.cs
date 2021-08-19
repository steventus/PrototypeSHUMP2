using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //Loads a map using textfile
    public TextAsset beatmapTextFile;

    //Instructions

    //Time 

    void Start()
    {
        
    }

    private void LoadMap()
    {
        string[] datalines = beatmapTextFile.text.Split('\n'); //Split by lines
        string[][] datapairs = new string[datalines.Length][]; //Create 2D array
        int linenumber = 0;

        foreach (string line in datalines) //Assign, to each datapair, the corresponding dataline which is split by ','
        {
            datapairs[linenumber] = datalines[linenumber].Split(',');
            linenumber++;
        }

        //Useful to make variable size 1 dimensinoal array
        float length = datapairs.GetLength(0);
        List<int> TEMPnotesdirection = new List<int>();
        List<float> TEMPnotes = new List<float>();

        //Debug.Log("Length: "+length);
        for (int y = 0; y < length; y++)
        {
            TEMPnotesdirection.Add(int.Parse(datapairs[y][1]));
            //notesdirection = TEMPnotesdirection.ToArray();
        }

        for (int x = 0; x < length; x++)
        {
            TEMPnotes.Add(float.Parse(datapairs[x][0]));
            //notes = TEMPnotes.ToArray();
        }
    }

    void Update()
    {
        
    }
}
