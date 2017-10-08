using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour {

	public int coinsPerSec = 5;
	public SpriteRenderer mark;
	public GameObject label;
    public GameObject deadTree;



	bool canActivate = false;
    float activationCost;
	[HideInInspector]
	public bool isMarkActivated = false;
	GameObject interactUI;

	void Start() {
		mark.gameObject.SetActive( false );
		interactUI = GameObject.FindGameObjectWithTag("InteractUI");
        activationCost = 40f;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Player")
        {
            if (!isMarkActivated)
            {
                interactUI.SetActive(true);
                interactUI.GetComponent<Text>().text = "PRESS F TO OVERTAKE";

                mark.gameObject.SetActive(true);
                canActivate = true;

                other.GetComponent<Player>().SendMessage("SetCurrentTree", this.gameObject);
            }
		}	
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if( other.tag == "Player" ) {
			if( !isMarkActivated ) {
				interactUI.SetActive( false );
				
				mark.gameObject.SetActive( false );
				canActivate = false;

				other.GetComponent<Player>().SendMessage("SetCurrentTree", null );
			}
		}
	}

	void ActivateMark( GameObject player) {
		if( canActivate ) {
			interactUI.SetActive( false );

			canActivate = false;
			isMarkActivated = true;
			mark.GetComponent<Animator>().Play("MarkActivate");
			
			CreateLabel( player.name );
			player.GetComponent<PlayerStats>().Pee( activationCost );
            player.GetComponent<Player>().score += 1;
			StartCoroutine( MarkActive( player ) );
		}
	}


	IEnumerator MarkActive( GameObject player )
	{
		StartCoroutine( GetPeeCoins( player ) );

		yield return new WaitForSeconds( player.GetComponent<PlayerStats>().markTime.GetValue() );

		isMarkActivated = false;
		mark.GetComponent<Animator>().Play("Idle");
		label.SetActive( false );
        Instantiate(deadTree, transform.position, transform.rotation);
	}

	IEnumerator GetPeeCoins( GameObject player ) {
		while( isMarkActivated && !player.GetComponent<PlayerStats>().isDead ) {
			yield return new WaitForSeconds( 1f );
			player.GetComponent<PlayerStats>().AddCoins( coinsPerSec );
		}
	}

	void CreateLabel( string name ) {
		label.SetActive( true );
		label.GetComponentInChildren<Text>().text = name;
	}
} 
