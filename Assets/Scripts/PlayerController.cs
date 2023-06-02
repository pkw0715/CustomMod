using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    //----------------------------------------
    [SerializeField] Joystick m_joystick;
    Camera m_mainCam;

    //----------------------------------------
    [SerializeField]float m_cameraDir;
    [SerializeField] float m_moveSpeed = 5;
    bool m_cameraMoved = false;

    //----------------------------------------
    protected override void Start()
    {
        base.Start();
        m_mainCam = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        // The direction camera is looking at
        m_cameraDir = m_mainCam.transform.rotation.eulerAngles.y;

        // Moving Part
        float _x = Input.GetAxis("Horizontal") * Time.deltaTime * m_moveSpeed;
        float _z = Input.GetAxis("Vertical") * Time.deltaTime * m_moveSpeed;
        if (_x != 0 || _z != 0)
        {
            Move(new Vector3(_x, 0, _z), m_cameraDir);
        }
        else
        {
            m_animator.SetBool("IsWalking", false);
        }

        // temp
        // Camera moving
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_cameraMoved)
            {
                m_cameraMoved = false;
                m_mainCam.transform.localPosition = new Vector3(0, 1, -10);
                m_mainCam.transform.rotation = Quaternion.identity;
            }
            else
            {
                m_cameraMoved = true;
                m_mainCam.transform.localPosition = new Vector3(-10, 1, 0);
                m_mainCam.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }
}
