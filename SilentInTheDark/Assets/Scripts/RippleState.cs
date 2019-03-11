using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleState : MonoBehaviour
{
    public float speed;

    public Material material;

    private Vector4 rippleOrigin = Vector4.zero;
    public Vector3 RippleOrigin {  set { rippleOrigin = new Vector4( value.x, value.y, value.z, 0 ); } }

    // Update is called once per frame
    void Update()
    {
        rippleOrigin.w = Mathf.Min( rippleOrigin.w + ( Time.deltaTime * speed ), 1 );
        material.SetVector( "_RippleOrigin", rippleOrigin );
    }
}
