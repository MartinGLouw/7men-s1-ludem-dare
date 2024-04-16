using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObjects : Singleton<PoolableObjects>
{
       
    //Using a class so we can make a list of poolable objects so we don't have to create multiple
    [System.Serializable]
    public class NewPoolableObject
    {
        public BulletType type;
        public GameObject Prefab;
        public int sizeOfPool;
        public Transform parent;
    }
    
    public NewPoolableObject[] ObjectPools;

    public Dictionary<BulletType, List<GameObject>> pooledObjects;

    private void Awake()
    {
        pooledObjects = new Dictionary<BulletType, List<GameObject>>();
        
        GameObject objectToPool;

        foreach(NewPoolableObject pool in ObjectPools)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < pool.sizeOfPool; i++)
            {
                objectToPool = Instantiate(pool.Prefab, pool.parent);
                objectToPool.SetActive(false);
                gameObjects.Add(objectToPool);
            }
            pooledObjects.Add(pool.type, gameObjects);
        }
    }

    public void SpawnFromPool(BulletType type, Vector3 position, Quaternion rotation)
    {
        if(!pooledObjects.ContainsKey(type))
        {
            Debug.LogWarning($"Pool with index {type} does not exist!");
            return;
        }

        foreach(GameObject obj in pooledObjects[type])
        {
            GameObject objectToSpawn = obj;
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

        }
    }

    public GameObject GetObject(BulletType poolIndex, Vector3 pos)
    {
        //Debug.Log("New pool index" + poolIndex);
        int size = pooledObjects[poolIndex].Count;
        //Debug.Log("New name: " + pooledObjects[0][size - 1].name);
        

        //GameObject obj = pooledObjects[poolIndex][size - 1];
        GameObject obj = pooledObjects[poolIndex][0];   

        if (obj != null)
        {
            pooledObjects[poolIndex].Remove(obj);
            obj.transform.position = pos;
            obj.SetActive(true); 
            IPoolObject pool = obj.GetComponent<IPoolObject>();
            if(pool != null)
            {
                pool.Initialize();
            }
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnObject(BulletType poolIndex, GameObject obj)
    {
        Debug.Log(obj);
        if (obj == null)
        {
            Debug.Log("Object returning not working for index: " + poolIndex);
            return;
            
        }

        obj.SetActive(false);
        pooledObjects[poolIndex].Add(obj);
    }

}
