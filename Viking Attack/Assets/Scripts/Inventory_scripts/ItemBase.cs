using System;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    // Creates the ScriptableObject function for the Item objects.
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Create new item")]
    
    // Contains the base information for all items. Will later on be used to determine action when used.
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private Type type;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private int damage; // only interesting if weapon
        [SerializeField] private int healAmount; // only interesting if food
        [SerializeField] private Sprite sprite; // the icon shown when interacting with the item


        
        // Returns damage output when used
        public int GetDamage()
        {
            return damage;
        }
    
        // Returns name of the item
        public string GetName()
        {
            return name;
        }
        
        // Returns the amount healed when using the item
        public int GetHealAmount()
        {
            return healAmount;
        }

        // Returns the description of the item
        public string GetDescription()
        {
            return description;
        }

        // Contains the different item type, add a new line to the enum in order to add an item type.
        public enum Type
        {
            Food,
            Weapon,
            Tool,
            Key
        }
    }
}