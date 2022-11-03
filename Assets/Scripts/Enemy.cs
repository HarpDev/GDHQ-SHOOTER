using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    [SerializeField]
    public bool _isLevel2Active = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second 
        if (_isLevel2Active == false)
        {
          transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if (_isLevel2Active == true)
        {
            transform.Translate(Vector3.down * Random.Range(2f, 10f) * Time.deltaTime);
        }
        


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
            //add 10 to score
            if (_player != null)
            {
                _player.Addscore(10);
            }
            Destroy(this.gameObject);
        }
        //damage player
    }
}
