using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _speedMultiplier = 2;




    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shotgunPrefab;

    [SerializeField]
    private GameObject _waveShotPrefab;

    public float horizontalInput;
    public float verticalInput;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isWaveShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private float _speedTime = 5.0f;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private bool _isShotgunActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;


   

    private UIManager _uiManager;



    public void Damage()
        {
            _lives--;
        //check if dead
        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
            {
            _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
            else if (_isShieldActive == true)
        {
            ShieldActive();
            _lives++;
        }
            else if (_isShieldActive == false)
        {
            _shieldVisualizer.SetActive(false);
        }
            
            
         }
    
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void WaveShotActive()
    {
        _isWaveShotActive = true;
        StartCoroutine(WaveShotPowerDownRoutine());
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        
        StartCoroutine(SpeedPowerDownRoutine());

    }

    public void ShotgunActive()
    {
        _isShotgunActive = true;

        StartCoroutine(ShotgunPowerDownRoutine());

    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);

        StartCoroutine(ShieldPowerDownRoutine());

    }


    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }




    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    IEnumerator ShotgunPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShotgunActive = false;
    }

    IEnumerator WaveShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isWaveShotActive = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        
        yield return new WaitForSeconds(_speedTime);
        _isSpeedActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //take the current position = new pos (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("the spawn manager is null");
        }
        
        if (_uiManager == null)
        {
            Debug.LogError("THE UI MANAGER IS NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {

        //if space key is hit
        //spawm game object

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            if (_isWaveShotActive == true)
            {
                Instantiate(_waveShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            else if (_isShotgunActive == true)
            {
                Instantiate(_shotgunPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

           


        }
        //restart script
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0); 

        //transform.Translate(Vector3.left * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        if (_isSpeedActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y <= -3.8f) 
        {

            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        
        
         }

         if (transform.position.x > 11.3f)
        {

            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f) 
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    

    }

    //method to add 10 to the score
    public void Addscore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    
}
