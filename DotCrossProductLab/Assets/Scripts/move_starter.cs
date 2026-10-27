using UnityEngine;

// Movement for Chibi

public class move_starter : MonoBehaviour 
{
    // chibi speed
    public float speed = 10.0f;
    // chibi rotation speed
    public float rotationSpeed = 200.0f;
    // Public GameObject to store the seekable object in
    public GameObject seek_me;

    Vector3 Cross(Vector3 v, Vector3 w) 
    {

        Vector3 crossProd = new Vector3(0, 0, 0);

        // TODO: Change the code below to do a cross product for v and w
        crossProd = new Vector3
            (
            v.y * w.z - v.z * w.y,
             v.z * w.x - v.x * w.z,
             v.x * w.y - v.y * w.x
            );


        // Use Unity method calculate cross product for comparison
        Vector3 UnityCrossProd = Vector3.Cross(v, w);

        Debug.Log($"My cross product {crossProd} unity cross product {UnityCrossProd}");

        return crossProd;
    }

    // Calculate the vector to the seek object
    float CalculateAngle()
    {

        // Chibi foward facing vector
        Vector3 tF = this.transform.forward;
        // Vector to the seek object
        Vector3 sD = seek_me.transform.position - this.transform.position;

        float dot = 0.0f;   // store the dot product
        float angle = 0.0f; // angle var to return

        // TODO: Calculate the dot product of tF and sD
        dot = tF.x * sD.x + tF.y * sD.y + tF.z * sD.z;
        // TODO: Calculate the angle between the two items - be careful it is in degs and not rads
        angle = 0f;
        float magProduct = tF.magnitude * sD.magnitude;
        if (magProduct > 0f)
        {
            float cos = Mathf.Clamp(dot / magProduct, -1f, 1f);
            angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
        }

        // Output the angles to the console - these should be the same
        Debug.Log("Angle: " + angle);
        // Output Unitys angle
        Debug.Log("Unity Angle: " + Vector3.Angle(tF, sD));

        // Draw a ray showing the chibi's forward facing vector
        Debug.DrawRay(this.transform.position, tF * 10.0f, Color.green, 2.0f);
        // Draw a ray showing the vector to the saught object
        Debug.DrawRay(this.transform.position, sD, Color.red, 2.0f);

        return angle;
    }

    // Calculate the distance from the chibi to whatever it is finding
    void CalculateDistance()
    {

        // Chibi position
        Vector3 tP = this.transform.position;
        // Seeking object position
        Vector3 sP = seek_me.transform.position;

        float distance = 0.0f;

        // TODO: Calculate the distance between the objects tP and sP using pythagoras
        distance = Mathf.Sqrt((sP.x - tP.x) * (sP.x - tP.x) + (sP.y - tP.y) * (sP.y - tP.y) + (sP.z - tP.z) * (sP.z - tP.z));

        // Calculate and compare your calculation with the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(tP, sP);

        // Print out the two results to the console - they should be the same
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        // Check for the spacebar being pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call Cross to calculate the cross product of the object's forward and right vectors
            // What is the result?  What vector does it correspond to?
            Cross(transform.forward, transform.right);

            // Call CalculateDistance method
            CalculateDistance();

            // Call CalculateAngle method
            CalculateAngle();
        }

        // Check for the T key being pressed
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            // Call CalculateAngle method
            float angle_to_turn = CalculateAngle();
            this.transform.Rotate(0, angle_to_turn,0);
        }
    }
}