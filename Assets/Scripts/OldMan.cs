using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour {

	public float moveSpeed = 5f;
	public float changeDirectionAfterMin = 2f;
	public float changeDirectionAfterMax = 5f;
	public float attackDistance = 2f;
	public int attackDamage = 50;
	public bool vertical = false;
    public AudioClip attackSound;
	
	float time;
	float fatigueTime;
	float changeDirectionAfter;
	bool direction = true;
	bool canMove = true;
	bool attacked = false;
	Player currentTarget;
	List<Player> players = new List<Player>();

	void Start() {
		changeDirectionAfter = Random.Range( changeDirectionAfterMin, changeDirectionAfterMax );
	}

	void Update () {
		time += Time.deltaTime;
		fatigueTime += Time.deltaTime;

		if( time >= changeDirectionAfter ) {
			direction = !direction;
			GetComponentInChildren<SpriteRenderer>().flipX = !GetComponentInChildren<SpriteRenderer>().flipX;
			changeDirectionAfter = Random.Range( changeDirectionAfterMin, changeDirectionAfterMax );
			time = 0f;
		}

		if( fatigueTime >= 20f ) Rest();
			
		if( canMove ) {
			if( !vertical )
				transform.position += Vector3.right * moveSpeed * Time.deltaTime * CalculateDirection();
			if( vertical )
				transform.position += Vector3.up * moveSpeed * Time.deltaTime * CalculateDirection();
			GetComponentInChildren<Animator>().Play("Walk");
		}

		if( currentTarget ) {
			canMove = false;
			transform.position = Vector2.MoveTowards( transform.position, currentTarget.transform.position, moveSpeed * 2f * Time.deltaTime );

			if( Vector3.Distance( transform.position, currentTarget.transform.position ) <= attackDistance && !attacked) {
				Attack();
			}
		} else {
			canMove = true;
			attacked = false;
		}
	}

	float CalculateDirection() {
		return direction ? 1f : -1f;
	}

	void Rest() {
		GetComponentInChildren<Animator>().Play("Idle");
		StartCoroutine( Resting() );
	}

	IEnumerator Resting() {
		canMove = false;
		yield return new WaitForSeconds( 5f );
		canMove = true;
	}

	void MoveTowardsTarget() {
		if( currentTarget ) {
			Vector2.MoveTowards( transform.position, currentTarget.transform.position, 0f );
		}
	}

	void Attack() {
		GetComponentInChildren<Animator>().Play("Attack");
        GetComponent<AudioSource>().PlayOneShot(attackSound);
		currentTarget.GetComponent<PlayerStats>().Pee( attackDamage );
		attacked = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if( col.GetComponent<Player>() ) {
			currentTarget = col.GetComponent<Player>(); 

			if( !players.Contains( currentTarget ) ) {
				players.Add( currentTarget );
				Debug.Log("Current target " + currentTarget.gameObject.name );
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		currentTarget = null;
	}
}
