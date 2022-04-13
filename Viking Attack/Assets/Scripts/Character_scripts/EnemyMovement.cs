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


    [Header("GroudCheck Settings")] [SerializeField]
    private GameObject groundCheck;

    private bool isGrounded;
    private LayerMask ground;
    private Collider[] colliders;


    [Header("Patrol Settnings")] [SerializeField]
    private float detectScopeRadius;
    private Vector3 nevMovePosition; //uppdate when isGuarding 
    //private LayerMask skogMask;
    [SerializeField] private float maxDetectDis;
    [SerializeField] private Collider collider;

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
        ground = LayerMask.GetMask("Ground");
        //skogMask = LayerMask.GetMask("Skog");
        var position = transform.position; // Enemy starting position 
        respawnPosWithoutY = new Vector3(position.x, position.y, position.z);
        position = respawnPosWithoutY;
        transform.position = position;
        nevMovePosition = RandomVector(-maxMoveDistans,maxMoveDistans,transform.position);
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
           // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
              //  Color.yellow);
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
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
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

        if (isGrounded) 
        {
            if (isGuarding) //start patrolling 
            {
                //if (Vector3.Distance(transform.position, respawnPosWithoutY) >= maxMoveDistans) // if too far from start 
                //{
                //    movingDirection = -movingDirection;
                //}

                //rigidBody.velocity = movingDirection * moveSpeed * Time.fixedDeltaTime;
      
                if (Vector3.Distance(transform.position, respawnPosWithoutY) <=   maxMoveDistans)
                {
                  
                        transform.position += nevMovePosition.normalized * moveSpeed * 0.1f * Time.deltaTime;
                    
                   
                }
                else //when enemy move too far 
                {
                    
                    nevMovePosition =  RandomVector(-maxMoveDistans, maxMoveDistans, new Vector3(transform.position.x,0f,transform.position.y));
                    transform.position += nevMovePosition.normalized * moveSpeed * 0.1f * Time.deltaTime;
                }

                
              

            }

            if (isChasing)
            {
                if (Vector3.Distance(transform.position, respawnPosWithoutY) >=
                    maxMoveDistans) //if enemy chasing too far, back to the position before chasing (see if(backToDefault))
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
                nevMovePosition = RandomVector(-maxMoveDistans, maxMoveDistans, transform.position);
                isGuarding = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, respawnPosWithoutY,
                    chasingSpeed * 3.5f * Time.deltaTime);
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
    private Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 resultVec3 = center + point;
        return resultVec3;
    }
    private Vector3 RandomVector(float min, float max, Vector3 currentPosition) //version1 lock enemy's y-axel
    {
        return new Vector3(UnityEngine.Random.Range(min, max), currentPosition.y, UnityEngine.Random.Range(min, max));
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectScopeRadius);
    }
}