using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;


public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;


    private bool _isAlive = true;

    [SerializeField]
    private bool IsInvincible = false;



    public bool IsAlive
    {
        get
        {

            return _isAlive;

        }
        set
        {

            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, _isAlive);


        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {

            return _maxHealth;
        }

        set
        {

            _maxHealth = value;

        }
    }
    [SerializeField]
    private int _health = 100;
    private float timeSinceHit = 0;

    [SerializeField]
    public float invincibilityTime = 0.25f;

    public int Health
    {
        get
        {

            return _health;
        }
        set
        {

            _health = value;

            if (_health <= 0)
            {

                IsAlive = false;
            }
        }
    }

    public bool hit(int dmg, Vector2 knockback)
    {

        if (IsAlive && !IsInvincible)
        {

            Health -= dmg;
            IsInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(dmg, knockback);
            CharacterEvents.characterDamaged.Invoke(dmg, gameObject);

            return true;
        }

        return false;
    }

    private void Update()
    {

        if (IsInvincible)
        {

            if (timeSinceHit > invincibilityTime)
            {

                IsInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime; //Time.Delta time ist die Zeit zwischen den frames
        }





    }

    public bool Heal(int healAmount)
    {
        if (IsAlive && Health != MaxHealth)
        {
           
            if (Health>= (MaxHealth - healAmount)) {
                healAmount = MaxHealth - Health;
            }

            Health += healAmount;
            CharacterEvents.characterHealed?.Invoke(healAmount, gameObject);

           
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            return true;
        }

        return false;

    }

    void Awake()
    {
        animator = GetComponent<Animator>();

    }
}

