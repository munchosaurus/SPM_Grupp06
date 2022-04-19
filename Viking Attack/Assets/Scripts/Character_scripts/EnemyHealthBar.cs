using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

// WHO TO BLAME: Martin Kings

// This script will make a GUITexture follow a transform (object placed above the enemy).
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform target; // the gameobject.transform that the UI should follow 
    [SerializeField] Slider healthBar; // the slider 
    [SerializeField] private GameObject healthSource; // the enemy gameobject

    private void Update()
    {
        Display(); // Runs the display method that places the Ui element in the correct place above the enemy. Will only run if active.
        
    }
    
    // Sets the health to the desired amount
    private void Start()
    {
        SetHealth();
        //healthBar.value = healthSource.GetComponent<EnemyMovement>().GetHealth();
        healthBar.maxValue = healthSource.GetComponent<EnemyMovement>().GetMaxHealth();
    }
    

    // Updates the health number of the slider
    public void SetHealth()
    {
        healthBar.value = healthSource.GetComponent<EnemyMovement>().GetHealth();
    }
    
    public void Display()
    {
        var wantedPos = Camera.main.WorldToScreenPoint (target.position);
        gameObject.transform.position = wantedPos;
    }
}


