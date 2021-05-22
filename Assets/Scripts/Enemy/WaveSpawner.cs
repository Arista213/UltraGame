using UnityEngine;
using System.Collections;
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
    public static int EnemiesAlive = 0;
    [SerializeField] private Wave[] waves;
    public float timeBetweenWaves = 5f;
    private float _countdown = 2f;

    //public Text waveCountdownText;

    //public GameManager gameManager;

    private int waveIndex = 0;

    void FixedUpdate()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        /*
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        */
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;

        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
        //waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        //PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}