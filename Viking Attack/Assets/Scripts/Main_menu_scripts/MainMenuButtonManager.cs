using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButtonManager : MonoBehaviour
{
    // launches a new run of game
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadGame() 
    {
        print("Load happens now");
    }
    
    // Exits the application
    public void ExitGame() 
    {
        Application.Quit();
    }
}