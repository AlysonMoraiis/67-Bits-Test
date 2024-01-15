using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Queue<GameObject>> _poolDictionary;
    [SerializeField] private List<PoolData> _pools;

    public static PoolManager Instance;

    private void Awake()
    {
        Instance = this;
        InstantiatePoolObjects();
    }

    private void InstantiatePoolObjects()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab, this.gameObject.transform, true);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        
        GameObject objectToSpawn =  _poolDictionary[tag].Dequeue();
        
        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        _poolDictionary[tag].Enqueue((objectToSpawn));

        return objectToSpawn;
    }
}
