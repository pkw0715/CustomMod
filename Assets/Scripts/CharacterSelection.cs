using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] _buildingPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Buildings");
        GameObject[] _characterPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Characters");
        GameObject[] _environmentPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Environments");
        GameObject[] _fxPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/FX");
        GameObject[] _propPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Props");
        GameObject[] _vehiclePrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Vehicles");
        GameObject[] _weaponPrefabs = Resources.LoadAll<GameObject>("PolygonWar/Prefabs/Weapons");

        GameObject[][] _prefabsArray = new GameObject[][] { _buildingPrefabs, _characterPrefabs, _environmentPrefabs, _fxPrefabs, _propPrefabs, _vehiclePrefabs, _weaponPrefabs };

        List<PoolingSystem.PoolingUnit> _tempList = new List<PoolingSystem.PoolingUnit>();

        foreach (GameObject[] _prefabs in _prefabsArray)
        {
            foreach (GameObject _prefab in _prefabs)
            {
                if (Random.Range(0, 2) == 0) break;

                PoolingSystem.PoolingUnit _unit = new PoolingSystem.PoolingUnit();
                _unit.Name = _prefab.name;
                _unit.PrefObj = _prefab;
                _unit.Amount = _prefabs.Equals(_characterPrefabs) ? 1 : 5;

                _tempList.Add(_unit);
            }
        }

        PoolingSystem.Instance.PoolingUnits = _tempList.ToArray();
        PoolingSystem.Instance.MakePool();

        _buildingPrefabs.Destroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
