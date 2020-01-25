using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour{

    private float timeLeft = 120;
    public int playerScore = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;

    void Update(){
        //time counts down every second
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);


        //if player runs out of time, they die and restart level
        if (timeLeft < 0.1f) {
            SceneManager.LoadScene("Main");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "EndLevel") {
            CountScore();
        }
        if(collision.gameObject.name == "Coin") {
            playerScore += 100;
            Destroy(collision.gameObject);
        }
    }

    void CountScore() {
        playerScore = playerScore + (int)(timeLeft * 10);
    }
}
