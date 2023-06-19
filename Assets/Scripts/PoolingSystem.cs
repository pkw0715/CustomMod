using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    #region Constants and Fields
    [System.Serializable]
    public class PoolingUnit
    {
        public string Name;
        public GameObject PrefObj;
        public int Amount;
        int m_curAmount;

        public int CurAmount
        {
            get { return m_curAmount; }
            set { m_curAmount = value; }
        }
    }
    public static PoolingSystem Instance;

    public PoolingUnit[] PoolingUnits;
    public List<GameObject>[] PooledUnitsList;

    public int DefPoolAmount = 5;
    public bool CanPoolExpand = true;

    #endregion

    #region Public Methods

    public GameObject InstantiateAPS(int idx, GameObject parent = null)
    {
        string pooledObjName = PoolingUnits[idx].Name;
        GameObject tmp = InstantiateAPS(pooledObjName, Vector3.zero,
                                        PoolingUnits[idx].PrefObj.transform.rotation,
                                        PoolingUnits[idx].PrefObj.transform.localScale,
                                        parent);

        return tmp;
    }

    public GameObject InstantiateAPS(
        int idx,
        Vector3 pos,
        Quaternion rot,
        Vector3 scale,
        GameObject parent = null)
    {
        string pooledObjName = PoolingUnits[idx].Name;
        GameObject tmp = InstantiateAPS(pooledObjName, pos, rot, scale, parent);

        return tmp;
    }

    public GameObject InstantiateAPS(string pooledObjName, GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);
        tmpObj.SetActive(true);
        return tmpObj;
    }

    public GameObject InstantiateAPS(
        string pooledObjName,
        Vector3 pos,
        Quaternion rot,
        Vector3 scale,
        GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);

        if (tmpObj != null)
        {
            if (parent != null)
                tmpObj.transform.parent = parent.transform;

            tmpObj.transform.position = pos;
            tmpObj.transform.rotation = rot;
            tmpObj.transform.localScale = scale;
            tmpObj.SetActive(true);
        }

        return tmpObj;
    }

    public List<GameObject> GetActivePooledItems()
    {
        List<GameObject> list = new List<GameObject>();

        for (int unitIdx = 0; unitIdx < PoolingUnits.Length; unitIdx++)
        {
            for (int listIdx = 0; listIdx < PooledUnitsList[unitIdx].Count; listIdx++)
            {
                if (PooledUnitsList[unitIdx][listIdx].activeInHierarchy)
                {
                    list.Add(PooledUnitsList[unitIdx][listIdx]);
                }
            }
        }

        return list;
    }

    public static void DestroyAPS(GameObject obj) { obj.SetActive(false); }

    public static void PlayEffect(ParticleSystem particleSystem)
    {
        if (particleSystem == null)
            return;

        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
    }

    public static void PlayEffect(GameObject obj)
    {
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        particleSystem.Play();
    }

    public static void PlayEffect(ParticleSystem particleSystem, int emitCount)
    {
        if (particleSystem == null)
            return;

        particleSystem.gameObject.SetActive(true);
        particleSystem.Emit(emitCount);
    }

    public static void PlayEffect(GameObject obj, int emitCount)
    {
        ParticleSystem tmp = obj.GetComponent<ParticleSystem>();
        if (tmp == null) return;
        PlayEffect(tmp, emitCount);
    }

    public static void PlaySoundRepeatedly(GameObject soundObj, float volume = 1.0f)
    {
        AudioSource tmp = soundObj.GetComponent<AudioSource>();
        if (tmp == null) return;
        if (tmp.isPlaying) return;

        if (tmp)
        {
            tmp.Play();
            tmp.loop = true;
            tmp.volume = volume;
        }
    }

    public static void PlaySound(GameObject soundObj, float volume = 1.0f)
    {
        AudioSource tmp = soundObj.GetComponent<AudioSource>();
        if (tmp == null) return;
        
        if (tmp)
        {
            tmp.PlayOneShot(tmp.clip);
            tmp.volume = volume;
        }
    }

    public static void StopSound(GameObject soundObj)
    {
        AudioSource audioSource = soundObj.GetComponent<AudioSource>();

        if (audioSource != null) audioSource.Stop();
    }

    #endregion

    #region Methods

    void Awake()
    {
        Instance = this;
        PooledUnitsList = new List<GameObject>[PoolingUnits.Length];

        for (int i = 0; i < PoolingUnits.Length; i++)
        {
            PooledUnitsList[i] = new List<GameObject>();

            if (PoolingUnits[i].Amount > 0)
                PoolingUnits[i].CurAmount = PoolingUnits[i].Amount;
            else
                PoolingUnits[i].CurAmount = DefPoolAmount;

            for (int j = 0; j < PoolingUnits[i].CurAmount; j++)
            {
                GameObject newItem = Instantiate(PoolingUnits[i].PrefObj);
                AddToPooledUnitsList(i, newItem, $"_{j}");
            }
        }
    }

    GameObject GetPooledItem(string pooledObjName)
    {
        for (int unitIdx = 0; unitIdx < PoolingUnits.Length; unitIdx++)
        {
            if (PoolingUnits[unitIdx].PrefObj.name == pooledObjName)
            {
                int listIdx;
                for (listIdx = 0; listIdx < PooledUnitsList[unitIdx].Count; listIdx++)
                {
                    if (PooledUnitsList[unitIdx][listIdx] == null)
                        return null;

                    if (PooledUnitsList[unitIdx][listIdx].activeInHierarchy == false)
                        return PooledUnitsList[unitIdx][listIdx];
                }

                if (CanPoolExpand)
                {
                    GameObject tempObj = Instantiate(PoolingUnits[unitIdx].PrefObj);
                    string suffix = $"_{listIdx}({listIdx - PoolingUnits[unitIdx].CurAmount + 1})";
                    AddToPooledUnitsList(unitIdx, tempObj, suffix);

                    return tempObj;
                }

                break;
            }
        }

        return null;
    }

    void AddToPooledUnitsList(int idx, GameObject newItem, string suffix)
    {
        newItem.name += suffix;
        newItem.SetActive(false);
        newItem.transform.parent = transform;
        PooledUnitsList[idx].Add(newItem);
    }

    #endregion
}

public static class PoolingSystemExtensions
{
    public static void DestroyAPS(this GameObject obj)
    {
        PoolingSystem.DestroyAPS(obj);
    }

    public static void PlaySoundRepeatedly(this GameObject soundObj, float volume = 1.0f)
    {
        PoolingSystem.PlaySoundRepeatedly(soundObj, volume);
    }

    public static void PlaySound(this GameObject soundObj, float volume = 1.0f)
    {
        PoolingSystem.PlaySound(soundObj, volume);
    }

    public static void StopSound(this GameObject soundObj)
    {
        PoolingSystem.StopSound(soundObj);
    }

    public static void PlayEffect(this GameObject effectObj, int emitCount)
    {
        PoolingSystem.PlayEffect(effectObj, emitCount);
    }

    public static void PlayEffect(this GameObject effObj)
    {
        PoolingSystem.PlayEffect(effObj);
    }
}
