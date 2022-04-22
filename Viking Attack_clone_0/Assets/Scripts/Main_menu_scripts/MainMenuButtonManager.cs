using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Who to blame: Martin Kings

// The script handles user input in terms of skin color and name choices, ships the chosen
// data to the GlobalPlayerInfo container.
public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject customizationScreen;
    [SerializeField] private Text chosenName;
    [SerializeField] private Image displayedImage;
    [SerializeField] private GameObject player;
    
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
    
    // launches a new run of game, ships the current inserted name and the chosen color to the GlobalPlayerInfo container
    public void LaunchGame()
    {
        //GlobalPlayerInfo.SetSkinColor(displayedImage.color);
        //lobalPlayerInfo.SetPlayerName(chosenName.text);
        SceneManager.LoadScene(1);
    }
}