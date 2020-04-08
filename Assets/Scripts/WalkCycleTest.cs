using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCycleTest : MonoBehaviour
{
    /// <summary>
    /// The transform of the 'hip' bone associated with this leg
    /// </summary>
    public Transform Hips;

    /// <summary>
    /// This is set via raycast, this should be the closest point on the ground directly underneath the `Hips`
    /// </summary>
    Vector3 startPosition;

    /// <summary>
    /// How long are the legs? this values is used to check if we can reach th ground
    /// </summary>
    float LegLength = 1000;

    /// <summary>
    /// How high above the ground should the foot be positioned in order to ensure a correct apperence
    /// </summary>
    float footHeightOffset = 0.75f;

    /// <summary>
    /// This is half the leangth of a single step
    /// </summary>
    public float gate = 15;

   /// <summary>
   /// This shifts the center of the player's tread behind the body (positive values backwards, negative values forwards) 
   /// </summary>
    public float treadOffset = 8;

    /// <summary>
    /// This is the max height of a given step
    /// </summary>
    public float height = 5;

    /// <summary>
    /// This is used to offset feet from one another
    /// </summary>
    public float stepOffset = 0;
    



    // Start is called before the first frame update
    void Start()
    {
        startPosition = FindPointUnderHips(); //calc start postion
        //print(startPosition);
        startPosition = new Vector3(startPosition.x,startPosition.y + footHeightOffset, startPosition.z); // apply foot offset
        //print(startPosition);
        transform.position = startPosition; //start the foot at the start position
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
    //TODO: Set walk paramiters here based on relative position of the ground
    
    }

    /// <summary>
    /// This function calculates the z position of the foot baised on a starting position
    /// </summary>
    /// <returns>The z position of the foot baised on it's starting position</returns>
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

    /// <summary>
    /// This function gets the closest point on the ground beneath the hips.  
    /// </summary>
    /// <returns>The closest point on the ground beneath the hips. If the hip is not set, the function will return (0,0,0)</returns>
    Vector3 FindPointUnderHips() {
        //TODO: make this relative to the body

        RaycastHit hit = RaycastFromHips();
        if (hit.distance > LegLength)
        {
            return transform.position;
        }
        else {
            return hit.point;
        }

    }

    /// <summary>
    /// This casts a ray away from the hips in a given direction
    /// </summary>
    /// <param name="direction">The direction the ray should cast in</param>
    /// <returns>The RaycastHit returned by this raycast, if the hip is not set the function will return an empty raycast hit object </returns>
    RaycastHit RaycastFromHips(Vector3 direction, bool shouldDrawRay = true) {
        //TODO: make this relative to the body
        if (Hips == null) {
            print("ERROR: hips not set");
            return new RaycastHit();
        }
        Ray FromHips = new Ray(Hips.position, direction);
        Physics.Raycast(FromHips, out RaycastHit hit);

        if (shouldDrawRay)
        {
            Debug.DrawRay(Hips.position, direction * 1000, new Color(255, 0, 0), 100000f, false);
            print(hit);
        }

        return hit;
    }

    /// <summary>
    /// This casts a ray underneath the hips given a direction
    /// </summary>
    /// <returns>The RaycastHit returned by this raycast, if the hip is not set the function will return an empty raycast hit object</returns>
    RaycastHit RaycastFromHips()
    {
        return RaycastFromHips(Vector3.down);
    }


    /// <summary>
    /// Modified sin wave for the height of walk cycle
    /// </summary>
    /// <param name="x">The x value to be passed in (time is expected)</param>
    /// <returns>The value at x</returns>
    public float WalkCurve(float x)
    {
        float y = Mathf.Sin(x * Mathf.PI);

        return WalkCurve(x, y);
    }

    /// <summary>
    /// Modified sin wave for the height of walk cycle
    /// </summary>
    /// <param name="x">the x value to be passed in (time is expected)</param>
    /// <param name="y">the y value to be passed in</param>
    /// <returns>The value at x and y</returns>
    public float WalkCurve(float x,float y) {


        return Mathf.Sin(x * Mathf.PI - y);
    }

   
}
