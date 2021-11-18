using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update、private Animator animation_controller;
    private CharacterController character_controller;
	//private Animator animation_controller;
	public GameObject car;
    public Vector3 movement_direction;
	
    public float walking_velocity;   
    public float velocity;
	public float rotationSpeed = 10.0f;
    void Start()
    {
        //animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 2.0f;
        velocity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);
		character_controller.Move(movement_direction * velocity * Time.deltaTime);
		//if (character_controller.isGrounded){
		if(Input.GetKey(KeyCode.UpArrow)){
			velocity = 3.0f;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			velocity = -3.0f;
		}else if(Input.GetKeyUp(KeyCode.UpArrow)){
			velocity = 0;
		}else if(Input.GetKeyUp(KeyCode.DownArrow)){
			velocity = 0;
		}else{}
    }
}
