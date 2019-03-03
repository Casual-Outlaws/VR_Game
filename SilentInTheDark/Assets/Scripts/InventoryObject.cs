using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public GameObject heldObject;

    void OnTriggerEnter(Collider other)
    {
        heldObject = other.gameObject;
        other.GetComponent<Rigidbody>().useGravity = false;
    }

    void OnTriggerExit(Collider other)
    {
        heldObject = null;
        other.GetComponent<Rigidbody>().useGravity = true;
    }

    void Update()
    {
        heldObject.gameObject.transform.position = this.transform.position;
    }
}
