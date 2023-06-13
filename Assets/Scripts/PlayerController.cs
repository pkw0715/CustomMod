using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    //----------------------------------------
    private Joystick m_joystick;
    private Camera m_refCam;

    //----------------------------------------
    [SerializeField]float m_cameraDir;
    private bool m_cameraMoved = false;

    //----------------------------------------
    protected override void Start()
    {
        base.Start();
        m_joystick = UIManager.Instance.Joystick;
        m_refCam = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        // The direction camera is looking at
        m_cameraDir = m_refCam.transform.rotation.eulerAngles.y;

        // Moving part with joystick
        if (m_joystick.Horizontal != 0 || m_joystick.Vertical != 0)
        {
            Move(new Vector3(m_joystick.Horizontal, 0f, m_joystick.Vertical), m_refCam.transform.eulerAngles.y);
        }
        else
        {
            m_animator.SetFloat("Velocity", 0);
        }

        /*
        // faster when LeftShift is pressed.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_moveSpeed = 7;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_moveSpeed = 5;
        }

        // Moving Part with wasd
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
        */

        // temp
        // Camera moving
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_cameraMoved)
            {
                m_cameraMoved = false;
                m_refCam.transform.localPosition = new Vector3(0, 1, -10);
                m_refCam.transform.rotation = Quaternion.identity;
            }
            else
            {
                m_cameraMoved = true;
                m_refCam.transform.localPosition = new Vector3(-10, 1, 0);
                m_refCam.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }
}
