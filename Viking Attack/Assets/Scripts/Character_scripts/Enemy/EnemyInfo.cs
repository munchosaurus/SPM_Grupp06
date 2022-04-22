using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// WHO TO BLAME: Martin Kings

namespace DefaultNamespace
{
    public class EnemyInfo : MonoBehaviour
    {
        [SerializeField] private float range;  // The range of the enemy attacks
        [SerializeField] private float attackCooldown; // the cooldown of the enemy attacks
        [SerializeField] private int damage; // the damage of the enemy attacks
        [SerializeField] private float cooldown; // float that will be reset to 0 after hitting the attackCooldown variable
        [SerializeField] private CharacterBase characterBase; // the scriptable object that we fetch all the variables from
        [SerializeField] private float chasingSpeedMultiplier; // the multiplier for the movement speed of the enemy (1 if to move at same pace as the regular movement speed)
        [SerializeField] private int moveSpeed; // movement speed of the enemy
        [SerializeField] private float health;
        [SerializeField] public float maxHealth;
        private bool hasHealthBarShown;

        private void Awake()
        {
            // START OF MARTIN
            // Updates the variables using the scriptable object
        
            range = characterBase.GetRange();
            attackCooldown = characterBase.GetAttackCooldown();
            damage = characterBase.GetDamage();
            chasingSpeedMultiplier = characterBase.GetChasingSpeed();
            moveSpeed = characterBase.GetMovementSpeed();
            health = characterBase.GetMaxHealth();
            maxHealth = characterBase.GetMaxHealth();
        }

        public void Kill()
        {
            //TODO ADD EVENT LISTENER HERE, NEEDS TO FIND ALL LISTENERS FOR ENEMY DEATHS
            gameObject.SetActive(false);
        }

        public bool CheckHealthBarStatus()
        {
            return hasHealthBarShown;
        }

        public void SetHealthBarStatus(bool b)
        {
            hasHealthBarShown = b;
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
    }
}