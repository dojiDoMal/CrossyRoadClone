using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType{
    YELLOW,
    RED,
    BLUE,
    PURPLE
}

public class Coin : MonoBehaviour
{

    public float rotationSpeed = 3f;
    public Vector3 direction;

    public GameObject VFX;

    public CoinType type;

    void Update()
    {
        transform.Rotate(direction * (rotationSpeed * Time.deltaTime));    
    }

    void OnTriggerEnter(Collider other) 
    {   
        if(other.transform.parent.GetComponent<PlayerMovement>()){
            PlayerMovement player = other.transform.parent.GetComponent<PlayerMovement>();
            switch(type){
                case CoinType.YELLOW:
                {
                    player.currentPower = PowerUp.IncreaseSpeed;
                    break;
                }
                case CoinType.RED:
                {
                    player.currentPower = PowerUp.StartSwing;
                    break;
                }
                case CoinType.BLUE:
                {
                    player.currentPower = PowerUp.StopSwing;
                    break;
                }
                case CoinType.PURPLE:
                {
                    player.currentPower = PowerUp.Invincible;
                    break;
                }
            }
            Instantiate(VFX, transform.position, transform.rotation);
            //Destroy(Instantiate(VFX, transform.position, transform.rotation), 4f);
            Destroy(this.gameObject);  
        }
    }
}
