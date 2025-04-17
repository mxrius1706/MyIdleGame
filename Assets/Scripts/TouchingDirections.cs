using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    Rigidbody2D rb;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    Animator animator;

    [SerializeField]
    private bool _isGrounded = true;

    [SerializeField]
    private bool _isOnWall;
    [SerializeField]
    private bool _isOnCeiling;  // Corrected typo

    

    /*
    Der Spieler schaut nach rechts, wenn localScale.x > 0 (also nicht gespiegelt), und nach links, wenn localScale.x < 0 (also gespiegelt).
    wallcheckdirection gibt einen Vektor zurück, der je nach der Blickrichtung des Spielers entweder
    */
    private Vector2 wallcheckdirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {

            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }


    public bool IsOnWall
    {
        get
        {
            return _isOnWall;; 
        }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    { //Wird in festen Zeitintervallen aufgerufen - Ideal für Physik-Berechnungen (z. B. Rigidbody-Bewegungen).

        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallcheckdirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

    }
}
