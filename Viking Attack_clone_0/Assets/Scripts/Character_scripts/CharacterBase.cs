using UnityEngine;
namespace DefaultNamespace

// WHO TO BLAME: Martin Kings
{
    // Creates the ScriptableObject function for the Item objects.
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Create new character")]
    
    // Contains the base information for all characters.
    public class CharacterBase : ScriptableObject
    {
        [SerializeField] private Type type;
        [SerializeField] private string characterName;
        [SerializeField] private string description;
        [SerializeField] private float range;
        [SerializeField] private float attackCooldown;
        [SerializeField] private int damage;
        [SerializeField] private float chasingSpeedMultiplier;
        [SerializeField] private int moveSpeed;
        [SerializeField] private float maxHealth;

        // Returns name of the item
        public string GetName()
        {
            return characterName;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        // Returns the description of the item
        public string GetDescription()
        {
            return description;
        }

        // Returns the range of the character
        public float GetRange()
        {
            return range;
        }

        // Returns the attack cooldown of the character
        public float GetAttackCooldown()
        {
            return attackCooldown;
        }

        
        // Returns the base damage of the character
        public int GetDamage()
        {
            return damage;
        }

        public float GetChasingSpeed()
        {
            return chasingSpeedMultiplier;
        }

        public int GetMovementSpeed()
        {
            return moveSpeed;
        }

        // Contains the different item type, add a new line to the enum in order to add an item type.
        public enum Type
        {
            Player,
            Enemy,
            Friendly
            
        }
    }
}