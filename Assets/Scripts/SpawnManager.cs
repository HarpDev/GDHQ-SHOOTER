using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _spawnRate = 0.5f;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    [SerializeField]
    private GameObject _shieldPowerUpPrefab;
    [SerializeField]
    private GameObject _waveShotPowerUpPrefab;
    [SerializeField]
    private GameObject _shotgunPowerUpPrefab;

    

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
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
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
           GameObject newEnemy = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }

    }


    IEnumerator SpawnEnemyRoutine()
    {
         while (_stopSpawning == false)
        {
            //1
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //2
            Vector3 postToSpawn2 = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //3
            Vector3 postToSpawn3 = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //4
            Vector3 postToSpawn4 = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //5
            Vector3 postToSpawn5 = new Vector3(Random.Range(-8f, 8f), 7, 0);

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
            yield return new WaitForSeconds(Random.Range(1, 3));

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }
      
    

}
