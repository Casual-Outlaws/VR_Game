using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public GameObject heldObject;
    GameObject otherObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ItemD" && !heldObject)
        {
            heldObject = other.gameObject;
            other.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (other.gameObject.tag == "ItemD" && heldObject)
        {
            Physics.IgnoreCollision(otherObject.GetComponent<Collider>(), heldObject.GetComponent<Collider>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ItemD" && heldObject == other.gameObject)
        {
            heldObject = null;
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void Update()
    {
        if (heldObject)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            heldObject.gameObject.transform.position = this.transform.position;
            heldObject.gameObject.transform.rotation = this.transform.rotation;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;            
        }
    }
}
