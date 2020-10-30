using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElistratovController : PlayerController
{
    [SerializeField] private GameObject clubPrefab;
    private float distanceToClub;
    [SerializeField] private float caseCooldown;
    [SerializeField] private float caseSpawnRange;
    [SerializeField] private GameObject casePrefab;
    [SerializeField] private float caseSpeedMultiplier;
    [SerializeField] private float caseBuffTime;
    private float defaultSpeed;
    private enum BoostState {
        unBoosted,
        boosted
    }
    private BoostState boostState;

    private void Start() {
        defaultSpeed = movementSpeed;
        StartCoroutine(CaseSpawnEvent());
    }

    protected override void Ability()
    {
        distanceToClub = isLookingRight ? 1.5f : -1.5f;

        Vector2 newClubPosition = new Vector2(transform.position.x + distanceToClub, transform.position.y);
        GameObject newClub = Instantiate(clubPrefab, newClubPosition, Quaternion.identity, projectilePackage.transform);

        newClub.GetComponent<ElistratovClubBehaviour>().direction = isLookingRight ? Vector2.right : Vector2.left;
        newClub.GetComponent<ElistratovClubBehaviour>().rotationSpeed = isLookingRight ? -850 : 850;

        if (isLookingRight) newClub.GetComponent<SpriteRenderer>().flipX = true;
        
        if (boostState == BoostState.boosted) {
            newClub.GetComponent<ElistratovClubBehaviour>().flightSpeed *= caseSpeedMultiplier;
        }
    }

    private IEnumerator CaseSpawnEvent() {
        while (true && !isPrefab) {
            yield return new WaitForSeconds(caseCooldown);

            float caseLocationX_FirstHalf = Random.Range(transform.position.x - caseSpawnRange / 2, transform.position.x - 2);
            float caseLocationX_SecondHalf = Random.Range(transform.position.x + 2, transform.position.x + caseSpawnRange / 2);

            float caseLocationY_FirstHalf = Random.Range(transform.position.y - caseSpawnRange / 2, transform.position.y - 2);
            float caseLocationY_SecondHalf = Random.Range(transform.position.y + 2, transform.position.y + caseSpawnRange / 2);

            float caseLocationX;
            float caseLocationY;

            switch(Random.Range(0, 2)) {
                case 0:
                    caseLocationX = caseLocationX_FirstHalf;
                    break;
                default:
                    caseLocationX = caseLocationX_SecondHalf;
                    break;
            }

            switch(Random.Range(0, 2)) {
                case 0:
                    caseLocationY = caseLocationY_FirstHalf;
                    break;
                default:
                    caseLocationY = caseLocationY_SecondHalf;
                    break;
            }

            Vector2 caseLocation = new Vector2(caseLocationX, caseLocationY);
            Instantiate(casePrefab, caseLocation, Quaternion.identity, projectilePackage.transform);          
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Case" && boostState == BoostState.unBoosted) {
            Destroy(other.gameObject);
            StartCoroutine(CaseBoost());
        }

        if (other.gameObject.GetComponent<EnemyBehaviour>() != null) {
            stopwatch.SaveTime();
            sceneSwitcher.SwitchScene("GameOver Screen");
        }
    }

    private IEnumerator CaseBoost() {        
        if (boostState == BoostState.unBoosted) {
            movementSpeed *= caseSpeedMultiplier;
            boostState = BoostState.boosted;

            yield return new WaitForSeconds(caseBuffTime);

            movementSpeed = defaultSpeed;
            boostState = BoostState.unBoosted;
        }         
    }
}
