using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    TextMesh textInfo;

    private void Awake()
    {
        textInfo = GetComponentInChildren<TextMesh>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if( textInfo != null )
        {
            textInfo.text = string.Empty;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter( Collider other )
    {
        if( other.transform.root.tag == "Player" )
        {
            textInfo.text = "You Win!";
            GameManager.Instance.gameState = GameState.WonGame;
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if( other.transform.root.tag == "Player" )
        {
            textInfo.text = string.Empty;
        }
    }
}
