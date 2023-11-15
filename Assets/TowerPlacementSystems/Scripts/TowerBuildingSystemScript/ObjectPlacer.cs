using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new List<GameObject>();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newTower = Instantiate(prefab);
        newTower.transform.position = position;
        placedGameObjects.Add(newTower);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(int gameTowerIndex)
    {
        if (placedGameObjects.Count <= gameTowerIndex)
            return;
        
        Destroy(placedGameObjects[gameTowerIndex]);
        placedGameObjects[gameTowerIndex] = null;
    }
}