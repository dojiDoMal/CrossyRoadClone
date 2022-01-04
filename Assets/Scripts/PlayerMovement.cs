using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public enum PowerUp{
    IncreaseSpeed,
    StartSwing,
    StopSwing,
    Invincible,
    None
}

public class PlayerMovement : MonoBehaviour
{
    public PowerUp currentPower = PowerUp.None;
    public float invicibleTime = 5f;
    public float increaseSpeedTime = 3f;

    bool movingLeft;
	bool movingRight;
    bool movingForward;

    public GameObject VFXInvincible;

    private bool isRotating = false;

    private Vector3 initialPosition; 
    public int Distance {
        get{ return (Mathf.RoundToInt(Vector3.Distance(initialPosition, new Vector3(0f,0f,transform.position.z))) / distanceScale) - 8; }
    }
    public int distanceScale = 5;

    public CharacterController controller;
    public Transform playerGraphics;

    public Rigidbody rb;

    public float speed = 6f;
    public float speedIncrease = 4f;
    public float rotationLimit = 0.33f; 
    public float timeToWait = 1.5f;
    
    public int fallingRotation = 20;

    public Animator animator;
 
    public GameObject target;

    public float rotationSpeed = 5f;

    IEnumerator GameOver(){
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    IEnumerator WaitBeforeMoving()
    {
        controller.enabled = false;
        yield return new WaitForSeconds(timeToWait);
        controller.enabled = true;
    }

    IEnumerator EndPowerUp(PowerUp power, float time = 0){
        currentPower = power;
        yield return new WaitForSeconds(time);
        currentPower = PowerUp.None;
    }

    void Awake() {
        AirConsole.instance.onMessage += OnMessage;        
    }

    void Start() {
        initialPosition = transform.position;   
        StartCoroutine(WaitBeforeMoving());
    }

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float vertical = verticalInput < 0 ? 0 : verticalInput;

        Vector3 direction = new Vector3(0f, 0f, 1f).normalized;
        
        if(playerGraphics.transform.rotation.x < -rotationLimit || playerGraphics.transform.rotation.x > rotationLimit){
            EndGame();
        }

        if(movingForward){
            if(!animator.GetBool("isWalking")){
                animator.SetBool("isWalking", true);
            }
            if(currentPower == PowerUp.IncreaseSpeed){
                controller.Move(direction * (speed + speedIncrease) * Time.deltaTime);
            }
            controller.Move(direction * speed * Time.deltaTime);
        } else {
            animator.SetBool("isWalking", false);
        }

        if((movingLeft || movingRight) && controller.enabled){
            if(movingLeft){
                playerGraphics.transform.RotateAround(target.transform.position, -playerGraphics.transform.forward, 100*Time.deltaTime);
            } else {
                playerGraphics.transform.RotateAround(target.transform.position, -playerGraphics.transform.forward, -100*Time.deltaTime);
            }
            
            isRotating = true;
            if(currentPower == PowerUp.StopSwing) currentPower = PowerUp.None;
        }

        if((isRotating && currentPower != PowerUp.StopSwing) || currentPower == PowerUp.StartSwing){
            if(playerGraphics.transform.rotation.x > 0){
                playerGraphics.transform.RotateAround(target.transform.position, playerGraphics.transform.forward, fallingRotation*Time.deltaTime);
            }
            else if(playerGraphics.transform.rotation.x <= 0){
                playerGraphics.transform.RotateAround(target.transform.position, playerGraphics.transform.forward, -fallingRotation*Time.deltaTime);
            }
        }

        if(currentPower == PowerUp.Invincible){
            VFXInvincible.SetActive(true);
            StopCoroutine(EndPowerUp(currentPower));
            StartCoroutine(EndPowerUp(currentPower, invicibleTime));
        } else {
            VFXInvincible.SetActive(false);
        }

        if(currentPower == PowerUp.IncreaseSpeed){
            StopCoroutine(EndPowerUp(currentPower));
            StartCoroutine(EndPowerUp(currentPower, increaseSpeedTime));
        }

    }

    public void EndGame(){
        controller.enabled = false;
        if(this.gameObject.GetComponent<Rigidbody>() == null) this.gameObject.AddComponent<Rigidbody>();
        StartCoroutine(GameOver());
    }

    void OnDestroy() {
        if(AirConsole.instance != null){
            AirConsole.instance.onMessage -= OnMessage;
        }
    }

	void OnMessage (int from, JToken data){
		Debug.Log ("message: " + data);
        switch(data.SelectToken("action").ToObject<string>()){
            case "right":
                movingRight = true;
                break;
            case "left":
                movingLeft = true;
                break;
            case "right-up":
			    movingRight = false;
			    break;
		    case "left-up":
			    movingLeft = false;
			    break;
            case "jump":
			    movingForward = true;
			    break;
		    case "jump-up":
			    movingForward = false;
			    break;
        }
	}
}