using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject dialogueBox;
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;

    int mousePress;

    // Start is called before the first frame update
    void Start()
    {
        mousePress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePress++; //if press left mouse button, switch to next dialogue
        }

        if(mousePress == 1)
        {
            setdialogue2();

        }else if (mousePress == 2)
        {
            setdialogue3();

        }else if (mousePress >= 3)
        {
            dialogue3.SetActive(false);
            dialogueBox.SetActive(false);
            gameManager.SetActive(true); // game start
            Destroy(gameObject); // destory this
        }


    }

    void setdialogue2()
    {
        dialogue1.SetActive(false);//activate this dialogue and diactivate the previous one
        dialogue2.SetActive(true);
    }

    void setdialogue3()
    {
        dialogue2.SetActive(false);//activate this dialogue and diactivate the previous one
        dialogue3.SetActive(true);
    }

}
