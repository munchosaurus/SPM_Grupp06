using UnityEngine;

namespace Main_menu_scripts
{
    public class MainMenuColorSelection : MonoBehaviour
    {


        // Drag & drop slider
        public UnityEngine.UI.Slider slider;

        // Drag & drop handle
        public UnityEngine.UI.Image skinColor;

        [SerializeField] Animator[] animators = new Animator[6];
        Color[] colors = new Color[]
        {
            new Color(0, 0, 0),
            new Color(0.66f, 0.54f, 0.4f),
            new Color(1, 1, 1)
            
            //new Color(0, 1, 1),
            
        };

        public void Update()
        {

            skinColor.color = colors[(int)slider.value];
        }

    }

}