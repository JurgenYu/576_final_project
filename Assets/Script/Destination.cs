using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    // Start is called before the first frame update
    private Level level;
    void Start()
    {
        level = GameObject.Find("Level").GetComponent<Level>();

    }

    void OntriggerEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Equals("CarObj"))
        {
            Debug.Log("Delivered");
            level.Delivered();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
