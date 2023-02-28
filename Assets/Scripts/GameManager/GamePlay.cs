using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlay : MonoBehaviour
{
    public static GamePlay instance;
    public static GamePlay Instance
    {
        get => instance;
    }
    public List<GameObject> listWheel = new List<GameObject>();
    public List<GameObject> pointSpawn = new List<GameObject>(); 

    public int mountObjectWheel = 10;
    public float minTimeSpawnNewWheel = 3.5f;
    public float maxTimeSpawnNewWheel = 5f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        PoolingObjectList();
    }

    private void Start()
    {
        SpawnObject();
    }

    void PoolingObjectList()
    {
        WheelPooling();
    }

    void WheelPooling()
    {
        PoolingObjectByName(StaticPath.WHEEL_ENEMY);
    }

    void PoolingObjectByName(string namePath)
    {
        GameObject objPool = Resources.Load<GameObject>(namePath);
        if (objPool == null)
        {
            Debug.LogWarning("Null obj" + namePath);
            return;
        }
        PoolingObjects.PoolObject(objPool, listWheel, mountObjectWheel);
    }
    
    public void SpawnObject()
    {
        // Spawn object wheel 
        int mountRandomWheel = Random.Range(1, 3);
        for (int i = 0; i < mountRandomWheel; i++)
        {
            GameObject objPool = PoolingObjects.GetObjectPool(listWheel);
            if(objPool == null) continue;
            Wheel currentWheel = objPool.GetComponent<Wheel>();
            currentWheel.SetPosition(pointSpawn[Random.Range(0, pointSpawn.Count)].transform.position);
            currentWheel.RandomDirecton();
            currentWheel.gameObject.SetActive(true);
        }
        StartCoroutine(TimeSpawnObject());
    }

    IEnumerator TimeSpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(minTimeSpawnNewWheel, maxTimeSpawnNewWheel));
        SpawnObject();
    }
}
