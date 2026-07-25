using UnityEngine;

namespace FPS.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 200f;
        float currentHealth;

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0f);
            print($"{name} - Current health = {currentHealth}");
        }

        void Awake()
        {
            currentHealth = maxHealth;
        }
    }
}