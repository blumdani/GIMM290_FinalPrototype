using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class WanderingAI : MonoBehaviour {
    private bool isAlive;
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    
    void Start() {
        isAlive = true;
    }
    void Update() {
        if(isAlive) {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit)) {
                if (hit.distance < obstacleRange) {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive (bool alive) {
        this.isAlive = alive;
    }
}