using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;

public class PlayerScript3D : MonoBehaviour
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
        //Vi ser om det inte �r lokala spelaren, om det inte �r det s� f�rst�r vi kameran som sitter p�, optimering skulle ha en kamera i scenen som i sin tur anv�nds n�r spelaren laddas in
        // if (!isLocalPlayer)
        // {
        //     Destroy(mainCamera.gameObject);
        //     return;
        // }
        myRigidbody = GetComponent<MyRigidbody3D>();
        if(states.Length > 0)    
            stateMachine = new StateMachine(this,states);
    }

    void Update()
    { //Avbryter alla uppdateringar som inte �r den lokala spelaren
        // if (!isLocalPlayer)
        // {
        //     return;
        // }
        //If there are any added states in the unity inspector
        if (states.Length > 0)      
            stateMachine.Update();
    }
    
    
    //Returns myRigidbody
    public MyRigidbody3D MyRigidbody3D => myRigidbody;
}
