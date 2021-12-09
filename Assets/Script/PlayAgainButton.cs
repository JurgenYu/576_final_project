using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    Button playAgain; 
    GameObject gameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        playAgain = GameObject.Find("Canvas/WinPanel/PlayAgainButton").GetComponent<Button>();
        gameCanvas = GameObject.Find("Canvas");
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void playAgainButtonClicked(){
        // gameCanvas.SetActive(false);

    }
}
