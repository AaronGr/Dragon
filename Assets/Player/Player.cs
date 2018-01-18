using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour, IDamagable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float meleeDamage = 10f;
    [SerializeField] float minTimeBetweenHit = .5f;
    [SerializeField] float maxAttackRange = 1f;

    private GameObject currentTarget;
    private float lastHitTime = 0;
    private float currentHealthPoints;
    private AICharacterControl aiCharacterControl = null;
    public GameObject walkTarget = null;
    private CameraRaycaster cameraRaycaster = null;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;

        cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = GameObject.Find("Walk Target");
        aiCharacterControl.SetTarget(walkTarget.transform);
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    private void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {

        switch(layerHit)
        {
            case (int)CameraRaycaster.Layers.EnemyLayer:
                currentTarget = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(currentTarget.transform);

                if((currentTarget.transform.position - transform.position).magnitude > maxAttackRange) {
                    return;
                }

                if(Time.time - lastHitTime > minTimeBetweenHit)
                {

                    lastHitTime = Time.time;
                    DoDamage();
                }
                break;

            case (int)CameraRaycaster.Layers.WalkableLayer:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
        }
    }

    private void DoDamage()
    {
        Component damagableComponent = currentTarget.GetComponent(typeof(IDamagable));
        if (damagableComponent)
        {
            print("Doing " + meleeDamage + " to " + currentTarget);
            (damagableComponent as IDamagable).TakeDamage(meleeDamage);
        }
    }
}
