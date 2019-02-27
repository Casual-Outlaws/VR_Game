using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Key : MonoBehaviour
{
    [SerializeField]
    bool isLevelKey;

    [SerializeField]
    TextMesh keyInfoText;

    // Start is called before the first frame update
    void Start()
    {
        keyInfoText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin( Hand hand )
    {
        if( isLevelKey )
        {
            keyInfoText.text = "Master Key";
        }
        else
        {
            keyInfoText.text = "Door Key";
        }
    }


    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd( Hand hand )
    {
        keyInfoText.text = string.Empty;
    }
}
