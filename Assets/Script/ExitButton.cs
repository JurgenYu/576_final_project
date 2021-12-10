using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject ExitPanel;
	public GameObject car;
	public Camera mainCamera;
	public Camera cameraForCar;
	public bool hasGameEnd;
	public GameObject PausePanel;
	public bool isPauseClicked;
	
    void Start()
    {
        car = GameObject.Find("CarObj");
		hasGameEnd = false;
		isPauseClicked = false;
		
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hasTimeUsed"+ " "+ car.GetComponent<Car>().hasTimeUsed);
		Debug.Log("isHealthZero" + " " + car.GetComponent<Car>().isHealthZero);
		if(car.GetComponent<Car>().hasTimeUsed || car.GetComponent<Car>().isHealthZero){
			mainCamera.enabled = true;
			cameraForCar.enabled = false;
			hasGameEnd = true;
		}
		ExitPanel.gameObject.SetActive (hasGameEnd);
		PausePanel.gameObject.SetActive(isPauseClicked);
    }
	public void onClickExitButton(){
		Debug.Log("Exit Game");
		Application.Quit();
		
	}
	public void onClickPlayAgain(string sceneName){
		SceneManager.LoadScene (sceneName);
	}
	public void pauseGame(){
		Time.timeScale = 0;
		isPauseClicked = true;
	}
	public void continueGame(){
		Time.timeScale = 1;
		isPauseClicked = false;
	}
}
