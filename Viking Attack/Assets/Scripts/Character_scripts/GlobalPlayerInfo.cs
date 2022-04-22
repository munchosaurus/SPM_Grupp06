using System;
using DefaultNamespace;
using UnityEngine;
using Mirror;

// WHO TO BLAME: Martin Kings

// Container for all player specifics, will add experience gained, HP, level, items owned etc...
public class GlobalPlayerInfo : MonoBehaviour
{
    public string playerName;
    public Color skinColor;
    public float health;
    public float maxHealth;
    public ItemBase[] items;
    public bool alive = true;
    private Canvas playerUI;

    private void Awake()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<Canvas>();
        if (playerUI != null)
        {
            playerUI.transform.SetParent(transform);

        }
        health = 100;
        maxHealth = 100;
        //Cursor.lockState = CursorLockMode.Locked; // Locks the mouse cursor
    }

    public void SetPlayerName(string insertedName)
    {
        playerName = insertedName;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // Gets called upon during game launch, the main menu sets the player skin color
    public void SetSkinColor(Color insertedColor)
    {
        skinColor = insertedColor;
    }

    // Returns the player name
    public string GetName()
    {
        return playerName;
    }

    // Returns the player skin color
    public Color GetSkinColor()
    {
        return skinColor;
    }

    // Checks if the player is alive
    public bool IsAlive()
    {
        return alive;
    }

    // Adds or reduces health
    public void UpdateHealth(float difference)
    {
        health += difference;
        // Updates the slider value
        gameObject.GetComponent<PlayerHealthBar>().SetHealth(health);
    }
}