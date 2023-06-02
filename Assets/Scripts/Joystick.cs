using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    GameObject m_stick;


    void ReturnToZero()
    {
        m_stick.transform.localPosition = Vector3.zero;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
