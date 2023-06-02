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

    Vector3 m_moveSpeed;
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
    protected void Move(Vector3 move, float yRotation = 0)
    {
        if (m_charCtrl == null) return;
        Vector3 _motion = Quaternion.Euler(0, yRotation, 0) * move;
        m_animator.SetBool(m_isWalkingHash, true);
        m_charCtrl.Move(_motion);
        
        transform.LookAt(transform.position + _motion.normalized);
    }
    #endregion

    #region Operator Overridings
    public static BaseCharacterController operator *(BaseCharacterController a, BaseCharacterController b)
    {
        throw new NotImplementedException();
    }
    #endregion
}
