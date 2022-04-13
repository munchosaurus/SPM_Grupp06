using UnityEngine;
using UnityEngine.UI;


// Who to blame: Martin Kings

// Will 
public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar; // the slider 
    private GlobalPlayerInfo globalPlayerInfo;
    public static float health;    // the health of the player
    [SerializeField] private GameObject player;
    
    private void Start()
    {
        //health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        globalPlayerInfo = player.GetComponent<GlobalPlayerInfo>();
        
        healthBar = GetComponent<Slider>();
        
        //healthBar.maxValue = ;
        healthBar.value = health;
    }

    private void Update()
    {
        
        SetHealth();
    }

    public void SetHealth()
    {
        healthBar.value = globalPlayerInfo.health;
    }
}