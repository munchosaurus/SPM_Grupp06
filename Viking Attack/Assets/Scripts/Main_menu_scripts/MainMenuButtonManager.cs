using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject customizationScreen;
    
    // opens player customization page
    public void NewGame()
    {
        customizationScreen.SetActive(true);
    }
    
    // loads the latest save
    public void LoadGame() 
    {
        print("Load happens now");
    }
    
    // Exits the application
    public void ExitGame() 
    {
        Application.Quit();
    }
    
    // launches a new run of game
    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }
}