using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;

    private float currentHealthPoints = 100f;
    private AICharacterControl aiCharacterControl = null;
    public GameObject walkTarget = null;
    private CameraRaycaster cameraRaycaster = null;

    private void Start()
    {
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

    private void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        print("Raycast hit:  " + raycastHit + "\n" +
              "Layer hit:  " + layerHit);

        switch(layerHit)
        {
            case (int)CameraRaycaster.Layers.EnemyLayer:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(raycastHit.transform);
                break;

            case (int)CameraRaycaster.Layers.WalkableLayer:
                walkTarget.transform.position = raycastHit.point;
                //walkTarget.transform.Translate(0, -walkTarget.transform.position.y, 0);
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
        }
    }
}
