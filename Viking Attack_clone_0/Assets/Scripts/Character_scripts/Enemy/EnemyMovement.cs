using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Mirror;

public class EnemyMovement : NetworkBehaviour
{
    [SerializeField] private int patrolRange;
    private Vector3 respawnPosWithoutY;
    private Rigidbody rigidBody;
    private Vector3 movingDirection;
    [Header("GroudCheck Settings")] [SerializeField]
    private GameObject groundCheck;
    private bool isGrounded;
    private LayerMask ground;
    private Collider[] colliders;
    [Header("Patrol Settnings")] [SerializeField]
    private float detectScopeRadius;
    private bool isGuarding;
    private bool isChasing;
    private bool backToDefault;
    private Vector3 posBeforeChasing; //save the position when enemy detected player
    private Collider[] sphereColliders;
    private GameObject chasingObject;
    [SerializeField] private float chasingSpeedMultiplier; // the multiplier for the movement speed of the enemy (1 if to move at same pace as the regular movement speed)
    [SerializeField] private int moveSpeed; // movement speed of the enemy
    [SerializeField] private CharacterBase characterBase; // the scriptable object that we fetch all the variables from

    // Syncs the position of the object to the server
    [SyncVar]
    [SerializeField] private Vector3 syncPosition;
    // Syncs the rotaion of the object to the server
    [SyncVar]
    [SerializeField] private Quaternion syncRotation;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        chasingSpeedMultiplier = characterBase.GetChasingSpeed();
        moveSpeed = characterBase.GetMovementSpeed();
        isGuarding = true;
        ground = LayerMask.GetMask("Ground");
        movingDirection = Vector3.forward;
        var position = transform.position; // Enemy starting position 
        respawnPosWithoutY = new Vector3(position.x, position.y, position.z);
        transform.position = position;
    }

    void Update()
    {

        colliders = Physics.OverlapBox(groundCheck.transform.position, new Vector3(0.1f, 0.1f, 0.1f),
            Quaternion.identity, ground); //Check if we are on the Ground
        if (colliders.Length > 0) //when we find the ground
        {
            isGrounded = true;
        }
        
        if (isGrounded) //start patrolling
        {
            if (isGuarding)
            {
                if (Vector3.Distance(transform.position, respawnPosWithoutY) >= patrolRange)
                {
                    movingDirection = -movingDirection;
                }

                rigidBody.velocity = movingDirection * moveSpeed * Time.fixedDeltaTime;
            }

            if (isChasing)
            {
                if (Vector3.Distance(transform.position, respawnPosWithoutY) >=
                    patrolRange) //if enemy chasing too far, back to the position before chasing
                {
                    isChasing = false;
                    backToDefault = true;
                }
                else
                {
                    if (chasingObject.Equals(null)) return;
                    Vector3 facePlayer = new Vector3(chasingObject.transform.position.x, transform.position.y,
                        chasingObject.transform.position.z);
                    transform.LookAt(facePlayer);
                    transform.position = Vector3.MoveTowards(transform.position, facePlayer,
                        chasingSpeedMultiplier * Time.fixedDeltaTime);
                }
            }
            else
            {
                CheckForPlayer();
            }

        }

        if (backToDefault)
        {
            if (Vector3.Distance(transform.position, respawnPosWithoutY) <= 3f)
            {
                backToDefault = false;
                isGuarding = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, respawnPosWithoutY,
                    chasingSpeedMultiplier * 1.5f * Time.deltaTime);
            }
        }
        //Foljande 2 rader skickar ett kommando till servern och da andrar antingen positionen eller rotationen samt HP
        CmdSetSynchedPosition(transform.position);
        CmdSetSynchedRotation(transform.rotation);
    }

    //Kommandlinjer for att be servern om uppdateringar po rotation och position
    [Command(requiresAuthority = false)]
    void CmdSetSynchedPosition(Vector3 position) => syncPosition = position;
    [Command(requiresAuthority = false)]
    void CmdSetSynchedRotation(Quaternion rotation) => syncRotation = rotation;

    private void CheckForPlayer()
    {
        sphereColliders = Physics.OverlapSphere(transform.position, detectScopeRadius);
        foreach (var coll in sphereColliders)
        {
            if (coll.tag == "Player") //find Player and start chasing
            {
                posBeforeChasing = transform.position;
                chasingObject = coll.gameObject;
                isGuarding = false;
                isChasing = true;
            }
        }
    }

    void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectScopeRadius);
    }
}