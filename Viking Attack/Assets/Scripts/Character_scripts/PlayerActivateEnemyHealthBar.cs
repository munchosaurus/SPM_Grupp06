using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        private GameObject targetHealthBar;

        void Update()
        {
            //Sends a raycast to check for colliders in the Enemy layer
            RaycastHit hit;

            if (Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position, 2f,
                    gameObject.transform.Find("Main Camera").transform.forward, out hit, 15, LayerMask.GetMask("Enemy")))
            {
                targetHealthBar = hit.collider.gameObject;
                targetHealthBar.transform.Find("Parent").gameObject.SetActive(true);
                GameObject child = targetHealthBar.transform.Find("Parent").gameObject.transform.Find("Health_bar").gameObject;
                child.GetComponent<EnemyHealthBar>().Display();
            }
            
        }
    }
}