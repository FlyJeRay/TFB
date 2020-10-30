using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private AIPath enemyAIPath;
    [SerializeField] private GameStart_MC_Creator mcCreator;
    private SpriteRenderer spriteRenderer;
    private AIDestinationSetter destinationSetter;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        destinationSetter.target = mcCreator.currentPlayer.transform;       

        if (transform.parent.name == "Prefabs") enemyAIPath.canMove = false;
        else enemyAIPath.canMove = true;
    }

    private void FixedUpdate() {
        if (enemyAIPath.desiredVelocity.x >= 0.01f) {
            spriteRenderer.flipX = true;
        }            
        else if (enemyAIPath.desiredVelocity.x <= 0.01f) {
            spriteRenderer.flipX = false;
        }            
    }
}