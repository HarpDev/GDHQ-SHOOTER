using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //This is the speed the powerup falls.
    [SerializeField]
    private float _speed = 3.0f;

    

    //player Target
    private Player _playerTarget;



    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    private void Start()
    {
        _playerTarget = GameObject.Find("Player").GetComponent<Player>();
    }





    // Update is called once per frame
    void Update()
    {
           
        if (Input.GetKey(KeyCode.E))
        {
            //when player holds down E key, Powerups move to player
            var step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, step);
          
        }
        else
        {
           
            //move down as usual
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, _playerTarget.transform.position) < 0.001f)
        {
            
            _playerTarget.transform.position *= -1.0f;
        }
        // move down at a speed of 3 

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
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            
            if(player != null)
            {
                if (powerupID == 0)
                {
                    //Activate Triple Shot
                    player.TripleShotActive();
                }
                //else if 1
                else if (powerupID == 1)
                {
                    //Activate Speed
                    player.SpeedActive();
                }
                else if (powerupID == 2)
                {
                    //Activate Shield
                    player.ShieldActive();
                }
                else if (powerupID == 3)
                {
                    //Activate Wave Shot 
                    player.WaveShotActive();
                }
                else if (powerupID == 4)
                {
                    //Activate Shotgun
                    player.ShotgunActive();
                }
                else if (powerupID == 5)
                {
                    //giveAmmo
                    player.ammoActive();
                }
                else if (powerupID == 6)
                {
                    //giveHealth
                    player.HealthPowerupActive();
                    
                }

                

                /*switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("collected Speed Boost");
                        player.SpeedActive();
                        break;
                    case 2:
                        Debug.Log("collected Sheilds");

                    

                        break;
                    
                }
              */
            }

            

            Destroy(this.gameObject);
        }
    }
    // only be collectable by player
}
