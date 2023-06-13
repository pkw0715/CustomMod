using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCharacterController : FSM<BaseCharacterController>
{
    #region Constants and Properties
    //----------------------------------------
    int m_isWalkingHash;

    //----------------------------------------
    protected Animator m_animator;
    CharacterController m_charCtrl;
    //----------------------------------------
    int m_maxHP = 100;
    int m_hp;

    [SerializeField] float m_moveSpeed = 5f;
    [SerializeField] float m_turnSmoothTime = 0.1f;
    float m_turnSmoothVelocity;
    //----------------------------------------
    #endregion

    #region Public Properties
    public int HP { get { return m_hp; } }
    #endregion

    #region Public Methods
    public virtual void Damage(BaseCharacterController atacker) { }
    #endregion

    #region Methods
    protected virtual void Awake()
    {
        // initializing fields
        InitState(this, StateIdle.Instance);
    }

    protected virtual void Start()
    {
        //Initialize Hash-------------------------
        m_isWalkingHash = Animator.StringToHash("IsWalking");
        //Initialize Fields-----------------------
        m_animator = GetComponent<Animator>();
        m_charCtrl = GetComponent<CharacterController>();
        m_hp = m_maxHP;
        //Debug Section---------------------------
        if (m_animator == null)
            Debug.Log($"There is no Animator in {name}");
        if (m_charCtrl == null)
            Debug.Log($"There is no CharacterController in {name}");
    }

    protected virtual void Update()
    {
        FSMUpdate();
    }

    // moves character. yRotation is for moving character toward intended direction in accordance with camera.
    protected void Move(Vector3 dir, float yRotation = 0f)
    {
        if (m_charCtrl == null) return;

        float _sqrMag = dir.sqrMagnitude;
        if (_sqrMag != 0f)
        {
            // smoothly face character toward moving direction.
            float _targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + yRotation;
            float _rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref m_turnSmoothVelocity, m_turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _rotation, 0f);

            // moving
            Vector3 moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            m_charCtrl.Move(m_moveSpeed * Time.deltaTime * moveDir.normalized);

            // animation
            m_animator.SetFloat("Velocity", _sqrMag);
        }
    }
    #endregion

    #region Operator Overridings
    public static BaseCharacterController operator *(BaseCharacterController a, BaseCharacterController b)
    {
        throw new NotImplementedException();
    }
    #endregion
}
