using FPS.Movement;
using UnityEngine;

namespace FPS.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField, Range(0,1)] float chaseSpeedFraction = 0.5f;
        [SerializeField] float chaseRange = 10f;
        [SerializeField] float attackRange = 2f;
        GameObject player;
        Mover mover;
        Animator animator;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer < attackRange)
            {
                mover.Stop();
                animator.SetTrigger("attack");
                mover.LookAt(player);
            }
            else if (distanceToPlayer < chaseRange)
            {
                animator.ResetTrigger("attack");
                mover.MoveTo(player.transform.position, chaseSpeedFraction);
            }
            else
            {
                mover.Stop();
            }
        }

        // Called in Unity Events
        void Hit()
        {
            print("HIT!!!");
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.softRed;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}