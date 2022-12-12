using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _lvl2EnemyPrefab;

    [SerializeField]
    public bool _isLevel2Active = false;

    [SerializeField]
    private GameObject _enemyContainer;


    //SpawnRate
    [SerializeField]
    private float _spawnRate = 0.5f;
    //SpawnRateUp
    [SerializeField]
    private float _powerupSpawnRate = 0.5f;
    //3Up
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    //AmmoUp
    [SerializeField]
    private GameObject _ammoPrefab;
    //SpeedUp
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    //ShieldUp
    [SerializeField]
    private GameObject _shieldPowerUpPrefab;
    //WaveUp
    [SerializeField]
    private GameObject _waveShotPowerUpPrefab;
    //ShotUp
    [SerializeField]
    private GameObject _shotgunPowerUpPrefab;
    //HealthUp
    [SerializeField]
    private GameObject _healthPowerUpPrefab;




    private bool _stopSpawning = false;

   

   


    public void StartSpawning()
    {
        //Powerup
        StartCoroutine(SpawnRoutine());
        //Enemy
        StartCoroutine(SpawnEnemyRoutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    //spawn  gameobjects every 5 seconds
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            _spawnRate = Random.Range(0.5f, 1f);
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
           GameObject newEnemy = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
            if (_isLevel2Active == true)
            {
                Vector3 postToSpawn2 = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy2 = Instantiate(_lvl2EnemyPrefab, postToSpawn2, Quaternion.identity);
                newEnemy2.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnRate);
            }
        }
    }
    IEnumerator SpawnEnemyRoutine()
    {
         while (_stopSpawning == false)
        {
            //The reason why each powerup is using a different random.range variable is there is a glitch with powerups spawning on top of each other.
            //Random 1
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //Random 2
            Vector3 postToSpawn2 = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //Random 3
            Vector3 postToSpawn3 = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //Random 4
            Vector3 postToSpawn4 = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //Random 5
            Vector3 postToSpawn5 = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //Random 6
            Vector3 postToSpawn6 = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);
            //triple shot
            Instantiate(_tripleShotPowerUpPrefab, postToSpawn, Quaternion.identity);
            //speed
            Instantiate(_speedPowerUpPrefab, postToSpawn2, Quaternion.identity);
            //shield
            Instantiate(_shieldPowerUpPrefab, postToSpawn3, Quaternion.identity);
            //waveshot
            Instantiate(_waveShotPowerUpPrefab, postToSpawn4, Quaternion.identity);
            //shotgun
            Instantiate(_shotgunPowerUpPrefab, postToSpawn5, Quaternion.identity);
            //ammo
            Instantiate(_ammoPrefab, postToSpawn5, Quaternion.identity);
            //health
            Instantiate(_healthPowerUpPrefab, postToSpawn6, Quaternion.identity);
            yield return new WaitForSeconds(_powerupSpawnRate);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }
}
