using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Animator enemyModel;
    public TextMesh GaneEndText;

    private void Start()
    {
        GaneEndText.text = string.Empty;
    }

    private void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == "Player" )
        {
            transform.LookAt( other.gameObject.transform );
            if( GameManager.Instance.gameState != GameState.LostGame )
            {
                GameManager.Instance.gameState = GameState.LostGame;
                enemyModel.SetTrigger( "attackPlayer" );
                GaneEndText.text = "You DIE!";
                Debug.Log( "You DIE!" );

                gameObject.GetComponentInParent<SimpleEnemyAI>().PlaySound( EnemySoundType.Attack );
            }
        }
    }
}
