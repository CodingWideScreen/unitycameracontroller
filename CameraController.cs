using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float turnSpeed = 4.0f;
    public Transform objectToOrbit;
    private Vector3 offset;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    GameObject rayerCaster;
     void Start () {
        rayerCaster = GameObject.Find("RayCaster");
		offset = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        initialRotation = new Quaternion(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z, transform.localRotation.w);
        initialPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
     }

    void FixedUpdate(){
        Vector3 fwd = rayerCaster.transform.TransformDirection(Vector3.forward);//Get our forward dir
        Debug.DrawLine(rayerCaster.transform.position, fwd * 50000000, Color.red);//Points towards object based on LateUpdate code.
       
        
    }
    private void OnTriggerEnter(Collider other)
	{
          Debug.Log("In tag type: " + other.gameObject.tag);
        if(other.gameObject.tag =="Terrain"){
             //We now need to use the ray cast to check if we are clipping and essentially move in the opposite direction until no contact
            Debug.Log("In terrain type: " + other.gameObject.name);
        }

    }
     void LateUpdate()
     {

         //Keep camera from flipping with car.
		if (Input.GetKey (KeyCode.LeftControl)) {	
			// offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = objectToOrbit.localPosition + offset; 
			transform.LookAt(objectToOrbit.localPosition);

		}else{
            transform.localRotation = initialRotation;
            transform.localPosition = initialPosition;     
        }

        transform.rotation = Quaternion.LookRotation(objectToOrbit.position - transform.position, Vector3.up); //Keep our position on top instead of flipping with object
        
     
     }
}
