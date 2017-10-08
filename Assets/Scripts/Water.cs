using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour {

    public KeyCode drinkKey;
    public Sprite[] sprites;

    int drinks;
    bool canDrink = false;
    SpriteRenderer sRenderer;
    GameObject currentTarget;
    GameObject interactUI;

    void Start()
    {
        drinks = 2;
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sprites[ drinks ];
        interactUI = GameObject.FindGameObjectWithTag("InteractUI");
    }

    void Update () {
        if( drinks >= 0 )
            sRenderer.sprite = sprites[ drinks ];

		if( canDrink && Input.GetKeyDown( drinkKey ) )
        {
            drinks--;

            if (currentTarget)
                currentTarget.GetComponent<PlayerStats>().DrinkWater( Mathf.Round(Random.Range(15f, 35f)));

            StartCoroutine(DrinkDelay());
        }

        if (drinks < 0)
            DestroyImmediate(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            canDrink = true;
            currentTarget = col.gameObject;
            interactUI.SetActive(true);
            interactUI.GetComponent<Text>().text = "PRESS E TO DRINK";
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            canDrink = false;
            currentTarget = null;
            interactUI.SetActive(false);
        }
    }

    IEnumerator DrinkDelay()
    {
        canDrink = false;

        yield return new WaitForSeconds(2f);

        canDrink = true;
    }
}
