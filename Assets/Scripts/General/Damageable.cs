using UnityEngine;

namespace General
{
    public class Damageable : MonoBehaviour
    {
        [Header("Attributes")] [SerializeField]
        protected float MaxHealth;

        public void TakeDamage(float damage)
        {
            MaxHealth -= damage;
            if (MaxHealth <= 0) Destroy(gameObject);
        }
    }
}