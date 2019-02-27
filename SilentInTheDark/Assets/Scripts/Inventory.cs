using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int numOfKeys = 0;
    int numOfMasterKeys = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKey( bool isMasterKey )
    {
        if( isMasterKey )
            numOfMasterKeys += 1;
        else
            numOfKeys += 1;
    }

    public bool HasKey( bool isMasterKey )
    {
        if( isMasterKey )
            return numOfMasterKeys > 0;
        else
            return numOfKeys > 0;
    }

    public void DecreaseKey( bool isMasterKey )
    {
        if( isMasterKey )
        {
            if( numOfMasterKeys <= 0 )
                throw new System.Exception( "Has no master key." );
            numOfMasterKeys -= 1;
        }
        else
        {
            if( numOfKeys <= 0 )
                throw new System.Exception( "Has no door key." );
            numOfKeys -= 1;
        }
    }
}
