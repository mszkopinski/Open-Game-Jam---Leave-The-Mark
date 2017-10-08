using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreText;

    int score;

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    void Update () {
        scoreText.text = score.ToString();
	}
}
