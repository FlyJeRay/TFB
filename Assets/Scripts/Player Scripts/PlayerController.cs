using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    [SerializeField] private float abilityCooldownSeconds;
    [SerializeField] protected GameObject projectilePackage;
    [SerializeField] private TextMeshProUGUI abilityText; 
    [SerializeField] protected GameStopwatch stopwatch;
    [SerializeField] protected Menu_SceneSwitcher sceneSwitcher;
    protected Rigidbody2D rigidBody2D;
    private float horizontalMovementValue;
    private float verticalMovementValue;
    protected enum AbilityState {
        Ready,
        Cooldown
    }
    protected AbilityState abilityState;
    protected bool isLookingRight;
    protected bool isPrefab => transform.parent.name == "Prefabs";
    
    [Header("Animation")]
    [SerializeField] protected Animator characterAnimator;

    private void Awake() {        
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.gravityScale = 0;
        abilityState = AbilityState.Ready;
    }

    private void FixedUpdate() {
        if (!isPrefab) {
            horizontalMovementValue = Input.GetAxis("Horizontal");
            verticalMovementValue = Input.GetAxis("Vertical");

            rigidBody2D.velocity = new Vector2(horizontalMovementValue, verticalMovementValue) * movementSpeed;
            
            characterAnimator.SetFloat("horizontal", rigidBody2D.velocity.x);
            characterAnimator.SetFloat("vertical", rigidBody2D.velocity.y);            

            if (rigidBody2D.velocity.x > 0) {
                GetComponent<SpriteRenderer>().flipX = true;
                isLookingRight = true;
            }
            else if (rigidBody2D.velocity.x < 0) {
                GetComponent<SpriteRenderer>().flipX = false;
                isLookingRight = false;
            }

            if (Input.GetKeyDown("e") && abilityState == AbilityState.Ready) {
                UseAbility();
            }
        }        
    }

    private void UseAbility() {
        Ability();        
        StartCoroutine(SetAbilityCooldown());
    }

    private IEnumerator SetAbilityCooldown() {
        abilityState = AbilityState.Cooldown;
        abilityText.text = "Падажжи";

        yield return new WaitForSeconds(abilityCooldownSeconds);

        abilityState = AbilityState.Ready;
        abilityText.text = "Способность: Е";
    }

    protected abstract void Ability();

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<EnemyBehaviour>() != null) {
            stopwatch.SaveTime();
            sceneSwitcher.SwitchScene("GameOver Screen");
        }
    }
}
