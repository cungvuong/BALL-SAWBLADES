using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObjects : MonoBehaviour
{
    public static void PoolObject(GameObject obj, List<GameObject> list, int mount)
    {
        for (int i = 0; i < mount; i++)
        {
            GameObject objPool = Instantiate(obj, Vector3.zero, Quaternion.identity);
            objPool.SetActive(false);
            list.Add(objPool);
        }
    }

    public static GameObject GetObjectPool(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
            {
                return list[i];
            }
        }
        return null;
    }
}
