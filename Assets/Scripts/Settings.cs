using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : SingletonMono<Settings>
{
    // saving once instantiated prefabs to use it later.
    protected override void Start()
    {
        base.Start();
    }

    /*
    private GameObject Instantiate(Model _model)
    {
        if (_model == Model.None || _model >= Model.MAX)
        {
            Debug.LogError($"{_model} is invalid model!");
            return null;
        }
        //--------------------------------------------------
        if (m_instantiatedModelPrefabTable.ContainsKey(_model))
        {
            return m_instantiatedModelPrefabTable[_model];
        }
        //--------------------------------------------------
        GameObject _prefab = Resources.Load<GameObject>($"PolygonWar/Prefabs/Characters/{_model}");
        if (_prefab == null)
        {
            Debug.LogError("Prefab is missing!");
            return null;
        }
        //--------------------------------------------------
        GameObject _modelObj = Instantiate(_prefab);
        m_instantiatedModelPrefabTable.Add(_model, _modelObj);
        return _modelObj;
    }
    */
}

public static class ExtensionExample
{
    public static void Destroy(this GameObject[] array)
    {

    }
}
