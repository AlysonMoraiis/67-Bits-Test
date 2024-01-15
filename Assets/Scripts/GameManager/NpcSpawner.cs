using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 4f;
    
    [SerializeField] private PoolData _npcData;

    [SerializeField] private List<GameObject> _respawnPoints;
    

    private bool _canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner(_respawnTime));
    }

    private IEnumerator Spawner(float spawnTime)
    {
        while (_canSpawn)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnNpc();
        }
    }

    private void SpawnNpc()
    {
        PoolManager.Instance.SpawnFromPool(_npcData.Tag, GenerateRandomPosition(), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    private Vector3 GenerateRandomPosition()
    {

        int randomPosition = Random.Range(0, _respawnPoints.Count);

        return _respawnPoints[randomPosition].transform.position;
    }
}
