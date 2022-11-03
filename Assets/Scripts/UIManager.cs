using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;

    //game over text
    [SerializeField]
    private Text _deadText;





    [SerializeField]
    private Text _livesText;
    // Start is called before the first frame update
    void Start()
    {
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _livesText.text = "Lives: " + 10;
        _deadText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        
    }

    public void UpdateLives(int playerLives)
    {
        _livesText.text = "Lives: " + playerLives.ToString();

        if (playerLives == 0)
        {
            _deadText.gameObject.SetActive(true);
        }
    }


    
}
