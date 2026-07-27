using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        [SerializeField] AmmoSlot[] ammoSlots;
        Gun currentGun;
        GunSO currentGunSO;
        float timeSinceLastFire = Mathf.Infinity;
        Dictionary<AmmoType, int> ammoLookup;

        public event Action OnGunEquipped;
        public event Action OnAmmoAdjusted;

        public GunSO GetCurrentGunSO()
        {
            return currentGunSO;
        }

        public int GetAmmo(AmmoType ammoType)
        {
            return ammoLookup[ammoType];
        }

        public void AdjustAmmo(AmmoType ammoType, int ammoAmount)
        {
            ammoLookup[ammoType] += ammoAmount;
            OnAmmoAdjusted?.Invoke();
        }

        public void EquipGun(GunSO newGunSO)
        {
            if (currentGun != null)
            {
                Destroy(currentGun.gameObject);
            }

            currentGunSO = newGunSO;
            currentGun = newGunSO.Spawn(gunContainer);
            OnGunEquipped?.Invoke();
        }

        public void Fire()
        {
            if (timeSinceLastFire < currentGunSO.GetCooldown())
            {
                return;
            }

            AmmoType currentAmmoType = currentGunSO.GetAmmoType();

            if (GetAmmo(currentAmmoType) <= 0)
            {
                return;
            }

            currentGun.Fire(currentGunSO.GetDamage(), currentGunSO.GetRange());
            timeSinceLastFire = 0f;
            AdjustAmmo(currentAmmoType, -1);
        }

        [System.Serializable]
        struct AmmoSlot
        {
            public AmmoType ammoType;
            public int ammoAmount;
        }

        void Awake()
        {
            CreateAmmoLookup();
            EquipGun(defaultGunSO);
        }

        void Update()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        void CreateAmmoLookup()
        {
            ammoLookup = new Dictionary<AmmoType, int>();

            foreach (AmmoSlot slot in ammoSlots)
            {
                ammoLookup[slot.ammoType] = slot.ammoAmount;
            }
        }
    }
}
