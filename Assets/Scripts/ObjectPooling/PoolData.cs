using UnityEngine;

[CreateAssetMenu(menuName = "PoolData")]
public class PoolData : ScriptableObject
{
    [SerializeField] private string _tag;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _size;

    public string Tag => _tag;
    public GameObject Prefab => _prefab;
    public int Size => _size;
}