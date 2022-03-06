using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsContent : MonoBehaviour
{
    public SpriteRenderer myRender;
    public Color[] mycolor;
    int randomColorIndex;

    public TextMeshPro myText;
    public string[] myTextContent;
    int randomTextIndex;

    public SpriteRenderer myImage;
    public Sprite[] myImageContent;
    int randomSpriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        randomColorIndex = Random.Range(0, mycolor.Length); // set a random color to this card
        myRender.color = mycolor[randomColorIndex];

        randomTextIndex = Random.Range(0, myTextContent.Length); // set a random text to this card
        myText.text = myTextContent[randomTextIndex];

        randomSpriteIndex = Random.Range(0, myImageContent.Length); // set a random image to this card
        myImage.sprite = myImageContent[randomSpriteIndex];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
