using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ApanasikKniveBehaviour : MonoBehaviour
{
    [HideInInspector] public Vector2 direction;
    [SerializeField] private float flightSpeed;
    private Rigidbody2D rigidBody2D;
    private bool isNotPrefab => transform.parent.name != "Prefabs";

    private void Awake() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.gravityScale = 0;
    }

    private void FixedUpdate() {
        if (isNotPrefab) {
            rigidBody2D.velocity = direction * flightSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (isNotPrefab) {
            if (other.gameObject.GetComponent<EnemyBehaviour>() != null) {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
