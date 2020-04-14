using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCycleTest : MonoBehaviour
{

    public Transform sholder;
    public Transform elbow;
    public float swingOffset;
    float width;

    float height;

    /// <summary>
    /// shifts the location of the swing around the unit circle
    /// </summary>
    public float unitShift = -1f;

    public float armExtension = .01f;


    // Start is called before the first frame update
    void Start()
    {

        CalcHeightWidth(1,1);



        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = CalcPosition();
        transform.eulerAngles = CalcAngle();
    }

    void CalcHeightWidth(float cos, float sin ) {

        float dist = Vector3.Distance(sholder.position, elbow.position);


        height = (armExtension * cos) + dist;


        width = (armExtension * sin) + dist;



            //armExtension + Vector3.Distance(sholder.position, elbow.position);
    }

    Vector3 CalcPosition() {
        float g = Mathf.Sin((Time.time * Mathf.PI) + swingOffset + Mathf.PI);

        float cos = Mathf.Cos(unitShift + g);
        float sin = Mathf.Sin(unitShift + g);

        CalcHeightWidth(cos,sin);

        
        float y = (height* sin) + sholder.position.y;
        float z = (width * cos) + sholder.position.z;

        return new Vector3(transform.position.x, y, z);
    }
    

    Vector3 CalcAngle() {

        Vector3 myLeftNut = sholder.position - transform.position;

        //myLeftNut d



        float ang = Mathf.Atan2(myLeftNut.y, myLeftNut.z); //Vector3.Angle(elbow.position, transform.position);

        print(Mathf.Rad2Deg * ang);

        return new Vector3((-Mathf.Rad2Deg*ang) + 30, 0, 0);
    }
}
