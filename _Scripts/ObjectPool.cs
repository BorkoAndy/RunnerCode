using System.Collections.Generic;
using UnityEngine;

public  class ObjectPool
{   
    private GameObject _prefab;
    private GameObject[] _prefabArray;
    private int _poolSize = 1;
    public ObjectPool(GameObject prefab, int poolSize)
    {
        _prefab= prefab;
        _poolSize = poolSize;
        CreatePool();
    }
    public ObjectPool(GameObject[] prefabArray, int poolSize) 
    {
        _prefabArray = prefabArray; 
        _poolSize = poolSize;
        CreatePool(_prefabArray);
    }   

    protected Queue<GameObject> _objectPool = new Queue<GameObject>();

    public GameObject GetFromPool()
    {        
        if (_objectPool.Count == 0)        
            CreateNewPoolObject(_prefabArray);        

        GameObject newPoolObject = _objectPool.Dequeue();        
        return newPoolObject;
    } 
    
    public void ReturnToPool(GameObject objectToReturn)
    {        
        objectToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objectToReturn);
    }
    
    private void CreatePool(GameObject[] poolArray = null)
    {
        for (int i = 0; i < _poolSize; i++)        
            CreateNewPoolObject(poolArray);       
    }   
    private void CreateNewPoolObject(GameObject[] poolArray = null)
    {
        GameObject prefab;
        if (poolArray != null)        
            prefab = poolArray[Random.Range(0, poolArray.Length)];        
        else
            prefab = _prefab;


        GameObject newObject = GameObject.Instantiate(prefab);
        newObject.SetActive(false);
        _objectPool.Enqueue(newObject);

    }
}
