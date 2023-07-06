using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectsToPool
    {
        public GameObject objectToPool;
        public int noOfObjects;
    }

    public static ObjectPooler SharedInstance;
    public ObjectsToPool[] objectsToPool;
    Dictionary<string, List<GameObject>> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();
        foreach(ObjectsToPool obj in objectsToPool)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < obj.noOfObjects;i++){
                GameObject go = Instantiate(obj.objectToPool, transform.position, Quaternion.identity);
                go.SetActive(false);
                go.transform.SetParent(this.transform);
                list.Add(go);
            }
            pooledObjects.Add(obj.objectToPool.name, list);
        }
    }

    public GameObject GetPooledObject(string objName)
    {
        List<GameObject> objList= pooledObjects[objName];

        for (int i = 0; i < objList.Count; i++)
        {
            if (!objList[i].activeInHierarchy)
            {
                return objList[i];
            }
        }

        foreach(ObjectsToPool obj in objectsToPool)
        {
            if(obj.objectToPool.name==objName)
            {
                GameObject go = Instantiate(obj.objectToPool, transform.position, Quaternion.identity);
                go.SetActive(false);
                go.transform.SetParent(this.transform);
                objList.Add(go);
                return go;
            }
        }
        return null;
    }
}
