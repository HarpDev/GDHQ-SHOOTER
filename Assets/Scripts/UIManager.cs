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
    //Slider
    [SerializeField]
    private Slider _coolSlider;
    //SecondWait
    [SerializeField]
    private float _secondWait = 1f;
    //Cooldown
    [SerializeField]
    public bool _isCDActive = true;
    //IsFireActive
    [SerializeField]
    private bool _isFireActive = true;
    //LivesTask
    [SerializeField]
    private Text _livesText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_scoreText, this.gameObject);
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _deadScoreText.text = "score: " + 0;
        _livesText.text = "Lives: " ;
        _deadText.gameObject.SetActive(false);
    }
    public void SetCurrentHeat(float value)
    {
        _coolSlider.value = value;
    }
     public void SetHeatSlider(int value)
        {
            _coolSlider.maxValue = value;
        }
    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "SCORE: " + playerScore.ToString();
        _deadScoreText.text = "SCORE: " + playerScore.ToString();  
    }
    public void UpdateLives(int playerLives)
    {
        _livesText.text = "LIVES: " + playerLives.ToString();
        if (playerLives == 0)
        {
            _deadText.gameObject.SetActive(true);
        }
    }
/*
    private void Update()
    {

        
        
        if (_isCDActive == true)
        {
            if (_isFireActive == true)
            {
                
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                     _coolSlider.value += 0.12f;
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
        _coolSlider.value -= 0.005f;
        

    }

   
    */
}
