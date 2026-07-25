using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        Gun currrentGun;

        public void Fire()
        {
            currrentGun.Fire(defaultGunSO.GetDamage(), defaultGunSO.GetRange());
        }

        void Awake()
        {
            currrentGun = defaultGunSO.Spawn(gunContainer);
        }
    }
}
