﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour{

    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(gameObject.transform.position.y < -7) {
            Die();
        }
    }

    void Die() {
        SceneManager.LoadScene("Main");
    }
}
