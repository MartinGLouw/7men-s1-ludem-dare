using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolableObjects : Singleton<PoolableObjects>
{
       
    //Using a class so we can make a list of poolable objects so we don't have to create multiple
    [System.Serializable]
    public class NewPoolableObject
    {
        public BulletType type; 
        public GameObject prefab;
        public int sizeOfPool;
        public Transform parent;
    }
    
    public NewPoolableObject[] ObjectPools;

    private Dictionary<BulletType, Queue<GameObject>> pooledObjects;

    private void Awake()
    {
        pooledObjects = new Dictionary<BulletType, Queue<GameObject>>();

        foreach (NewPoolableObject pool in ObjectPools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.sizeOfPool; i++)
            {
                GameObject newObj = Instantiate(pool.prefab, pool.parent);
                newObj.SetActive(false);
                objectQueue.Enqueue(newObj);
            }
            pooledObjects.Add(pool.type, objectQueue);
        }
    }

    public GameObject GetObject(BulletType type, Vector3 position)
    {
        if (!pooledObjects.ContainsKey(type))
        {
            Debug.LogWarning($"No pool exists for type: {type}");
            return null;
        }

        Queue<GameObject> objectQueue = pooledObjects[type];
        
        if (objectQueue.Count == 0)
        {
            NewPoolableObject pool = System.Array.Find(ObjectPools, p => p.type == type);
            if (pool != null)
            {
                GameObject newObj = Instantiate(pool.prefab, pool.parent);
                newObj.SetActive(true);
                newObj.transform.position = position;
                IPoolObject poolScript = newObj.GetComponent<IPoolObject>();
                poolScript?.Initialize();
                return newObj;
            }
        }

        GameObject obj = objectQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        IPoolObject poolObj = obj.GetComponent<IPoolObject>();
        poolObj?.Initialize();
        
        return obj;
    }

    public void ReturnObject(BulletType type, GameObject obj)
    {
        if (!pooledObjects.ContainsKey(type))
        {
            Debug.LogError("Returned object has no matching pool.");
            return;
        }

        obj.SetActive(false);
        pooledObjects[type].Enqueue(obj);
    }

}
