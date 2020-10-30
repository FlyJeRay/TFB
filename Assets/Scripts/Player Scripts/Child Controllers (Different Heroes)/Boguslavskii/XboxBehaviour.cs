using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class XboxBehaviour : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private bool isPrefab => transform.parent.name == "Prefabs";
    private bool active = false;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();

        boxCollider2D.isTrigger = true;
    }

    private void AnimationEventEnableCollider() {
        if (!isPrefab) active = true;
    }

    private void AnimationEventDestroyXbox() {
        if (!isPrefab) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<EnemyBehaviour>() != null && active) {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.GetComponent<EnemyBehaviour>() != null && active) {
            Destroy(other.gameObject);
        }
    }
}
