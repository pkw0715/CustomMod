using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T m_instance;
    static object m_lock = new object();

    public static T Instance
    {
        get
        {
            lock (m_lock)
            {
                if (m_instance == null) 
                {
                    m_instance = FindObjectOfType(typeof(T)) as T;

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("--- FSMSingleton error ---");
                        return m_instance;
                    }

                    if (m_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                    }
                    else
                        Debug.LogError("--- FSMSingleton already exists ---");

                }

                return m_instance;
            }
        }
    }
}
