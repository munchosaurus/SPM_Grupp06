using System.Runtime.CompilerServices;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyInfo : MonoBehaviour
    {
        [SerializeField] private CharacterBase characterBase;

        public void Kill()
        {
            Destroy(gameObject);
        }
    }

    
}