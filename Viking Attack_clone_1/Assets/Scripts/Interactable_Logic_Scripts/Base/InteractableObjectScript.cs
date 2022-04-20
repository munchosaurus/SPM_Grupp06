using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    
    [SerializeField] private char buttonToPress; //The button to press to interact with the object
    [SerializeField] private string interactionDescriptionPositiv; //The text with information about a uninteracted object
    [SerializeField] private string interactionDescriptionNegative; //The text with information about a interacted object
    [SerializeField] private GameObject interactableGameObject; //The object that is interacted with
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
