using System;
using UnityEngine;
using System.Collections;
using General;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;
    public float rate;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    public float timeBetweenWaves = 20f;

    private float _countdown = 20f;
    //public Text waveCountdownText;

    //public GameManager gameManager;

    private int waveIndex = 0;

    private void Start()
    {
        foreach (var wave in waves)
            Map.EnemiesAlive += wave.count;
        print(Map.EnemiesAlive);
    }

    private void FixedUpdate()
    {
        if (_countdown <= 0f && waveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.rate);
        }

        waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}