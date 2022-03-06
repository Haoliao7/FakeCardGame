using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public SpriteRenderer myRender;
    public Vector3 playPos;
    CardGameManager myManager;

    public enum State //create your own data type
    {
        OnHand,
        Selected

    }

    State currentState;

    // Start is called before the first frame update
    void Start()
    {

        myManager = CardGameManager.FindInstance(); // get access to the game manager that exists in my scene

        TransitionState(State.OnHand); 
    }

    


    private void OnMouseDown()
    {
        if (myManager.currentState == CardGameManager.State.Select && gameObject.transform.position.y<-2) // if the state is Select and the cards are at the bottom of your screen
        {
            
            if (currentState == State.OnHand) //if the card is on your hand
            {
                TransitionState(State.Selected); //change the state to Selected
                myManager.TransitionState(CardGameManager.State.Action); // change the GameManager state to action
            }
        }

    }

    void TransitionState(State newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case State.Selected:
                gameObject.transform.position = playPos;// change the position
                break;
            case State.OnHand:
                break;
            default:
                Debug.Log("no transition for this state");
                break;

        }

    }
}
