using UnityEngine;
using UnityEngine.UI;


// Who to blame: Martin Kings


public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar; // the slider 
    private GlobalPlayerInfo globalPlayerInfo; // contains the global info of the current player
    [SerializeField] private GameObject player; // the current player object
    
    private void Start()
    {
        globalPlayerInfo = player.GetComponent<GlobalPlayerInfo>(); // fetches the globalplayerinfo from the player
        healthBar = GetComponent<Slider>();
        healthBar.value = globalPlayerInfo.GetHealth();
    }

    // Updates the value of the slider to the players current health (will be called upon when being attacked, healed etc)
    public void SetHealth()
    {
        healthBar.value = globalPlayerInfo.health;
    }
}