using System;
using UnityEngine;
using UnityEngine.UI;


// Who to blame: Martin Kings


public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar; // the slider 

    private void Start()
    {
        
        healthBar = GameObject.FindGameObjectWithTag("health_bar").gameObject.GetComponent<Slider>();
        healthBar.maxValue = gameObject.GetComponent<GlobalPlayerInfo>().GetMaxHealth();
        healthBar.value = gameObject.GetComponent<GlobalPlayerInfo>().GetHealth();
    }

    // Updates the value of the slider to the players current health (will be called upon when being attacked, healed etc)
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}