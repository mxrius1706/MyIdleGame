using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Sagt das Rigidbody verf+gbar sein muss
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;// f bedeutet Flieskommazahl aso quasi 5.0
    public float runSpeed = 8f;

    public float jumpImpulse = 10f;

    private float wallJumpXImpulse = 8f;

    private float wallJumpYImpulse = 12f;

    private bool hasWallJumped = false;
    Vector2 moveInput; //Soll fuer jeds Frame neu gesetzt werden - //Dateityp vektor speichert X und Y Koordinate

    TouchingDirections touchingDirections;
    Damageable damageable;

   

    public float curMoveSpeed
    {
        get
        {
            if (canMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {

                    if (isRunning)
                    {

                        return runSpeed;
                    }

                    else
                    {
                        return walkSpeed;
                    }

                }

                else
                {
                    //Standing still
                    return 0;
                }
            }

            else
            {
                //Locked in
                return 0;
            }
        }

    }
    //Managen der laufen Animation

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {  //Property ist wie eine art intelligente variable die getter und Setter haben kann. Mischung aus Methode und variable

        get
        {
            return _isMoving;
        }
        private set
        {

            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value); //Animator gibt is isMoving den Wahrheitswert der Setterfunktion
        }
    }

    //Managen der rennen Animation
    [SerializeField]//macht das man die variable in Unity sieht - geht nur mit variablen
    private bool _isRunning = false;

    public bool isRunning
    {
        get
        {

            return _isRunning;
        }

        private set
        {

            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }

    }

    public bool canMove
    {
        get
        {

            return animator.GetBool(AnimationStrings.canMove);

        }

    }

    public bool isAlive 
    { 
        
    get {

        return animator.GetBool(AnimationStrings.isAlive);
    } 
    
    }



    Rigidbody2D rb;
    Animator animator;

    private void Awake() //Wird aufgerufen am Anfang sogar noch vor Start und gibt der rb Varible die Startwerte unseres Rigidbody
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    
        }

    // Start is called before the first frame update
    void Start() //Wenn das GameObjekt das erste mal aktiviert wird
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vorwärst rückwärts laufen - transform.localScale verändert die Skalierung des GameObjects auf der X- und Y-Achse
        if (moveInput.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void FixedUpdate()
    {
        //Bewegung auf der X Auchse wird mit dem WalkSpeed multipliziert
        //Y wird standardmäßig durch die Graviät bestimmt. Deshab übernehmen wir den Standardwert von den Rigidbody2D

        if(!damageable.LockVelocity) { //Nur laufen wenn er gerade nicht von knockback betroffen ist
        rb.velocity = new Vector2(moveInput.x * curMoveSpeed, rb.velocity.y);
        }

        if (touchingDirections.IsOnWall && !touchingDirections.isGrounded)
        {
            // X-Geschwindigkeit = 0 erzwingen, damit kein "Kleben"
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (rb.velocity.y > 0)
        {
            animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        }
        //WallJump Reset wenn auf boden
        if (touchingDirections.isGrounded && hasWallJumped)
        {
            hasWallJumped = false;
        }
    }

    //wird ausgeführt wenn Move event eintritt
    public void OnMove(InputAction.CallbackContext context)
    {
        if(isAlive) {
        moveInput = context.ReadValue<Vector2>();
        //Wenn x und y Variable von moveInput nicht gleich null sind dann bewegt sich der Player
        //Variablen Grossbuchstabe am Anfang = Konstante
        //
        IsMoving = moveInput != Vector2.zero;
        }

        else {

            IsMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {

        //Wenn shift gerade gedrückt wurde
        if (context.started)
        {

            isRunning = true;

        }

        else if (context.canceled)
        {

            isRunning = false;

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        //TODO when player is dead he wont be allowed to jump
        if (context.started)

        {

            if (canMove)
            {
                //Walljump
                if (touchingDirections.IsOnWall && !hasWallJumped && !touchingDirections.isGrounded)
                {

                    animator.SetTrigger(AnimationStrings.jump);
                    hasWallJumped = true;

                    float jumpDirection = transform.localScale.x > 0 ? -1 : 1;

                    rb.velocity = new Vector2(jumpDirection * wallJumpXImpulse, wallJumpYImpulse);
                }

                //Normaler Jump
                if (touchingDirections.isGrounded)
                {

                    animator.SetTrigger(AnimationStrings.jump);
                    rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
                }
            }
        }
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {

        if (context.started)
        {

            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    //Wird durch UnityEvent aufgerufen
    public void OnHit(int dmg, Vector2 knockback)
{

     rb.velocity = new Vector2 (knockback.x, knockback.y + rb.velocity.y);
}


}
