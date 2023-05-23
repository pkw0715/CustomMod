using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    public static T Instance
    {
        get;
        private set;
    }

    static Singleton()
    {
        if (Instance == null)
            Instance = new T();
    }

    public virtual void Clear()
    {
        Instance = null;
    }
}

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    static Object m_lock = new Object();

    static T m_inst = null;
    bool m_isInit = false;

    public static T Instance
    {
        get
        {
            if (m_inst == null && Time.timeScale != 0)
            {
                lock (m_lock)
                    CreateInst();

            }

            return m_inst;
        }
    }

    static void CreateInst()
    {
        m_inst = FindObjectOfType(typeof(T)) as T;

        if (m_inst == null)
        {
            m_inst = new GameObject("SingleTon Of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

            if (m_inst == null)
                Debug.LogError("Problem during the creation of " + typeof(T).ToString());
            else
                DontDestroyOnLoad(m_inst);
        }
        else
            DontDestroyOnLoad(m_inst);

        if (!m_inst.m_isInit)
            m_inst.Init();

    }

    void Awake()
    {
        if (m_inst == null)
        {
            m_inst = this as T;
        }
    }

    protected virtual void Init() { m_isInit = true; }

    private void OnDestroy()
    {
        m_inst = null;
        Time.timeScale = 0;
    }
}
