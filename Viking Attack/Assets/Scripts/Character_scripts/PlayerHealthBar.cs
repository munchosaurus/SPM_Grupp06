
using UnityEngine;
using UnityEngine.UI;


// Who to blame: Martin Kings

// Will 
public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar; // the slider 
    public Health health;    // the health of the player
    
    
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.maxHealth;
        healthBar.value = health.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}