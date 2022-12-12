using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public UIManager _UI;

    [SerializeField]
    private AudioClip _laserSoundClip;

    [SerializeField]
    private float _sizeDecreaseRate = 0.01f;

   
    //slider
    private float currentHeat = 0f;
    [SerializeField]
    private int totalHeat = 100;



    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _speedMultiplier = 2;

    //Pla
    [SerializeField]
    private GameObject _explosionPrefab;
    //CameraShake
    [SerializeField]
    private Animator _shakeAnim;
    //LaserPrefab
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



    public float _ammoCount = 15;

    [SerializeField]
    private float _ammoPowerupAmount = 15;

    [SerializeField]
    public int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isWaveShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private float _speedTime = 5.0f;


    public float _healthBack = 1;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private bool _isShotgunActive = false;

    [SerializeField]
    public bool _isGunActive = true;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;


    private bool _coolDownActive = false;


    [SerializeField]
    private float degreesPerSecond = 900f;



    private UIManager _uiManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            _lives++;
        }
    }
    public void Damage()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
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

            degreesPerSecond = degreesPerSecond + 20f;


            _lives++;
        }
        else if (_isShieldActive == false)
        {
           
            _shakeAnim.Play("Camera_Shake");
            _shieldVisualizer.SetActive(false);
            degreesPerSecond = 10f;
        }


    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void HealthPowerupActive()
    {

        _lives++;
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
    public void ammoActive()
    {
        _ammoCount = _ammoCount + _ammoPowerupAmount;

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

        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("the spawn manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("THE UI MANAGER IS NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("audioSource is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
        //sets max value to max
        _uiManager.SetHeatSlider(totalHeat);
        _uiManager.SetCurrentHeat(currentHeat);
    }
    // Update is called once per frame
    void Update()
    {

        if (_ammoCount > -1)
        {
            _isGunActive = true;
        }
        else if (_ammoCount == -1)
        {
            _isGunActive = false;
        }



        if (_isShieldActive == true)
        {
            _shieldVisualizer.transform.Rotate(new Vector3(degreesPerSecond, degreesPerSecond, degreesPerSecond) * Time.deltaTime);

        }







        
        CalculateFiring();
        CalculateMovement();



 //restart script
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

         

       

       



    }
    IEnumerator CoolDownRoutine()
    {
        yield return new WaitForSeconds(1f);
        _coolDownActive = false;
        //once, end of slider is reached, wait x seconds before being able to fire again
    }
    //method to add 10 to the score
    public void Addscore(int points)
    {
        //add score
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    private void CalculateFiring()
    {


        //FIRING SCRIPT
        if (_coolDownActive == false)
        {
            if (currentHeat > 0)
            {
                currentHeat -= 0.3f; 
                _uiManager.SetCurrentHeat(currentHeat);
            }

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {

                if (_isGunActive == true)
                {
                    _canFire = Time.time + _fireRate;
                    if (_ammoCount >= 0)
                    {

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

                        _ammoCount--;

                        _audioSource.Play();

                        currentHeat += 5;
                      
                        if (currentHeat >= totalHeat)
                        {

                            _coolDownActive = true;
                            StartCoroutine(CoolDownRoutine());
                        }

                    }
                }

            }

        }
    }
    private void CalculateMovement()
    {
        //Shift to sprint

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //transform.Translate(Vector3.left * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //checks to see if speed is active at all.
        if (_isSpeedActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y <= -3.8f)
        {

            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        //Script does the teleport across boundaries thing
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
}

