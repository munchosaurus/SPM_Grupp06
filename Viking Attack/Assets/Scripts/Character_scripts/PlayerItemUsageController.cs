using DefaultNamespace;
using UnityEngine;

public class PlayerItemUsageController : MonoBehaviour
{
    [SerializeField] private ItemBase itemBase; // Will need to be updated if another item is being used.

    // WHO TO BLAME: Martin Kings
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if the left mouse button is clicked
        {
            if (itemBase.GetType() == ItemBase.Type.Food) // checks to see if the itembase is food in the playerhand
            {
                // eat, to be implemented
            }
            else if (itemBase.GetType() == ItemBase.Type.Weapon) // checks to see if the itembase is a weapon in the playerhand
            {
                RaycastHit hit;
                if (Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position, 1f,
                        gameObject.transform.Find("Main Camera").transform.forward, out hit, itemBase.GetRange(),
                        LayerMask.GetMask("Enemy")))
                {
                    hit.collider.gameObject.GetComponent<EnemyMovement>().UpdateHealth(-itemBase.GetDamage());
                }
            }
            else if (itemBase.GetType() == ItemBase.Type.Tool) // checks to see if the itembase is a tool in the playerhand
            {
                // do tool stuff, to be implemented
            }
            else // ff the itembase is key in the playerhand
            {
                // do key stuff, to be implemented
            }
        }
    }
}