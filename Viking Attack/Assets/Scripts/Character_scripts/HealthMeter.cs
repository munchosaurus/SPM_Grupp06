using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    // Drag & drop slider to choose the color choice
    public UnityEngine.UI.Slider slider;

    // Displays the current color code in an image
    public UnityEngine.UI.Image healthImage;

    Color[] colors = new Color[]
    {
        new Color(0, 1, 0) // white
        //new Color(1, 0, 0) // black

        // to add new color, simply add a new row in the same format new Color(0, 1, 1)
    };

    public void Update()
    {
        // For updating the color of slider 
        //healthImage.color = colors[(int) slider.value];
        
    }
}
