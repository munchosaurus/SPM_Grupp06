using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float range;  // The range of the enemy attacks
        [SerializeField] private float attackCooldown; // the cooldown of the enemy attacks
        [SerializeField] private int damage; // the damage of the enemy attacks
        [SerializeField] private float cooldown; // float that will be reset to 0 after hitting the attackCooldown variable
        [SerializeField] private CharacterBase characterBase; // the scriptable object that we fetch all the variables from
        [SerializeField] private GameObject player;
        [SerializeField] private GlobalPlayerInfo globalPlayerInfo;

        void Start()
        {
            range = characterBase.GetRange();
            attackCooldown = characterBase.GetAttackCooldown();
            damage = characterBase.GetDamage();
        }
        
        private void FixedUpdate()
        {
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
            
                // If in range and if cooldown has been passed and if the object that the raycast connects with has the tag Player.
                if (hit.distance < range && cooldown > attackCooldown && hit.collider.CompareTag("Player")) 
                {
                    player = hit.collider.gameObject; // updates which player object to attack and to 
                    globalPlayerInfo = player.GetComponent<GlobalPlayerInfo>();
                    ResetCoolDown(); // resets cooldown of the attack
                    Attack(); // Attacks player
                }
            }
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
    }
}