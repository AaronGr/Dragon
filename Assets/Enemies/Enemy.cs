using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float playerDetectionRadius = 5f;

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
           Mathf.Abs(distanceFromPlayer.x) <= playerDetectionRadius &&
           Mathf.Abs(distanceFromPlayer.y) <= playerDetectionRadius &&
           Mathf.Abs(distanceFromPlayer.z) <= playerDetectionRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else if(thirdPersonCharacter && aiCharacterControl)
        {
            aiCharacterControl.SetTarget(null);
            aiCharacterControl.agent.SetDestination(startingPosition);
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

}
