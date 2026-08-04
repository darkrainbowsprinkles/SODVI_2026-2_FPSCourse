using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        [SerializeField] AmmoSlot[] ammoSlots;
        [SerializeField] CinemachineCamera firstPersonCamera;
        [SerializeField] Camera gunCamera;
        Gun currentGun;
        GunSO currentGunSO;
        float timeSinceLastFire = Mathf.Infinity;
        float defaultFieldOfView;
        bool isZooming;
        Dictionary<AmmoType, int> ammoLookup;

        public event Action OnGunEquipped;
        public event Action OnAmmoAdjusted;

        public GunSO GetCurrentGunSO()
        {
            return currentGunSO;
        }

        public bool IsZooming()
        {
            return isZooming;
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

        public void ToggleZoom(bool isZooming)
        {
            this.isZooming = isZooming;

            if (isZooming && currentGunSO.CanZoom())
            {
                firstPersonCamera.Lens.FieldOfView = currentGunSO.GetZoomFOV();
                gunCamera.fieldOfView = currentGunSO.GetZoomFOV();
            }
            else
            {
                firstPersonCamera.Lens.FieldOfView = defaultFieldOfView;
                gunCamera.fieldOfView = defaultFieldOfView;
            }
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
            defaultFieldOfView = firstPersonCamera.Lens.FieldOfView;
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
