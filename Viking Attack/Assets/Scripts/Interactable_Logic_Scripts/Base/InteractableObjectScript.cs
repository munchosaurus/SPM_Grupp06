using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private char buttonToPress;
    [SerializeField] private string interactionDescriptionPositiv;
    [SerializeField] private string interactionDescriptionNegative;
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
        interactableGameObject.GetComponent<BaseObjectInteraction>().interactedWith();
    }
}
