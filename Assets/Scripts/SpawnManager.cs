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
    private GameObject _lvl3EnemyPrefab;

    [SerializeField]
    public bool _isLevel2Active = false;

    [SerializeField]
    public bool _isLevel3Active = false;

    [SerializeField]
    private GameObject _enemyContainer;


    //SpawnRate
    [SerializeField]
    private float _spawnRate = 0.5f;
    //SpawnRateFor Enemy2
    [SerializeField]
    private float _spawnRatePoweredEnemy = 2f;
    [SerializeField]
    private float _spawnRateShieldEnemy = 2f;
    //SpawnRateUp
    [SerializeField]
    private float _powerupSpawnRate = 0.5f;
    //3Up
    [SerializeField]
    private GameObject[] _avaliablePowerups;

    



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
                yield return new WaitForSeconds(_spawnRatePoweredEnemy);
            }
            if (_isLevel3Active == true)
            {
                Vector3 postToSpawn3 = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy3 = Instantiate(_lvl3EnemyPrefab, postToSpawn3, Quaternion.identity);
                newEnemy3.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnRateShieldEnemy);
            }
        }
    }




    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            //pick a powerup to spawn
            int selectedPowerup = Random.Range(0, _avaliablePowerups.Length);

            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), Random.Range(7, 30), 0);

            Instantiate(_avaliablePowerups[selectedPowerup], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(_powerupSpawnRate);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }
}
