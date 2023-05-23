using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> : MonoBehaviour
{
    private T owner;
    private IFSMState<T> m_currentState = null;
    private IFSMState<T> m_previousState = null;

    public IFSMState<T> CurrentState { get { return m_currentState; } }
    public  IFSMState<T> PreviousState { get { return m_previousState; } }

    protected void InitState(T owner, IFSMState<T> initialState)
    {
        this.owner = owner;
        ChangeState(initialState);
    }

    protected void FSMUpdate()
    {
        if (m_currentState != null)
        {
            CurrentState.Execute(owner);
        }
    }

    public void ChangeState(IFSMState<T> newState)
    {
        m_previousState = m_currentState;

        if (PreviousState != null)
            PreviousState.Exit(owner);

        m_currentState = newState;

        if (m_currentState != null)
            CurrentState.Enter(owner);
    }

    public void RevertState()
    {
        if (m_previousState != null)
            ChangeState(m_previousState);
    }

    public override string ToString()
    {
        return CurrentState.ToString();
    }
}
