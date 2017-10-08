using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[Header("UI")]
	public Image peeTube;
	public Image deathScreen;
	public List<Sprite> peeTubeSprites;
	public Text peeCoins;
    public Image heartContainer;
    public Sprite[] hearts;

	public Stat startPee;
	public Stat markTime;
	public int startPeeCoins = 0;
	public float peeTime = 2f;
	public float markActivateTime = 5f;
	public Sprite deadSprite;
    public bool isDead = false;
    public AudioClip deathSound;


	float currentPee;
	int coins;
    int deathCount;
	float time;

	void Awake() {
		currentPee = startPee.GetValue();
		coins = startPeeCoins;
		time = 0f;
        deathCount = 0;
        heartContainer.GetComponent<Image>().sprite = hearts[0];
        PlayerPrefs.SetInt("Score", 0);
    }

	public void Pee( float amount ) {
		currentPee -= amount;
		
		if( currentPee <= 0 ) {
			Die();
		}
	}

	public void DrinkWater( float amount ) {
		currentPee += amount;

		if( currentPee >= 100f ) currentPee = 100f;
	}

	public void AddCoins( int amount ) {
		coins += amount;
	}

	public void RemoveCoins( int amount ) {
		if( amount <= coins ) 
			coins -= amount;
		else 
			Debug.Log("You need more coins ");
	}

	void Update () {
		peeCoins.text = coins.ToString();
        peeTube.GetComponentInChildren<Text>().text = currentPee.ToString();

        if (currentPee <= 100f) peeTube.sprite = peeTubeSprites[4];
		if( currentPee <= 75f ) peeTube.sprite = peeTubeSprites[3]; 
		if ( currentPee <= 50f ) peeTube.sprite = peeTubeSprites[2];
		if( currentPee <= 25f ) peeTube.sprite = peeTubeSprites[1];
		if( currentPee <= 10f ) {
			peeTube.sprite = peeTubeSprites[0];	
			peeTube.GetComponent<Animator>().Play("PeeTubeShaking");
		}

        if (deathCount == 0)
            heartContainer.GetComponent<Image>().sprite = hearts[0];
        if (deathCount == 1)
            heartContainer.GetComponent<Image>().sprite = hearts[1];
        if (deathCount == 2)
            heartContainer.GetComponent<Image>().sprite = hearts[2];
    }

	void Die() {
		GetComponent<PlayerMovement>().canMove = false;
		GetComponent<Animator>().Play("Dog_Dead");
		GetComponent<SpriteRenderer>().sprite = deadSprite;
        GetComponent<AudioSource>().PlayOneShot( deathSound );

		deathScreen.gameObject.SetActive( true );

        coins = 0;
        isDead = true;
        deathCount++;

        if (deathCount < 3)
            Invoke("Respawn", 3f);
        else
            GameObject.FindObjectOfType<LevelManager>().LoadNextLevel();

        if( GetComponent<Player>().score >= PlayerPrefs.GetInt("Score") )
            PlayerPrefs.SetInt("Score", GetComponent<Player>().score );

        GetComponent<Player>().score = 0;
    }	

	void Respawn() {
		transform.position = GameObject.FindObjectOfType<Spawner>().transform.position;
		currentPee = startPee.GetValue();
		peeTube.sprite = peeTubeSprites[4];
		GetComponent<Animator>().Play("Dog_Idle");
		peeTube.GetComponent<Animator>().Play("Idle");
		deathScreen.gameObject.SetActive( false );
		GetComponent<PlayerMovement>().canMove = true;
        isDead = false;
	}
}
