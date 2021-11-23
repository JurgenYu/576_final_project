using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update、private Animator animation_controller;
    //private CharacterController controller;
	//private Animator animation_controller;
	public GameObject car;
    public Vector3 moveDirection;
	public float turnSpeed = 10.0f;
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
        movementSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        ////float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        //movement_direction = new Vector3(xdirection, 0.0f, zdirection);
		//character_controller.Move(movement_direction * velocity * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.UpArrow)) {
             transform.position += transform.forward * Time.deltaTime * movementSpeed;
         }
         else if(Input.GetKey(KeyCode.DownArrow)) {
             transform.position -= transform.forward * Time.deltaTime * movementSpeed;
         }else{}
		 if(Input.GetKey(KeyCode.LeftArrow)) {
             transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
         }
         else if(Input.GetKey(KeyCode.RightArrow)) {
             transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);
         }
	}
}

