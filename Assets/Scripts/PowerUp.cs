using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when we leave the screen, destroy
        if (transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    //ontriggercollision
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.TripleShotActive();
            }

            Destroy(this.gameObject);
        }
    }
    // only be collectable by player
}
