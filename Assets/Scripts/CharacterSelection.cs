using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CharacterSelection : SingletonMono<CharacterSelection>
{
    [SerializeField] GameObject[] m_selectablePrefab;
    [SerializeField] List<GameObject> m_selectableInstance;
    //----------------------------------------
    [SerializeField] int m_index = 0;
    [SerializeField] GameObject m_currentlySelected;

    public void NextCharacter(bool _left)
    {
        Debug.Log(_left ? "left" : "right");
        HideCharacer(m_index);
        if (_left)
        {
            if (m_index == 0) m_index = m_selectableInstance.Count - 1;
            else m_index--;
        }
        else
        {
            if (m_index == m_selectableInstance.Count - 1) m_index = 0;
            else m_index++;
        }
        m_currentlySelected = ShowCharacter(m_index);
    }

    private GameObject ShowCharacter(int _index)
    {
        if (_index >= m_selectableInstance.Count)
        {
            Debug.Log($"index is too big! The size of the list is {m_selectableInstance.Count}.");
            Debug.Log(m_selectableInstance == null);
            return null;
        }
        m_selectableInstance[_index].SetActive(true);
        m_selectableInstance[_index].transform.localPosition = new Vector3(0, 1, -15f);
        m_selectableInstance[_index].transform.rotation = Quaternion.Euler(0, 180f, 0);
        return m_selectableInstance[_index];
    }

    private void HideCharacer(int _index)
    {
        m_selectableInstance[_index].SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        if (m_selectableInstance == null || m_selectableInstance.Count == 0)
        {
            m_selectableInstance = new List<GameObject>();
            for (int i = 0; i < m_selectablePrefab.Length; i++)
            {
                GameObject _obj = Instantiate(m_selectablePrefab[i]);
                _obj.transform.parent = transform;
                _obj.transform.localPosition = Vector3.zero;
                _obj.SetActive(false);
                m_selectableInstance.Add(_obj);
            }
        }
        //----------------------------------------
        m_currentlySelected = ShowCharacter(m_index);
    }


}
