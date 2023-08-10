using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windzone3d : MonoBehaviour
{

    public Vector3 Direction;
    public float force = 5;

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Ball")
        {
            other.GetComponent<Rigidbody>().AddForce(Direction * force);
        }
    }
}
