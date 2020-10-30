using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApanasikContoller : PlayerController
{
    [SerializeField] private GameObject knivePrefab;
    private float distanceToKnive;
    [SerializeField] private RuntimeAnimatorController shiryaevAnimator;
    [SerializeField] private RuntimeAnimatorController apanasikAnimator;

    private enum AnimationState {
        Apanasik,
        Shiryaev
    }

    private AnimationState currentAnimState;

    private void Awake() {        
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.gravityScale = 0;
        abilityState = AbilityState.Ready;

        StartCoroutine(BecomeShiryaevEvent());
    }

    protected override void Ability()
    {
        distanceToKnive = isLookingRight ? 1.5f : -1.5f;

        Vector2 newKnivePosition = new Vector2(transform.position.x + distanceToKnive, transform.position.y);
        GameObject newKnive = Instantiate(knivePrefab, newKnivePosition, Quaternion.identity, projectilePackage.transform);

        newKnive.GetComponent<ApanasikKniveBehaviour>().direction = isLookingRight ? Vector2.right : Vector2.left;
        if (isLookingRight) newKnive.GetComponent<SpriteRenderer>().flipX = true;
    }

    private IEnumerator BecomeShiryaevEvent() {
        while (true) {
            yield return new WaitForSeconds(10);
            int chance = Random.Range(0, 11);
            if (chance == 10 && currentAnimState == AnimationState.Apanasik) {
                characterAnimator.runtimeAnimatorController = shiryaevAnimator;
                currentAnimState = AnimationState.Shiryaev;
                StartCoroutine(BecomeApanasikBack());
            }
        }
    }

    private IEnumerator BecomeApanasikBack() {
        yield return new WaitForSeconds(5);
        characterAnimator.runtimeAnimatorController = apanasikAnimator;
        currentAnimState = AnimationState.Apanasik;
    }
}
