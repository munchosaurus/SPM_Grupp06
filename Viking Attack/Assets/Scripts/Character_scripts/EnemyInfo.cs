using System.Runtime.CompilerServices;
using UnityEngine;

// WHO TO BLAME: Martin Kings

namespace DefaultNamespace
{
    public class EnemyInfo : MonoBehaviour
    {
        public void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}