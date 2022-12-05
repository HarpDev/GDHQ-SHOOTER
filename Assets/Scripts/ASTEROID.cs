using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTEROID : MonoBehaviour
{

    [SerializeField]
    private GameObject _explosionPrefab;
 

    public float degreesPerSecond = 20;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(degreesPerSecond, degreesPerSecond, degreesPerSecond) * Time.deltaTime);
    }

    // check for laser collision 
    //instantiate effect at position
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            //begins the wave
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
            
        }
    }
}
