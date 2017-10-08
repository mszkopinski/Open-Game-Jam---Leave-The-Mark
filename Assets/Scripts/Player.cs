using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

 #region variables

    public GameObject canvas;
    public AudioClip activationSound;
    public GameObject pee;
    [HideInInspector]
    public int score;

    PlayerStats playerStats;
	float time;
	bool isPeeing = false;
	GameObject currentTree;

 #endregion

 #region methods 

    void Awake() {
		playerStats = GetComponent<PlayerStats>();
	}

    void Start()
    {
        score = 0;
    }
	
	void Update () {
		//HandlePeeing();
		HandleMarkActivation();
	}

	void HandleMarkActivation() {
		if( Input.GetKey( KeyCode.F ) && currentTree && !currentTree.GetComponent<Tree>().isMarkActivated ) {
			canvas.SetActive( true );
			time += Time.deltaTime;

			ChangeBarValue(  time * 100f / playerStats.markActivateTime );

			if( time >= playerStats.markActivateTime ) {
				currentTree.SendMessage("ActivateMark", this.gameObject );

                time = 0f;
				canvas.SetActive( false );
                GetComponent<AudioSource>().PlayOneShot(activationSound);

                GameObject tempPee = Instantiate( pee, transform.position + new Vector3(0.39f, -0.69f), transform.rotation );
				Destroy( tempPee, 2f );
			}
		}

		if( Input.GetKeyUp( KeyCode.F ) ) {
			canvas.SetActive( false );
			time = 0f;
		}
	}

	void ChangeBarValue( float value ) {
		canvas.GetComponentInChildren<Slider>().value = value;
	}

	void SetCurrentTree( GameObject obj ) {
        if (!obj)
            currentTree = null;

		currentTree = obj;
	}

#endregion
}

