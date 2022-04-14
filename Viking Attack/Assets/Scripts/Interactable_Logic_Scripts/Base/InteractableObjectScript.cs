using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    //The button to press to interact with the object
    [SerializeField] private char buttonToPress;
    //The text with information about a uninteracted object
    [SerializeField] private string interactionDescriptionPositiv;
    //The text with information about a interacted object
    [SerializeField] private string interactionDescriptionNegative;
    //The object that is interacted with
    [SerializeField] private GameObject interactableGameObject;
    private string interactionDescription;
    public char ButtonToPress => System.Char.ToUpper(buttonToPress);
    public string InteractionDescription => interactionDescription;
    public void Start()
    {
        interactionDescription = interactionDescriptionPositiv;
    }
    public void buttonPressed()
    {
        if(interactionDescription.Equals(interactionDescriptionPositiv))
            interactionDescription = interactionDescriptionNegative;
        else
            interactionDescription = interactionDescriptionPositiv;
        //Calls the object to interact with (uses the BaseObjectInteraction so i can call different objects)
        interactableGameObject.GetComponent<BaseObjectInteraction>().InteractedWith();
    }
}
