using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    private void Awake()
    {
        text.text = GlobalPlayerInfo.GetName();
        image.color = GlobalPlayerInfo.GetSkinColor();
    }
}
