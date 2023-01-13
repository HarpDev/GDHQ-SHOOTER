using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidShot : MonoBehaviour
{
    public Transform _playerBullet;
    public Transform _enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    } 
    void OnTriggerEnter(Collider other) { 

            if (other.tag == "Laser")
            {
                var step = -10 * Time.deltaTime;
                _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, other.transform.position, step);
            }
        }
}
