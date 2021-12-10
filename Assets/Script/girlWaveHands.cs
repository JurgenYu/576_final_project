using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlWaveHands : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animation_controller;
    public GameObject car;
    public GameObject destinationHouse;

    private int NextUpdate;
    //private Vector3 dist;
    void Start()
    {
		NextUpdate = 0;
        animation_controller = GetComponent<Animator>();
        car = GameObject.Find("CarObj");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(car.transform.position, destinationHouse.transform.position);
        // Debug.Log(car.GetComponent<Car>().hasDelivered);
        if (Time.time > NextUpdate)
        {
			animation_controller.SetBool("isCongrats", false);
			animation_controller.SetBool("isWaving", true);

        }
        else if (dist < 100.0f)
        {
            animation_controller.SetBool("isWaving", true);
        }

    }

    public void Congrate()
    {
        NextUpdate = Mathf.FloorToInt(Time.time + 5);
        animation_controller.SetBool("isCongrats", true);
		animation_controller.SetBool("isWaving", false);
    }
}
