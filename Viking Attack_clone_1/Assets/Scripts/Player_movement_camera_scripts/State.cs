using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//Base class to all states
public abstract class State : ScriptableObject
{

    public object owner;
    public StateMachine stateMachine;
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
