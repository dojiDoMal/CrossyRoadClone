using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing{
    LEFT,
    RIGHT,
    FORWARD
}

public class BirdController : MonoBehaviour
{
    public float timeToDisappear = 10f; 
    public float speed = 1f;
    public Facing facingDirection;

    IEnumerator Disappear(){
        while(true){
            yield return new WaitForSeconds(timeToDisappear);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider other) {
        if(other.transform.parent.GetComponent<PlayerMovement>().currentPower != PowerUp.Invincible){
            other.transform.parent.GetComponent<PlayerMovement>().EndGame();
        } else {
            StopCoroutine(Disappear());
            Destroy(this.gameObject);
        }    
    }
}
