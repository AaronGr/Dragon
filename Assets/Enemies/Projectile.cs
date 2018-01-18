using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileSpeed = 10f;

    public float damageCaused
    {
        get;
        set;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        
        Component damagableComponent = collider.gameObject.GetComponent(typeof(IDamagable));
        if(damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
    }
}
