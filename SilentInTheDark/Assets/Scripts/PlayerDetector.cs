using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Animator enemyModel;
    public GameObject GaneEndText;

    private void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == "Player" )
        {
            transform.LookAt( other.gameObject.transform );
            if( GameManager.Instance.gameState != GameState.LostGame )
            {
                GameManager.Instance.gameState = GameState.LostGame;
                enemyModel.SetTrigger( "attackPlayer" );
                GaneEndText.SetActive( true );
            }
        }
    }
}
