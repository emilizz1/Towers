using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] float attackRadius = 4f;
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float secondsBetweenShots = 0.5f;
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 0, 0);

    [SerializeField] Component currentTarget = null;
    bool isAttacking = false;
    CapsuleCollider collider;
    List<Component> targets = new List<Component>();

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        collider.radius = attackRadius;
    }

    //TODO change to 100% hit rate

    void Update()
    {
        if (currentTarget)
        {
            float distanceToEnemy = Vector3.Distance(currentTarget.transform.position, transform.position);
            if (distanceToEnemy <= attackRadius && !isAttacking)
            {
                isAttacking = true;
                InvokeRepeating("FireProjectile", 0f, secondsBetweenShots); // TODO switch to coroutines
            }

            if (distanceToEnemy > attackRadius)
            {
                isAttacking = false;
                CancelInvoke();
            }
        }
        else
        {
            CheckForNewTarget();
        }
    }

    void CheckForNewTarget()
    {
        if (targets != null)
            foreach (Component target in targets)
            {
                if (target != null)
                {
                    currentTarget = target;
                }
            }
    }

    // TODO seperate this class
    void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetDamage(damagePerShot);
        projectileComponent.SetShooter(gameObject);

        Vector3 unitVectorToPlayer = (currentTarget.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.LaunchSpeed();
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
    }

    void OnDrawGizmos()
    {
        // Draw attack sphere 
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        Component enemy = other.gameObject.GetComponent(typeof(Enemy));
        if (enemy)
        {
            targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Component enemy = other.gameObject.GetComponent(typeof(Enemy));
        if(currentTarget == enemy)
        {
            currentTarget = null;
        }
        targets.Remove(enemy);
    }
}
