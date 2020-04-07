using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCycleTest : MonoBehaviour
{
    public Transform Hips;
    float LegLength = 1000;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = FindPointUnderHips();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Hips.position, Vector3.down, new Color(255, 0, 0,255), 100000f, false);
    }


    void CalcGate() { 
    
    
    }

    //TODO: make this relaitce to the body
    Vector3 FindPointUnderHips() {
        
        RaycastHit hit = RaycastFromHips();
        if (hit.distance > LegLength)
        {
            return transform.position;
        }
        else {
            return hit.point;
        }

    }

    RaycastHit RaycastFromHips(Vector3 direction) {
        if (Hips == null) {
            print("ERROR: hips not set");
            return new RaycastHit();
        }
        Ray FromHips = new Ray(Hips.position, direction);
        Debug.DrawRay(Hips.position, direction, new Color(255,0,0), 100000f,false);
        Physics.Raycast(FromHips, out RaycastHit hit);
        print(hit);
        return hit;
    }

    RaycastHit RaycastFromHips()
    {
        return RaycastFromHips(Vector3.down);
    }



    public float WalkCurve(float x)
    {
        float y = Mathf.Sin(x * Mathf.PI);

        return WalkCurve(x, y);
    }
    public float WalkCurve(float x,float y) {


        return Mathf.Sin(x * Mathf.PI - y);
    }

   
}
