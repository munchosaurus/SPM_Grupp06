using UnityEngine;
using UnityEngine.UI;


// Who to blame: Martin Kings

// Will 
public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar; // the slider 
    private GlobalPlayerInfo globalPlayerInfo;
    public float health;    // the health of the player
    [SerializeField] private GameObject player;
    
    private void Start()
    {
        globalPlayerInfo = player.GetComponent<GlobalPlayerInfo>();
        healthBar = GetComponent<Slider>();
        healthBar.value = globalPlayerInfo.GetHealth();
    }

    public void SetHealth()
    {
        healthBar.value = globalPlayerInfo.health;
    }
}