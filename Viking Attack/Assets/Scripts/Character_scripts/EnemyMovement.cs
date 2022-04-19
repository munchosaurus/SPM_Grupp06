using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int patrolRange;
    private Vector3 respawnPosWithoutY;
    private Rigidbody rigidBody;
    private Vector3 movingDirection;
    [Header("GroudCheck Settings")]
    [SerializeField]
    private GameObject groundCheck;
    private bool isGrounded;
    private LayerMask ground;
    private Collider[] colliders;
    [Header("Patrol Settnings")]
    [SerializeField]
    private float detectScopeRadius;
    [SerializeField] private float maxMoveDistans;
    private bool isGuarding;
    private bool isChasing;
    private bool backToDefault;
    private Vector3 startPos; //save the position when enemy detected player
    private Vector3 nevMovePosition;
    private Collider[] sphereColliders;
    private GameObject chasingObject;
    private CapsuleCollider capsuleCollider;
    private BoxCollider boxCollider;
    private LayerMask skogLayer;
    private LayerMask collisionMask;
    private Vector3 point1;
    private Vector3 point2;
    private bool needChangeDire;

    [Header("MARTIN BELOW")]
    // Martin variables down
    [SerializeField] LayerMask playerMask;
    [SerializeField] private GameObject player;
    [SerializeField]
    private GlobalPlayerInfo globalPlayerInfo;

    // TODO: Fetch enemy information in prefab that determines range, cooldown, damage
    // PLACEHOLDER BELOW
    [SerializeField] private float range;  // The range of the enemy attacks
    [SerializeField] private float attackCooldown; // the cooldown of the enemy attacks
    [SerializeField] private int damage; // the damage of the enemy attacks
    [SerializeField] private float cooldown; // float that will be reset to 0 after hitting the attackCooldown variable
    [SerializeField] private CharacterBase characterBase; // the scriptable object that we fetch all the variables from
    [SerializeField] private float chasingSpeedMultiplier; // the multiplier for the movement speed of the enemy (1 if to move at same pace as the regular movement speed)
    [SerializeField] private int moveSpeed; // movement speed of the enemy
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Awake()
    {

    }
    void Start()
    {
        // START OF MARTIN
        // Updates the variables using the scriptable object

        range = characterBase.GetRange();
        attackCooldown = characterBase.GetAttackCooldown();
        damage = characterBase.GetDamage();
        rigidBody = GetComponent<Rigidbody>();
        chasingSpeedMultiplier = characterBase.GetChasingSpeed();
        moveSpeed = characterBase.GetMovementSpeed();
        health = characterBase.GetMaxHealth();
        maxHealth = characterBase.GetMaxHealth();


        // END OF MARTIN
        boxCollider = GetComponent<BoxCollider>();
        isGuarding = true;
        ground = LayerMask.GetMask("Ground");
        skogLayer = LayerMask.GetMask("Skog");
        movingDirection = Vector3.forward;
        var position = transform.position; // Enemy starting position 
        respawnPosWithoutY = new Vector3(position.x, position.y, position.z);



        nevMovePosition = RandomVector(-maxMoveDistans, maxMoveDistans, transform.position);



    }
    private void FixedUpdate()
    {
        /*
        * START OF RAYCAST, MARTINS CODE
         * handles raycasting towards player and checks if a hit is performed and will then if cooldown condition is met attack the player with the damage.
        */
        if (cooldown < attackCooldown) // adds to cooldown if attackCooldown hasn't been met
        {
            cooldown += Time.fixedDeltaTime;
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            // Prints a line of the raycast if a player is detected.
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);

            if (hit.distance < range && cooldown > attackCooldown && hit.collider.CompareTag("Player")) // If in range and if cooldown has been passed and if the object that the raycast connects with has the tag Player.
            {
                player = hit.collider.gameObject; // updates which player object to attack and to 
                globalPlayerInfo = player.GetComponent<GlobalPlayerInfo>();
                StartCoroutine("ResetCoolDown");
                StartCoroutine("Attack"); // Attacks player
            }
        }
        /*
         * END OF RAYCAST, MARTINS CODE
         */
    }

    // Resets the attack cooldown
    private void ResetCoolDown()
    {
        cooldown = 0;
    }

    // Attacks with the damage of the object.
    private void Attack()
    {
        if (globalPlayerInfo.IsAlive()) // checks if the player is even alive
        {
            // Tests if the correct player is attacked.
            globalPlayerInfo.UpdateHealth(-damage);
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
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


                if (Vector3.Distance(transform.position, respawnPosWithoutY) >= maxMoveDistans)
                {
                    Vector3 test = transform.position + nevMovePosition;
                    Debug.DrawLine(transform.position, test, Color.green);
                    RaycastHit hit;
                    //Physics.CapsuleCast(point1, point2, capsuleCollider.radius, test, out hit, 1f, playerMask)
                    //if (Physics.CapsuleCast(point1, point2, capsuleCollider.radius, test,  1f, collisionMask))
                    nevMovePosition = RandomVector(-maxMoveDistans, maxMoveDistans, new Vector3(transform.position.x, 0.5f, transform.position.y));
                    if (Physics.BoxCast(boxCollider.bounds.center, transform.localScale, nevMovePosition.normalized, out hit, transform.rotation, 0.5f, skogLayer))
                    {

                        Debug.Log("Find skog");
                        Vector3 temp = new Vector3(nevMovePosition.x, nevMovePosition.y, nevMovePosition.z + 5);
                        Debug.DrawRay(transform.position, temp, Color.blue);
                        nevMovePosition = temp;
                    }


                    transform.position += nevMovePosition * moveSpeed * 0.01f * Time.deltaTime;
                }
                else
                {
                    Vector3 test = transform.position + nevMovePosition;
                    Debug.DrawLine(transform.position, test, Color.green);
                    RaycastHit hit;
                    //if (Physics.CapsuleCast(point1,point2,capsuleCollider.radius,test,1f, 9))
                    if (Physics.BoxCast(boxCollider.bounds.center, transform.localScale, nevMovePosition.normalized, out hit, transform.rotation, 0.5f, skogLayer))
                    {
                        Debug.Log("Find skog");

                        Vector3 temp = new Vector3(nevMovePosition.x, nevMovePosition.y, nevMovePosition.z + 5);
                        nevMovePosition = temp;
                        Debug.DrawRay(transform.position, temp, Color.blue);
                    }

                    transform.position += nevMovePosition * moveSpeed * 0.01f * Time.deltaTime;
                }
                //float angle = Vector3.Angle(Vector3.forward, nevMovePosition);






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
                    chasingSpeedMultiplier * Time.deltaTime);
            }
        }
    }
    private Vector3 RandomVector(float min, float max, Vector3 currentPosition) //version1 lock enemy's y-axel
    {
        return new Vector3(UnityEngine.Random.Range(min, max), currentPosition.y, UnityEngine.Random.Range(min, max));
    }


    private void CheckForPlayer()
    {
        sphereColliders = Physics.OverlapSphere(transform.position, detectScopeRadius);
        foreach (var coll in sphereColliders)
        {
            if (coll.tag == "Player") //find Player and start chasing
            {
                //posBeforeChasing = transform.position;
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
        Gizmos.DrawWireCube(transform.position, new Vector3());
    }

    public void UpdateHealth(float difference)
    {

        health += difference;
        gameObject.transform.Find("Parent").gameObject.transform.Find("Health_bar").gameObject.GetComponent<EnemyHealthBar>().SetHealth();
        if (health <= 0)
        {
            gameObject.GetComponent<EnemyInfo>().Kill();
        }
    }

    public float GetHealth()
    {
        return health;
    }
}