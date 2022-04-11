using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript3D : MonoBehaviour
{
    private MyRigidbody3D rb;
    [SerializeField] private State[] states;
    public float jumpForce = 10f;
    public float acceleration  = 10f;
    public Camera mainCamera;
    public bool firstPerson;
    private StateMachine st;
    void Awake()
    {
        rb = GetComponent<MyRigidbody3D>();
        if(states.Length > 0)    
            st = new StateMachine(this,states);
    }

    void Update()
    {  
        if(states.Length > 0)      
            st.Update();
    }

    public MyRigidbody3D getMyRigidbody3D()
    {
        return rb;
    }
}
