using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] Gun gunPrefab;
        [SerializeField] Transform gunContainer;

        public void Fire()
        {
            gunPrefab.Fire();
        }

        void Awake()
        {
            Instantiate(gunPrefab, gunContainer);
        }
    }
}
