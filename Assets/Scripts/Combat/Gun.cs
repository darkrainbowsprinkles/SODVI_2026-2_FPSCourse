using FPS.Core;
using UnityEngine;

namespace FPS.Combat
{
    public class Gun : MonoBehaviour
    {
        public void Fire()
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;

            if (Physics.Raycast(cameraPosition, cameraForward, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.TryGetComponent(out Health health))
                {
                    health.TakeDamage(50);
                }
            }
        }
    }
}