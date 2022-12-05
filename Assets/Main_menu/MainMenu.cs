using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  public void LoadGame()
    {
        //load game scene
        SceneManager.LoadScene(1);//main game scene
        // in a list, 0 is one, so "1" is the second variable in the index.
    }

    public void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
        
       
    }

        
        
}
