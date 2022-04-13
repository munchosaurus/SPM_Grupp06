using DefaultNamespace;
using UnityEngine;


// Who to blame: Martin Kings

// Container for all player specifics, will add experience gained, HP, level, items owned etc...
public class GlobalPlayerInfo : MonoBehaviour
{
    public static string name;
    public static Color skinColor;
    private static int health;
    public ItemBase[] items;

    // Gets called upon during game launch, the main menu sets the player name
    public static void SetPlayerName(string insertedName)
    {
        name = insertedName;
    }

    // Gets called upon during game launch, the main menu sets the player skin color
    public static void SetSkinColor(Color insertedColor)
    {
        skinColor = insertedColor;
    }

    // Returns the player name
    public static string GetName()
    {
        return name;
    }

    // Returns the player skin color
    public static Color GetSkinColor()
    {
        return skinColor;
    }

}