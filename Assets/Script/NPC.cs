using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Buildings;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> houses;
    private NavMeshAgent agt;
    private GameObject CurrentTarget;
    void Start()
    {
        houses = new List<GameObject>();
        agt = GetComponent<NavMeshAgent>();
        CurrentTarget = null;
    }

    private GameObject findNearestWithPackage()
    {
        GameObject nearest = null;
        float minDist = float.MaxValue;
        foreach (GameObject h in houses)
        {
            if (h.GetComponent<House>().HasPackage())
            {
                if (nearest == null || Vector3.Distance(h.transform.position, transform.position) < minDist)
                {
                    nearest = h;
                    minDist = Vector3.Distance(h.transform.position, transform.position);
                }
            }
        }
        return nearest;
    }

    // Update is called once per frame
    void Update()
    {
        if (houses.Count == 0)
        {
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name.Contains("HOUSE"))
                {
                    houses.Add(gameObj);
                }
            }
            if (houses.Count != 0)
            {
                GameObject.Find("Terrain").GetComponent<RunTimeBake>().Bake();
            }
            // Debug.Log(houses.Count);
        }
        if (!CurrentTarget)
        {
            CurrentTarget = findNearestWithPackage();
            if (CurrentTarget)
            {
                Debug.Log("New House: " + CurrentTarget.name);
                agt.SetDestination(CurrentTarget.transform.position);
            }
        }
        else
        {
            if (Vector3.Distance(CurrentTarget.transform.position, transform.position) < 10.0f)
            {
                this.CurrentTarget.GetComponent<House>().Clear();
                CurrentTarget = null;
            }
        }
    }
}
