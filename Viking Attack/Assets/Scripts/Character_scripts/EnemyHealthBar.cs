using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

// This script will make a GUITexture follow a transform.
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] Slider healthBar; // the slider 
    [SerializeField] private GameObject healthSource;
    
    private void Start()
    {
        //healthBar = GetComponent<Slider>();
        //healthBar.value = gameObject.GetComponent<EnemyMovement>().GetHealth();
        //healthBar.value = gameObject.transform.parent.gameObject.transform.parent.GetComponent<EnemyMovement>()
        //    .GetHealth();
        healthBar.value = healthSource.GetComponent<EnemyMovement>().GetHealth();
        healthBar.maxValue = healthSource.GetComponent<EnemyMovement>().GetMaxHealth();
    }

    public void SetHealth()
    {
        healthBar.value = healthSource.GetComponent<EnemyMovement>().GetHealth();
    }
    
    
    public void Display()
    {
        var wantedPos = Camera.main.WorldToScreenPoint (target.position);
        gameObject.transform.position = wantedPos;
    }

    public void Activate()
    {
        Debug.Log("aktiveras");
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
    
    public void Deactivate()
    {
        Debug.Log("deaktiveras");
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}


