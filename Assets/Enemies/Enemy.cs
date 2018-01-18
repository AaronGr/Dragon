using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float chaseRadius = 10f;
    [SerializeField] float damagePerShot = 10f;
    [SerializeField] float secondsBetweenShots = 0.5f;  //TODO switch to coroutines
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;
    

    public bool isAttacking = false;

    private float currentHealthPoints;
    private ThirdPersonCharacter thirdPersonCharacter = null;
    private AICharacterControl aiCharacterControl = null;
    private GameObject player = null;
    private Vector3 startingPosition; 
   
    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if(currentHealthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /*
    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }
    */

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;

        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius && !isAttacking) 
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots);
        }

        if (attackRadius < distanceToPlayer)
        {
            isAttacking = false;
            CancelInvoke();
        }

        if (distanceToPlayer <= chaseRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(null);
            aiCharacterControl.agent.SetDestination(startingPosition);

        }

    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.damageCaused = damagePerShot;

        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
    }

    private void OnDrawGizmos()
    {

        //Draw Attack Spheres
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        //Draw Move Spheres
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
