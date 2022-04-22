using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(EnemyVitalController))]
public class EnemyHealthBarController : MonoBehaviour
{
    //[SerializeField]Image bar = null;
    //[SerializeField]Text bartext = null;

    public void SetVitalController(EnemyVitalController controller) => controller.OnHealthChanged += OnHealthChanged;

    void OnHealthChanged(float valueInPercent)
    {
        //bar.fillAmount = valueInPercent;
        //bartext.text = $"{value}";
    }
}