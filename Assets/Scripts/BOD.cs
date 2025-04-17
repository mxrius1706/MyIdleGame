using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class BOD : MonoBehaviour

{
    public DetectionZone attackZone;
    public float walkSpeed = 3f;
    public float WalkStopRate = 0.6f;
    Rigidbody2D rb;

    Animator animator;

    public enum WalkableDirection { right, left };

    private WalkableDirection _walkDirection;

    TouchingDirections touchingDirections;

    public Vector2 WalkDirectionVector = Vector2.left;

    Damageable damageable;




    public WalkableDirection WalkDirection
    {

        get
        {

            return _walkDirection;

        }

        set
        {
            if (_walkDirection != value)
            {

                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.right)
                {

                    WalkDirectionVector = Vector2.right;
                }

                else if (value == WalkableDirection.left)
                {

                    WalkDirectionVector = Vector2.left;
                }


            }
            _walkDirection = value;
        }

    }
    public bool _hasTargets = false;
    public bool hasTargets
    {

        get
        {

            return _hasTargets;
        }
        private set
        {

            _hasTargets = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool canMove { get { return animator.GetBool(AnimationStrings.canMove); } }




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {

        if (touchingDirections.IsOnWall && touchingDirections.isGrounded)
        {

            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {

            if (canMove)
            {
                rb.velocity = new Vector2(walkSpeed * WalkDirectionVector.x, rb.velocity.y);
            }
            else
            {

                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, WalkStopRate), rb.velocity.y);
            }
        }

    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.right)
        {

            WalkDirection = WalkableDirection.left;
        }

        else if (WalkDirection == WalkableDirection.left)
        {

            WalkDirection = WalkableDirection.right;
        }

        else
        {
            Debug.LogError("Current Walkable DIrection is not set to a legal value");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackZone.detectedColliders.Count > 0)
        {
            hasTargets = true;
        }

        else
        {
            hasTargets = false;
        }
    }

    public void OnHit (int dmg, Vector2 knockback) {

        rb.velocity = new Vector2 (knockback.x, knockback.y + rb.velocity.y);
    }

}
