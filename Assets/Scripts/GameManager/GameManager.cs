using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private List<GameObject> listWheel = new List<GameObject>();
    public int mountObjectWheel = 10;

    private void Awake()
    {
        GameObject wheelEnemy = Resources.Load<GameObject>("WheelObject/WheelEnemy");
        if (wheelEnemy == null)
        {
            Debug.LogWarning("Null Enemy Wheel");
            return;
        }
        PoolingObjects.PoolObject(wheelEnemy, listWheel, mountObjectWheel);
    }

    private void Start()
    {
        InitObject();
    }

    void InitObject()
    {
        // Spawn object wheel 
        for (int i = 0; i < listWheel.Count; i++)
        {
            listWheel[i].SetActive(false);
        }
    }
}
