using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second 

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen respawn at top
        //BONUS CHALLENGE: GET THE CUBE TO RESPAWN AT A RANDOM X POSITIONS
        if (transform.position.y <= -10f)
        {
            transform.position = new Vector3(Random.Range(-11, 11), Random.Range(10, 6), 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.transform.name);
        //if other is player
        if (other.tag == "Player")
        {
            
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        //destroy enemy
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        //damage player
    }
}
