
using DefaultNamespace;
using UnityEngine;


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

    private void Awake()
    {
        
        health = 100;
        maxHealth = 100;
        //Cursor.lockState = CursorLockMode.Locked; // Locks the mouse cursor
    }

    // Gets called upon during game launch, the main menu sets the player name
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
        gameObject.transform.Find("UI").gameObject.transform.Find("Health_bar").gameObject.transform.Find("Health_bar_slider").gameObject.GetComponent<PlayerHealthBar>().SetHealth();
    }

}