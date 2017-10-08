using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pigeon : MonoBehaviour {

	public GameObject pigeonUI;
    public int beerCost = 20;
    public GameObject beerParticles;

	GameObject currentInteracter;
	GameObject interactUI;
	bool isBuyable = false;
    float beerAdd;

    void Start() {
        beerAdd = Mathf.Round(Random.Range( 15f, 35f ));
		interactUI = GameObject.FindGameObjectWithTag("InteractUI");
	}

	void Update() {
		if( isBuyable && Input.GetKeyDown( KeyCode.X ) ) {
			currentInteracter.GetComponent<PlayerStats>().RemoveCoins( beerCost );
            currentInteracter.GetComponent<PlayerStats>().DrinkWater(beerAdd);
            GameObject temp = Instantiate(beerParticles, transform.position, transform.rotation) as GameObject;
            Destroy(temp, 2f);
		}
	}

	void OnTriggerEnter2D ( Collider2D col ) {
		if( col.tag == "Player" ) {
			pigeonUI.SetActive( true );
			interactUI.SetActive( true );
			interactUI.GetComponent<Text>().text = "PRESS X TO BUY " + beerCost.ToString();
			currentInteracter = col.gameObject;
			isBuyable = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if( col.tag == "Player" ) {
			pigeonUI.SetActive( false );
			interactUI.SetActive( false );
			currentInteracter = null;
			isBuyable = false;
		}
	}
}
