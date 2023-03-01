using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleColor : MonoBehaviour
{
    public Material unactive;
    public Material active;
    public GameObject ball;

    void OnTriggerEnter(Collider other)
    {
        ball.GetComponent<MeshRenderer>().material = active;
    }
    void OnTriggerExit(Collider other)
    {
        ball.GetComponent<MeshRenderer>().material = unactive;
    }

}
