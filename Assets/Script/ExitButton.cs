using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject ExitPanel;
	public GameObject car;
	public Camera mainCamera;
	public Camera cameraForCar;
	public bool hasGameEnd;
    void Start()
    {
        car = GameObject.Find("CarObj");
		
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(car.GetComponent<Car>().hasTimeUsed);
		if(car.GetComponent<Car>().hasTimeUsed){
			mainCamera.enabled = true;
			cameraForCar.enabled = false;
		}
		ExitPanel.gameObject.SetActive (true);
    }
	public void onClickExitButton(){
		Debug.Log("Exit Game");
		Application.Quit();
		
	}
}
