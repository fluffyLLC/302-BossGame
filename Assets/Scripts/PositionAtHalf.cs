using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PositionAtHalf : MonoBehaviour
{

    public Transform tOne;
    public Transform tTwo;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (tOne != null && tTwo != null)
        {
            Vector3 tOneToTTwo =  tTwo.position - tOne.position;
            Vector3 newPos = (Vector3.Normalize(tOneToTTwo) * (tOneToTTwo.magnitude/2)) + tOne.position;
            transform.position = newPos;
        }
    }
}
