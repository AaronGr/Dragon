using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float chaseRadius = 10f;

    private float currentHealthPoints = 100f;
    private ThirdPersonCharacter thirdPersonCharacter = null;
    private AICharacterControl aiCharacterControl = null;
    private GameObject player = null;
    private Vector3 startingPosition;

    private void Start()
    {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {

        Vector3 playerPosition = player.transform.position;
        Vector3 distanceFromPlayer = transform.position - playerPosition;

        if (aiCharacterControl &&
           Mathf.Abs(distanceFromPlayer.x) <= attackRadius &&
           Mathf.Abs(distanceFromPlayer.y) <= attackRadius &&
           Mathf.Abs(distanceFromPlayer.z) <= attackRadius)
        {
            print(gameObject.name + " attacking player");
        }
 

        if (aiCharacterControl &&
           Mathf.Abs(distanceFromPlayer.x) <= chaseRadius &&
           Mathf.Abs(distanceFromPlayer.y) <= chaseRadius &&
           Mathf.Abs(distanceFromPlayer.z) <= chaseRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else if (thirdPersonCharacter && aiCharacterControl)
        {
            aiCharacterControl.SetTarget(null);
            aiCharacterControl.agent.SetDestination(startingPosition);
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
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
