using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveParametersSo> waves = new List<WaveParametersSo>();
    [SerializeField] private float timeBetweenWaves = 2;

    private WaveParametersSo currentWave;
    [SerializeField] private GameObject enemyPrefab;

    private bool isActive = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (isActive)
        {
            foreach (WaveParametersSo wave in waves) // cycle over waves
            {
                currentWave = wave;

                List<EnemyParameterSO> waveEnemies = currentWave.enemies;

                for (int i = 0; i < waveEnemies.Count; i++) // cycle over enemies in current wave
                {
                    GameObject newEnemy = Instantiate(
                        enemyPrefab,
                        currentWave.GetWaypoints()[0].position,
                        Quaternion.identity,
                        transform);

                    newEnemy.GetComponentInChildren<SpriteRenderer>().sprite = waveEnemies[i].sprite;
                    newEnemy.GetComponent<Health>().SetInitialHealth(waveEnemies[i].health);


                    yield return new WaitForSeconds(currentWave.timeBetweenEnemies);
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    public WaveParametersSo GetCurrentWave()
    {
        return currentWave;
    }
}
