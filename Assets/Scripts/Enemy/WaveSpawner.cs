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
    public float timeBetweenWaves = 30f;

    private float _countdown = 2f;
    //public Text waveCountdownText;

    //public GameManager gameManager;

    private int waveIndex = 0;

    void FixedUpdate()
    {
        // if (Map.EnemiesAlive > 0)
        // {
        //     return;
        // }

        /*
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        */
        if (_countdown <= 0f && waveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;
        //waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        //PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Map.EnemiesAlive++;
        Instantiate(enemy, transform.position, transform.rotation);
    }
}