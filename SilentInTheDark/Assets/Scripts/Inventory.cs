using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int numOfKeys = 0;
    int numOfLevelKeys = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKey( bool isLevelKey )
    {
        if( isLevelKey )
            numOfLevelKeys += 1;
        else
            numOfKeys += 1;
    }

    public bool HasKey( bool isLevelKey )
    {
        if( isLevelKey )
            return numOfLevelKeys > 0;
        else
            return numOfKeys > 0;
    }

    public void DecreaseKey( bool isLevelKey )
    {
        if( isLevelKey )
        {
            if( numOfLevelKeys <= 0 )
                throw new System.Exception( "Has no level key." );
            numOfLevelKeys -= 1;
        }
        else
        {
            if( numOfKeys <= 0 )
                throw new System.Exception( "Has no key." );
            numOfKeys -= 1;
        }
    }
}
