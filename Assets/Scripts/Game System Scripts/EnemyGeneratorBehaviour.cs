using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyPackage;
    [SerializeField] private float generationPeriod, generationRange;
    [SerializeField] private float minAmountOfEnemies, maxAmountOfEnemies;

    private void Awake() {
        StartCoroutine(EnemyGeneratingCycle());
    }

    private IEnumerator EnemyGeneratingCycle() {
        while(true) {
            float enemiesAmount = Random.Range(minAmountOfEnemies, maxAmountOfEnemies);

            for (int i = 0; i < enemiesAmount; i++) {
                float newEnemy_positionX = Random.Range(transform.position.x - generationRange, transform.position.x + generationRange);
                float newEnemy_positionY = Random.Range(transform.position.y - generationRange, transform.position.y + generationRange);

                Vector2 newEnemy_position = new Vector2(newEnemy_positionX, newEnemy_positionY);

                Instantiate(enemyPrefab, newEnemy_position, Quaternion.identity, enemyPackage);
            }

            generationPeriod = generationPeriod / 1.2f;

            yield return new WaitForSeconds(generationPeriod);
        }        
    }
}
