using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameManager : MonoBehaviour
{
    public State currentState;
    public int cardCount;
    public GameObject cardObj;
    public float offset;

    [Header("NPC")]
    public Vector3 firstPersonCardPos;
    public Vector3 secondPersonCardPos;
    public Vector3 thirdPersonCardPos;
    public GameObject dialogueBox;
    public TextMeshPro othersAction;
    public string[] othersActionContent;
    int othersActionRandomIndex;
    public int whoIsPlaying;
    public SpriteRenderer BG;
    public Sprite neutral;
    public Sprite lookAtYou;
    public Sprite Teasing;
    public Sprite Surprise;
    public string[] goodPlayContent;
    public string[] badPlayContent;

    [Header("Action")]
    public GameObject ActionOptionBox;
    public TextMeshPro yourSayOption;
    public TextMeshPro yourActionOption;
    int yourSayRandomIndex;
    int yourActionRandomIndex;
    public string[] yourSayContent;
    public string[] yourActionContent;
    public GameObject Options;
    public TextMeshPro actionQuestionText;

    [Header("Resolve")]
    public float howManyPercentYouMayWin;
    float dice;
    bool nicePlay;
    public GameObject goodPlayDialogueBox;
    public GameObject badPlayDialogueBox;
    public GameObject getonePointDialogueBox;
    public TextMeshPro scoreText;
    public int score;
    public int howManyScoreToWin;
    public int howManyTurns;
    int currentTurn;
    public GameObject win;
    public GameObject lose;

    public enum State// create your own data type
    {
        DrawCards,
        OthersTurn,
        Select,
        Action,
        Resolve
    }

    private static CardGameManager instance;

    //in some cases, if a variable of this game manager is NOT static, we'll need a reference to
    //the instance with the game manager

    public static CardGameManager FindInstance()
    {
        return instance;
    }

    private void Awake()
    {
        //if we have already chosen a king game manager
        //(null literally means "nothing", so if instance is NOT nothing)
        //AND
        //if the king game manager is NOT this instance of the class
        //destroy this
        if (instance != null && instance != this)
        {
            //what we;re doing is ensuring that there can only
            //be one game manager in any scene
            Destroy(this);
        }
        else //otherwise, if we do not have a king game manager
        {
            //make this the king game manager
            instance = this;
            //and do not destroy this game object when we load new scenes
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentTurn = 1;
        whoIsPlaying = 0;
        TransitionState(State.DrawCards);// game state start from DrawCards
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionState(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.DrawCards:
                BG.sprite = neutral; // change background image
                DestroyAll("card"); // destory all the objects with "card" tag
                for (int i = 0; i < cardCount; i++)
                {
                    GameObject newCard = Instantiate(cardObj); // Instantiate 5 cards to players hand
                    Vector3 newPos = new Vector3(
                        gameObject.transform.position.x + i * (newCard.transform.localScale.x + offset),
                        gameObject.transform.position.y,
                        0);
                    newCard.transform.position = newPos;
                }
                TransitionState(State.OthersTurn); // change to next state
                break;

            case State.OthersTurn:

                OthersPlayCard();

                break;
            case State.Select:
                BG.sprite = lookAtYou; // change background image
                break;
            case State.Action:
                ActionOptionBox.SetActive(true); // activate the action selection box
                Options.SetActive(true);
                actionQuestionText.text = ("What are you gonna do?");
                yourSayRandomIndex = Random.Range(0, yourSayContent.Length);//set random option for each action choice
                yourActionRandomIndex = Random.Range(0, yourActionContent.Length);
                yourSayOption.text = yourSayContent[yourSayRandomIndex];
                yourActionOption.text = yourActionContent[yourActionRandomIndex];
                break;
            case State.Resolve:
                //Random Win Condition
                dice = Random.Range(0, 100);
                if (dice < howManyPercentYouMayWin) //if the random number < the win posibility
                {
                    //win
                    BG.sprite = Surprise;// change background image
                    goodPlayDialogueBox.GetComponent<TextMeshPro>().text = goodPlayContent[currentTurn - 1];
                    goodPlayDialogueBox.SetActive(true);
                    Invoke("GetOnePoint", 1.5f);
                }
                else
                {
                    //lose
                    BG.sprite = Teasing;// change background image
                    goodPlayDialogueBox.GetComponent<TextMeshPro>().text = badPlayContent[currentTurn - 1];
                    badPlayDialogueBox.SetActive(true);
                    Invoke("NextTurn",1.5f);
                }



                break;
            default:
                Debug.Log("this state doesn't exist");
                break;

        }

    }

    void OthersPlayCard()
    {

        whoIsPlaying++;

        if (whoIsPlaying == 1)
        {
            dialogueBox.SetActive(false);
            othersAction.text = (" ");
            GameObject newCard = Instantiate(cardObj); //Instantiate a card
            newCard.transform.position = firstPersonCardPos; // put it in front of the NPC
            Invoke("OthersDoAction", 1f); // the NPC does action after 1 sec
        }else if (whoIsPlaying == 2)
        {
            dialogueBox.SetActive(false);
            othersAction.text = (" ");
            GameObject newCard = Instantiate(cardObj);//Instantiate a card
            newCard.transform.position = secondPersonCardPos;// put it in front of the NPC
            Invoke("OthersDoAction", 1f);// the NPC does action after 1 sec
        }
        else if (whoIsPlaying == 3)
        {
            dialogueBox.SetActive(false);
            othersAction.text = (" ");
            GameObject newCard = Instantiate(cardObj);//Instantiate a card
            newCard.transform.position = thirdPersonCardPos;// put it in front of the NPC
            Invoke("OthersDoAction", 1f);// the NPC does action after 1 sec
        }
        else if(whoIsPlaying == 4)
        {
            dialogueBox.SetActive(false);
            othersAction.text = (" ");
            whoIsPlaying = 0;
            TransitionState(State.Select); // change to next state
        }
    }

    void OthersDoAction()
    {
        if (whoIsPlaying == 1)//Patch
        {
            dialogueBox.SetActive(true);
            othersActionRandomIndex = Random.Range(0, othersActionContent.Length);
            othersAction.text = ("Patch " + othersActionContent[othersActionRandomIndex]); // set a random action
            Invoke("OthersPlayCard", 1.5f);
        }else if (whoIsPlaying == 2)//Sen
        {
            dialogueBox.SetActive(true);
            othersActionRandomIndex = Random.Range(0, othersActionContent.Length);
            othersAction.text = ("Sen " + othersActionContent[othersActionRandomIndex]); // set a random action
            Invoke("OthersPlayCard", 1.5f);
        }else if (whoIsPlaying == 3)//Riley
        {
            dialogueBox.SetActive(true);
            othersActionRandomIndex = Random.Range(0, othersActionContent.Length);
            othersAction.text = ("Riley " + othersActionContent[othersActionRandomIndex]); // set a random action
            Invoke("OthersPlayCard", 1.5f);
        }

        
    }


    void GetOnePoint()
    {
        score++; // get one point
        goodPlayDialogueBox.SetActive(false);
        getonePointDialogueBox.SetActive(true); // display dialogue for this part
        scoreText.text = score.ToString(); // display score
        Invoke("NextTurn", 1.5f);
    }

    void NextTurn()
    {
        getonePointDialogueBox.SetActive(false);
        badPlayDialogueBox.SetActive(false);
        
        if(currentTurn >= howManyTurns)
        {
            if (score >= howManyScoreToWin)
            {
                //Win the game
                win.SetActive(true);
                BG.sprite = Surprise;
            }
            else
            {
                //lose
                lose.SetActive(true);
                BG.sprite = Teasing;
            }


        }
        else
        {
            currentTurn++;
            TransitionState(State.DrawCards);

        }

    }


    void DestroyAll(string tag)
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag(tag); // find every object with tag named "card"
        for (int i = 0; i < cards.Length; i++)
        {
            Destroy(cards[i]); // destory them
        }
    }



}
