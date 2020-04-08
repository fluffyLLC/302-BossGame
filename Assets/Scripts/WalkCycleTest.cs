using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCycleTest : MonoBehaviour
{
    public Transform Hips;
    Vector3 startPosition;
    float LegLength = 1000;
    float footOffset = 0.75f;
    public float gate = 15;
    public float treadOffset = 8;
    public float height = 5;
    public float stepOffset = 0;
    



    // Start is called before the first frame update
    void Start()
    {
        startPosition = FindPointUnderHips();
        print(startPosition);
        startPosition = new Vector3(startPosition.x,startPosition.y + footOffset, startPosition.z);
        print(startPosition);
        transform.position = startPosition;
        //Vector3.down

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( startPosition.x, CalcFootLift(), CalcFootSlide());
        //Debug.DrawRay(Hips.position, Hips.up*10000 /*Vector3.down*10000*/, new Color(255, 0, 0,255), 1f, false);
    }


    //TODO:Animate baised on ray casts
    void CalcGate() { 
    
    
    }

    float CalcFootSlide() {
        //print(Mathf.Cos(Time.time) * (startPosition.z + gate));
        return  ((startPosition.z - treadOffset) + (Mathf.Cos(stepOffset + Time.time * Mathf.PI) * gate));
    }

    float CalcFootLift() {
        float mult = WalkCurve(stepOffset+1 + Time.time);
        if (mult < 0) {
            mult = 0;
        }
        return startPosition.y + (mult * height);
    }

    //float CalcWalk

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
        Debug.DrawRay(Hips.position, direction*1000, new Color(255,0,0), 100000f,false);
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
