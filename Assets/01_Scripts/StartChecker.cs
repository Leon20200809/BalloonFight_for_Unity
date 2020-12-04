using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    MoveObject moveObject;

    // Start is called before the first frame update
    void Start()
    {
        moveObject = GetComponent<MoveObject>();
    }

    public void SetInitialSpeed()
    {
        moveObject.moveSpeed = 0.005f;
    }
}
