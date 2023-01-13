using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(Rigidbody))]


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    private Rigidbody _rb;

    [SerializeField]
    public bool _isReverserType = false;

    [SerializeField]
    public bool _isShieldActive = false;

    [SerializeField]
    public bool _isBackwardsActive = false;

    [SerializeField]
    public bool _isBullet = false;


    [SerializeField]
    private float _speed = 8.0f;

    public Transform target;
   
    [SerializeField]
    private GameObject explosionEffect;

    [SerializeField]
    private GameObject PowerUpEnemyPrefab;

    private Player _player;

    [SerializeField]
    public bool _isBossActive = false;

    [SerializeField]
    public bool _isLevel2Active = false;

    [SerializeField]
    public bool _isShieldEnemy = false;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player Is null");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("anim Is null");
        }
    }

  




    // Update is called once per frame
    void Update()
    {

       if (_isBullet == true){
            transform.LookAt(Vector3.zero);
        }
        
          
        //move down at 4 meters per second 
        if (_isLevel2Active == false)
        {

            //transform.LookAt(Vector3.zero);
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
           
          
        }
        else if (_isLevel2Active == true)
        {
          
                transform.Translate(Vector3.down * Random.Range(2f, 10f) * Time.deltaTime);
                
                
                
            _isBackwardsActive = _isBackwardsActive == true;
          
            
        }
        
        if (_isReverserType == true)
        {

            if (transform.position.y < -9f)
            {
                //rptate z 180 degrees
                transform.Rotate(new Vector3(0, 0, 180));
            }
            if (transform.position.y > 100f)
            {
                Destroy(this.gameObject);
            }
        }
        if (_isBossActive == false)
        {
if (transform.position.y <= -10f)
        {
            
            
            
                transform.position = new Vector3(Random.Range(-11, 11), Random.Range(10, 6), 0);
            

        }
        else if (transform.position.y <= -9f)
        {
          if (_isLevel2Active == true)
            {
                transform.position = new Vector3(Random.Range(-11, 11), Random.Range(10, 6), 0);
                {
                    

                }
    
            
            }
        }     
       
        }
        //if bottom of screen respawn at top
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyPickup")
        {
            //Spawn Enemy Type Reverser
            Instantiate(PowerUpEnemyPrefab, transform.position, Quaternion.identity);
            //Destroy Current Enemy Type
            Destroy(this.gameObject);

        }
        if (_isShieldActive == true)
        {
            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
                _isShieldActive = _isShieldActive = false;
            }
        }
       else if (_isShieldActive == false)
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
                Laser laser = other.transform.GetComponent<Laser>();
                if (laser != null)
                {
                    if (laser.IsEnemyLaser() == false)
                    {
                        Destroy(other.gameObject);

                        if (_player != null)
                        {
                            _player.Addscore(10);
                        }
                        Destroy(this.gameObject);
                    }
                }

            }

         
           

        }
    }
}
