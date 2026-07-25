using UnityEngine;

namespace FPS.Combat
{
    [CreateAssetMenu(menuName = "FPS/New Gun")]
    public class GunSO : ScriptableObject
    {
        [SerializeField] Gun gunPrefab;
        [SerializeField] float damage = 30f;
        [SerializeField] float range = 40f;

        public Gun Spawn(Transform container)
        {
            return Instantiate(gunPrefab, container);
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }
    }
}
