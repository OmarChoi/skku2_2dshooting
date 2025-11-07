using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private float _spawnChance = 0.5f;

    [SerializeField]
    private GameObject[] _itemPrefabs;

    [Header("스폰 확률")]
    private int _totalWeight = 0;
    private int[] _probabilityWeights = new int[] { 10, 10, 10 };

    private void Start()
    {
        foreach (int weight in _probabilityWeights)
        {
            _totalWeight += weight;
        }
    }

    public void SpawnItem(Vector3 position)
    {
        if (UnityEngine.Random.value < _spawnChance) return;
        CreateItem(ref position);
    }

    private void CreateItem(ref Vector3 position)
    {
        EItemType itemType = (EItemType)Utils.GetRandomIndexByWeight(_totalWeight, _probabilityWeights);
        GameObject item = Instantiate(_itemPrefabs[(int)itemType]);
        item.transform.position = position;
    }
}
