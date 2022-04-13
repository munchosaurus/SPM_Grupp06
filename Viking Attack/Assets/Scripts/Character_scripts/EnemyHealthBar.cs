using UnityEngine;

// This script will make a GUITexture follow a transform.
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform target;
    public void Display()
    {
        var wantedPos = Camera.main.WorldToScreenPoint (target.position);
        gameObject.transform.position = wantedPos;
    }
}


