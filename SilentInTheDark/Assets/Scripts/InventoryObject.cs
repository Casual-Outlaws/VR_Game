using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public GameObject heldObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HoldableObject" && !heldObject)
        {
            heldObject = other.gameObject;
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HoldableObject" && heldObject == other.gameObject)
        {
            heldObject = null;
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void Update()
    {
        heldObject.gameObject.transform.position = this.transform.position;
    }
}
