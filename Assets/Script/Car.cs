using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update、private Animator animation_controller;
    private CharacterController controller;
	//private Animator animation_controller;
	public GameObject car;
    public Vector3 moveDirection;
	// public float turnSpeed = 10.0f;
    public float turnSpeed = 50.0f;
    public float walking_velocity;   
    public float movementSpeed;
	public float rotationSpeed = 10.0f;
	public float gravity = 20.0f;	
    void Start()
    {
        //animation_controller = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 2.0f;
        // movementSpeed = 5.0f;
        movementSpeed = 0.0f;
        //controller = GetComponent<CharacterController>();
        // car = GameObject.Find("Car_4_Blue");
        // Instantiate(car, new Vector3(6.0f, 1.1f, 3.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    //     float zdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
    //     float xdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float xdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        moveDirection = new Vector3(xdirection, 0.0f, zdirection);
		//character_controller.Move(movement_direction * velocity * Time.deltaTime);
		Debug.Log("Moving direction is: "+ moveDirection);

		if(Input.GetKey(KeyCode.UpArrow)) {
             // transform.position += transform.forward * Time.deltaTime * movementSpeed;
             // moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
             // transform.position += moveDirection * Time.deltaTime * movementSpeed;
            //  if (movementSpeed < 5.0f) {
            //      movementSpeed += 0.1f;
            //  }
             movementSpeed = 5.0f;
			 //transform.position += moveDirection * Time.deltaTime * movementSpeed;
             // Debug.Log("Up pressed");
             Debug.Log("Up pressed, moving direction is: "+ moveDirection);
			 
         }else if(Input.GetKeyUp(KeyCode.UpArrow)){
			 movementSpeed = 0.0f;
		 }
         else if(Input.GetKey(KeyCode.DownArrow)) {
             // transform.position -= transform.forward * Time.deltaTime * movementSpeed;
             //  moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
             movementSpeed = -5.0f;
             //transform.position += moveDirection * Time.deltaTime * movementSpeed;
         }else if(Input.GetKeyUp(KeyCode.DownArrow)){
			movementSpeed = 0.0f;
		 }
		 if(Input.GetKey(KeyCode.LeftArrow)) {
             // transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
             // transform.Rotate(Vector3.up * (-turnSpeed) * Time.deltaTime, Space.World);
             transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);
             // transform.rotation.eulerAngles.y = new Vector3(0.0f, Time.deltaTime*turnSpeed, 0.0f);
             Debug.Log("Left pressed, moving direction is: "+ moveDirection);

         }
         else if(Input.GetKey(KeyCode.RightArrow)) {
              // transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);
              // transform.Rotate(Vector3.up * (turnSpeed) * Time.deltaTime, Space.World);
              transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
              // transform.rotation.eulerAngles.y = new Vector3(0.0f, Time.deltaTime*turnSpeed, 0.0f);
              Debug.Log("Right pressed, moving direction is: "+ moveDirection);

         }else{}

        //controller.Move(moveDirection * movementSpeed * Time.deltaTime);
		transform.position += moveDirection * Time.deltaTime * movementSpeed;
	}
}



