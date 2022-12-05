using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _deadScoreText;

    //game over text
    [SerializeField]
    private Text _deadText;

    [SerializeField]
    private Slider _coolSlider;

    [SerializeField]
    private float _secondWait = 1f;


    [SerializeField]
    private bool _isCDActive = true;


    [SerializeField]
    private bool _isFireActive = true;


    [SerializeField]
    private Text _livesText;
    // Start is called before the first frame update
    void Start()
    {
        Player player = gameObject.transform.GetComponent<Player>();
        _isFireActive = player._isGunActive;
        

        Debug.Log(_scoreText, this.gameObject);
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _deadScoreText.text = "score: " + 0;
        _livesText.text = "Lives: " ;
        _deadText.gameObject.SetActive(false);
    }



    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        _deadScoreText.text = "Score: " + playerScore.ToString();
        
    }

    public void UpdateLives(int playerLives)
    {
        _livesText.text = "Lives: " + playerLives.ToString();

        if (playerLives == 0)
        {
            _deadText.gameObject.SetActive(true);
        }
    }


    private void Update()
    {

        
        
        if (_isCDActive == true)
        {
            if (_isFireActive == true)
            {
                
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                     _coolSlider.value += 0.1f;
                     }
            }
            
        }
         

        if (_coolSlider.value == 1f)
        {
            _isCDActive = false;
            
            
        }
        if (_coolSlider.value == 0f)
        {
                _isCDActive = true;
            
        }
       new WaitForSeconds(_secondWait);
        _coolSlider.value -= 0.001f;
        

    }



}
