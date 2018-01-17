﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float damageCaused = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        
        Component damagableComponent = collider.gameObject.GetComponent(typeof(IDamagable));
        print("Damagable component: " + damagableComponent);
        if(damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
    }
}
