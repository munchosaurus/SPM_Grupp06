using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;

    [SerializeField] private int maxMoveDistans;

    //[SerializeField] private Transform respawnPosition;
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

    [SerializeField] private float chasingSpeed;
    private bool isGuarding;
    private bool isChasing;
    private bool backToDefault;
    private Vector3 posBeforeChasing; //save the position when enemy detected player
    private Collider[] sphereColliders;
    private GameObject chasingObject;

    // Martin variables down
    [SerializeField] LayerMask playerMask;
    // TODO: Fetch enemy information in prefab that determines range, cooldown, damage
    // PLACEHOLDER BELOW
    [SerializeField] private float range = 1f;
    [SerializeField] private float attackCooldown = 0.9f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float cooldown;

    void Start()
    {
        isGuarding = true;
        rigidBody = GetComponent<Rigidbody>();
        ground = LayerMask.GetMask("Ground");
        movingDirection = Vector3.forward;
        var position = transform.position; // Enemy starting position 
        respawnPosWithoutY = new Vector3(position.x, position.y, position.z);
        position = respawnPosWithoutY;
        transform.position = position;
    }

    private void FixedUpdate()
    {
        /*
        * START OF RAYCAST, MARTINS CODE
         * handles raycasting towards player and checks if a hit is performed
        */
        if (cooldown < attackCooldown)
        {
            cooldown += Time.fixedDeltaTime;
            //Debug.Log(cooldown);
        }
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,
                playerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);
            //Debug.Log("Did Hit");
            if (hit.distance < range && cooldown > attackCooldown)
            {
                cooldown = 0;
                Debug.Log("JAG KAN ATTACKERA NU");
                //EnemyAttack.Attack(damage);
                //Debug.Log("SPELARE TAR SKADA");
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
        /*
         * END OF RAYCAST, MARTINS CODE
         */
    }

    // private RaycastHit CheckAttackRange()
    // {
    //     return null;
    // }

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
                if (Vector3.Distance(transform.position, respawnPosWithoutY) >= maxMoveDistans)
                {
                    movingDirection = -movingDirection;
                }

                rigidBody.velocity = movingDirection * moveSpeed * Time.fixedDeltaTime;
            }

            if (isChasing)
            {
                if (Vector3.Distance(transform.position, respawnPosWithoutY) >=
                    maxMoveDistans) //if enemy chasing too far, back to the position before chasing
                {
                    isChasing = false;
                    backToDefault = true;
                }
                else
                {
                    Vector3 facePlayer = new Vector3(chasingObject.transform.position.x, transform.position.y,
                        chasingObject.transform.position.z);
                    transform.LookAt(facePlayer);
                    transform.position = Vector3.MoveTowards(transform.position, facePlayer,
                        chasingSpeed * Time.fixedDeltaTime);
                    // If the enemy is close enough to the player to swing attack (PLACEHOLDER CODE) it will damage the player
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
                    chasingSpeed * 1.5f * Time.deltaTime);
            }
        }
    }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectScopeRadius);
    }
}