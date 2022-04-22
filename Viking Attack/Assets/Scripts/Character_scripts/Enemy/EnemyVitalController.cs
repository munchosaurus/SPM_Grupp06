using System;
using DefaultNamespace;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(EnemyHealthBarController))]
public class EnemyVitalController : NetworkBehaviour
{
    float maxHealth;
    [SerializeField] private EnemyHealthBarController enemyHealthBarController;
    [SerializeField] private CharacterBase characterBase;
    
    [SerializeField][SyncVar(hook = nameof(OnHealthChangedHook))]float currentHealth = 100f;
//TODO Vi behöver lägga till så nuvarande HP uppdateras. EnemyHealthBarController ska uppdatera alla värden där.
    //spara maxvärdet så vi kan räkna ut procent 
    void Start()
    {
        currentHealth = characterBase.GetMaxHealth();
        maxHealth = currentHealth;

        enemyHealthBarController.SetVitalController(this);
    }

    //körs på alla klienter efter att syncvarens värde ändras
    void OnHealthChangedHook(float old, float @new)
    {
        //invoka vårt event, passa med vårt nya värde (syncvaren är uppdaterad när hooken körs)
        this.OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateHealth(float change) => UpdateHealth(change);

    private void UpdateHealth(float change)
    {
        
        if (base.isServer)
        {
            Debug.Log("I am in" + change);
            //clampa värdet så vi inte kan få mer hp än maxvärdet
            currentHealth = Mathf.Clamp(currentHealth += change, -Mathf.Infinity, maxHealth);

            if(currentHealth <= 0f)
            {
                this.OnDeath?.Invoke(this);
                NetworkServer.Destroy(this.gameObject);
            }
        }
        else
            CmdUpdateHealth(change);
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    //andra script kan registrera på detta event
    public event Action<float> OnHealthChanged;

    //OBS KÖRS ENDAST PÅ SERVERN
    public event Action<EnemyVitalController> OnDeath;
}