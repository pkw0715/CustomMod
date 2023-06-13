using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    /*
    [SerializeField] private Transform m_playerBody;

    private float m_mouseSensitivity = 100f;
    private float m_xRotation;

    public float MouseSensitivity
    {
        get { return m_mouseSensitivity; }
    }

    private void Update()
    {
        float _mouseX = 0f;
        float _mouseY = 0f;

        if (TouchScreen.current.touches.Count > 0 && TouchScreen.current.touches[0].isInProgress)
        {
            _mouseX = TouchScreen.current.touches[0].delta.ReadValue().x;
            _mouseY = TouchScreen.current.touches[0].delta.ReadValue().y;
        }

        _mouseX *= m_mouseSensitivity;
        _mouseY *= m_mouseSensitivity;

        m_xRotation -= _mouseY * Time.deltaTime;
        m_xRotation = Mathf.Clamp(m_xRotation, -80, 80);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        m_playerBody.Rotate(Vector3.up * _mouseX * Time.deltaTime);
    }
    */
}
