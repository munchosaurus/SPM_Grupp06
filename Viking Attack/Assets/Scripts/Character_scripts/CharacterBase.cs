using UnityEngine;
namespace DefaultNamespace
{
    // Creates the ScriptableObject function for the Item objects.
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Create new character")]
    
    // Contains the base information for all characters.
    public class CharacterBase : ScriptableObject
    {
        [SerializeField] private Type type;
        [SerializeField] private string name;
        [SerializeField] private string description;

        // Returns name of the item
        public string GetName()
        {
            return name;
        }

        // Returns the description of the item
        public string GetDescription()
        {
            return description;
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