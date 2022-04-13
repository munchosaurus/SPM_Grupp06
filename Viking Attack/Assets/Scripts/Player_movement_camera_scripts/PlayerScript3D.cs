using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScript3D : NetworkBehaviour
{
    [SerializeField] private State[] states;
    private MyRigidbody3D myRigidbody;
    private StateMachine stateMachine;
    public float jumpForce = 10f;
    public float acceleration  = 10f;
    public Camera mainCamera;
    public bool firstPerson;
    
    void Start()
    {
        //Vi ser om det inte är lokala spelaren, om det inte är det så förstör vi kameran som sitter på, optimering skulle ha en kamera i scenen som i sin tur används när spelaren laddas in
        if (!isLocalPlayer)
        {
            Destroy(mainCamera.gameObject);
            return;
        }
        myRigidbody = GetComponent<MyRigidbody3D>();
        if(states.Length > 0)    
            stateMachine = new StateMachine(this,states);
    }

    void Update()
    { //Avbryter alla uppdateringar som inte är den lokala spelaren
        if (!isLocalPlayer)
        {
            return;
        }
        //If there are any added states in the unity inspector
        if (states.Length > 0)      
            stateMachine.Update();
    }
    
    
    //Returns myRigidbody
    public MyRigidbody3D MyRigidbody3D => myRigidbody;
}
