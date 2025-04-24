using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;



    void Awake () {

        attackCollider = GetComponent<Collider2D> ();
    }



    private void OnTriggerEnter2D (Collider2D collision) {

        //See if target is valid to be hit
        Damageable damageable  = collision.GetComponent<Damageable> ();

        if (damageable != null) {

            //macht knockback rückwärst wenn gegner weg von uns läuft
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2 (-knockback.x, knockback.y);

            bool gotHit = damageable.hit(attackDamage, deliveredKnockback);
           

        }
    }
}
