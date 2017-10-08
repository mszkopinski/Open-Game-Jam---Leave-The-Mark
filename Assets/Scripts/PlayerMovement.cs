using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 2f;
	public float sprintCooldown = 2f;
	public float sprintTime = 1f;
	public float speedUpFactor = 2f;

	float currentSpeed;
	bool canSprint;
	[HideInInspector]
	public bool canMove = true;
	SpriteRenderer spriteRenderer;

	void Awake () {
		currentSpeed = moveSpeed;
		canSprint = true;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update () {
		if( canMove ){
			if( Input.GetKeyDown( KeyCode.LeftArrow ) ) {
				spriteRenderer.flipX = false;
			}

			if( Input.GetKey( KeyCode.LeftArrow ) ) {
				transform.position += Vector3.left * Time.deltaTime * currentSpeed;
                GetComponent<Animator>().Play("Dog_Walk");
			}

			if( Input.GetKey( KeyCode.RightArrow ) ) {
				transform.position += Vector3.right * Time.deltaTime * currentSpeed;
                GetComponent<Animator>().Play("Dog_Walk");
            }

			if( Input.GetKeyDown( KeyCode.RightArrow ) ) {
				spriteRenderer.flipX = true;
			}

			if( Input.GetKey( KeyCode.DownArrow ) ) {
				transform.position += Vector3.down * Time.deltaTime * currentSpeed;
                GetComponent<Animator>().Play("Dog_Walk");
            }

			if( Input.GetKey( KeyCode.UpArrow ) ) {
				transform.position += Vector3.up * Time.deltaTime * currentSpeed;
                GetComponent<Animator>().Play("Dog_Walk");
            }


			if( Input.GetKey( KeyCode.LeftShift ) && canSprint ) {
				StartCoroutine( Sprint() );
			} 
		}
	}

	IEnumerator Sprint()
	{
		if( currentSpeed == moveSpeed ) 
			currentSpeed *= speedUpFactor;

		yield return new WaitForSeconds( sprintTime );
		currentSpeed = moveSpeed;
		canSprint = false;
		StartCoroutine( ReloadSprint() );
	}

	IEnumerator ReloadSprint() {
		yield return new WaitForSeconds( sprintCooldown );
		canSprint = true;
	}
}
