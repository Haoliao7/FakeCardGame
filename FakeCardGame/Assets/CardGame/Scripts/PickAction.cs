using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickAction : MonoBehaviour
{
    public CardGameManager myManager;
    public GameObject Options;
    public GameObject actionOptionBox;

    public TextMeshPro fillinText;

    string content;
    public TextMeshPro actionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        content = fillinText.text; // put the text of option into content
        actionText.text = ("You "+ content); // show the content
        Options.SetActive(false);//deactivate
        Invoke("changeState", 1f); // wait for 1 sec and then change state to next state
    }

    void changeState()
    {
        actionOptionBox.SetActive(false); //deactivate
        myManager.TransitionState(CardGameManager.State.Resolve); // change state
    }


}
