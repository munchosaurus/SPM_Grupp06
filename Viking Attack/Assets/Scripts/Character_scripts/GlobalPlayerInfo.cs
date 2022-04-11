using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerInfo : MonoBehaviour
{
    public static string name;
    public static Color skinColor;

    public void SetPlayerName(string insertedName)
    {
        name = insertedName;
    }
    
    public void SetSkinColor(Color insertedColor)
    {
        skinColor = insertedColor;
    }
    
}
