using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlWaveHands : MonoBehaviour
{
    // Start is called before the first frame update
	private Animator animation_controller;
	public GameObject car;
	public GameObject destinationHouse;
	//private Vector3 dist;
    void Start()
    {
        animation_controller = GetComponent<Animator>();
		car = GameObject.Find("CarObj");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(car.transform.position, destinationHouse.transform.position);
		// Debug.Log(car.GetComponent<Car>().hasDelivered);
		if(car.GetComponent<Car>().hasDelivered){
			animation_controller.SetBool("isCongrats", true);
			
		}else if(dist<100.0f){
			animation_controller.SetBool("isWaving", true);
		}else{}
		
    }
}
