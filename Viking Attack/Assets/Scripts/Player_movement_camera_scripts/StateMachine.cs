using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{ 
    private State currentState;
    private State[] stateList;
    private Queue<State> stateQueue = new Queue<State>();
    public StateMachine(object controller, State[] states)
    {
        stateList = new State[states.Length];
        //Instantiate all states and add them to local list
        for (int i = 0; i < states.Length; i++)
        {
            State temp = UnityEngine.Object.Instantiate(states[i]);
            temp.owner = controller;
            temp.stateMachine = this;
            stateList[i] = temp;
        }
        currentState = stateList[0];
        currentState.Enter();
    }
    public void Update()
    {
        //Updated states in the order of addition
        if(stateQueue.Count > 0)
        {
            State temp = stateQueue.Dequeue();
            temp.Update();
            currentState = temp;
        }
        else
            currentState.Update();
    }
    //Changes state
    public void ChangeState<T>() where T : State
    {
        foreach(State s in stateList)
            if(s.GetType() == typeof(T))
            {
                currentState.Exit();
                stateQueue.Enqueue(s);
                currentState.Enter();
            }
                
    }
}
