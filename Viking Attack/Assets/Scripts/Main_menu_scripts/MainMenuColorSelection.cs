using UnityEngine;

namespace Main_menu_scripts
{
    public class MainMenuColorSelection : MonoBehaviour
    {
        // Drag & drop slider to choose the color choice
        public UnityEngine.UI.Slider slider;

        // Displays the current color code in an image
        public UnityEngine.UI.Image skinColor;

        Color[] colors = new Color[]
        {
            new Color(0, 0, 0), // white
            new Color(0.66f, 0.54f, 0.4f), // beige
            new Color(1, 1, 1) // black

            // to add new color, simply add a new row in the same format new Color(0, 1, 1)
        };

        public void Update()
        {
            skinColor.color = colors[(int) slider.value];
        }
    }
}