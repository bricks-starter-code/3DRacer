using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody CarRigidBody;
    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        CarRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = 1 << 8; //TrackLayer
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log(hit.transform.gameObject.name);

            var myForward = hit.transform.TransformDirection(Vector3.up);
            CarRigidBody.MovePosition(transform.position + myForward * Time.fixedDeltaTime * speed);

            Quaternion myRotation = transform.rotation;
            Quaternion trackRotation = hit.transform.rotation;
            Quaternion neededRotation = Quaternion.Inverse(trackRotation) * myRotation; //From https://answers.unity.com/questions/35541/problem-finding-relative-rotation-from-one-quatern.html
            
            //CarRigidBody.MoveRotation(transform.rotation * neededRotation );
            CarRigidBody.MoveRotation(Quaternion.Lerp(transform.rotation, trackRotation, .1f));


        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
