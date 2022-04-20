using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

// WHO TO BLAME: Martin Kings

// This script will make a GUITexture follow a transform (object placed above the enemy).
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] public Transform target; // the gameobject.transform that the UI should follow 
    [SerializeField] public Slider healthBar; // the slider 
    [SerializeField] private GameObject healthSource; // the enemy gameobject


    public EnemyHealthBar(Transform t, GameObject hs)
    {
        target = t;
        healthSource = hs;
    }
    private void Update()
    {
        //SetHealth();
        Display(); // Runs the display method that places the Ui element in the correct place above the enemy. Will only run if active.
    }
    
    // Sets the health to the desired amount
    private void Start()
    {
        healthBar = gameObject.GetComponent<Slider>();
    }

    public void SetHealthSource(GameObject hs)
    {
        healthSource = hs;
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


