using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        Gun currentGun;
        GunSO currentGunSO;
        float timeSinceLastFire = Mathf.Infinity;

        public GunSO GetCurrentGunSO()
        {
            return currentGunSO;
        }

        public void Fire()
        {
            if (timeSinceLastFire < currentGunSO.GetCooldown())
            {
                return;
            }

            currentGun.Fire(currentGunSO.GetDamage(), currentGunSO.GetRange());
            timeSinceLastFire = 0f;
        }

        void Awake()
        {
            EquipGun(defaultGunSO);
        }

        void Update()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        void EquipGun(GunSO newGunSO)
        {
            currentGunSO = newGunSO;
            currentGun = newGunSO.Spawn(gunContainer);
        }
    }
}
