using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    List<ISoundListener> eventListeners;

    public static EventManager Instance = null;

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else if( Instance != this )
        {
            UnityEngine.Object.Destroy( gameObject );
        }

        DontDestroyOnLoad( gameObject );

        eventListeners = new List<ISoundListener>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterEventListener( ISoundListener observer )
    {
        eventListeners.Add( observer );
    }

    public void UnregisterEventListener( ISoundListener observer )
    {
        eventListeners.Remove( observer );
    }

    public void NotifyObservers( RoomEvent eventType, Vector3 location )
    {
        foreach( var observer in eventListeners )
        {
            observer.HeardSound( eventType, location );
        }
    }
}
