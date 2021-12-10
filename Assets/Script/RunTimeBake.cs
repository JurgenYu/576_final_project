using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RunTimeBake : MonoBehaviour
{
    // Start is called before the first frame update
    private static NavMeshSurface surface;//NavMeshSurface组件

    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    public void Bake()
    {
        surface.BuildNavMesh();
    }

}
