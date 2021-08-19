using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnerBehaviour : MonoBehaviour
{
    public List<GameObject> listOfObjts;
    public List<Transform> listOfPositions;

    private Vector3 oldPosition;

    private int count;
    [SerializeField] private float numberOfSecondToNextSpawn;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        count++;
        if (count >= numberOfSecondToNextSpawn * 60)
        {
            Instantiate(chooseRandomObject(), chooseRandomPosition(), Quaternion.identity);
            count = 0;
        }

    }

    private GameObject chooseRandomObject()
    {
        return listOfObjts[Random.Range(0, listOfObjts.Count)];
    }

    private Vector3 chooseRandomPosition()
    {
        bool choosing = true;
        while (choosing)
        {
            Vector3 _chosen = listOfPositions[Random.Range(0, listOfPositions.Count)].position;
            if (_chosen != oldPosition)
            {
                oldPosition = _chosen;
                choosing = false;
                break;
            }
        }

        return oldPosition;

    }
}
