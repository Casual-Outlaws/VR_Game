using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHighlight : MonoBehaviour
{
    public SphereCollider thisCollider;
    [SerializeField] bool onItem, onEnemy, onPlayer, enemyHit;
    [SerializeField] GameObject enemyUpstairs, enemyDownstarirs;

    Target targetScript;

    void Start()
    {
        targetScript = GameObject.Find("Target").GetComponent<Target>();
        thisCollider = GetComponent<SphereCollider>();
        enemyHit = false;

        if (onItem)
            thisCollider.radius = 3;

        if (onEnemy)
            thisCollider.radius = 2;

        if (onPlayer)
            thisCollider.radius = 3;
    }

    private void Update()
    {
        if (enemyHit == false && onItem || onPlayer)
        {
            StartCoroutine("reactionCheck");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("ItemS"))
        {
            ItemsUsable script = col.gameObject.GetComponent<ItemsUsable>();
            //script.outline.SetActive(true);
            print(script.name);
        }

        if (col.gameObject.tag == ("ItemD"))
        {

        }

        if (col.gameObject.tag == ("Door"))
        {
            print(col.gameObject.name);
        }

        if (col.gameObject.tag == ("Enemy") && onItem || onPlayer)
        {
            print(targetScript.name);
            targetScript.targetPos.transform.position = this.transform.position;
            targetScript.forceChange = true;
            enemyHit = true;
        }

        else
            enemyHit = false;
    }

    IEnumerator reactionCheck()
    {
        if (enemyHit == false)
        {
            float distanceCheck, reactionLimit;
            float coin = Random.Range(0f, 10f);
            distanceCheck = Vector3.Distance(this.gameObject.transform.position, enemyUpstairs.transform.position);

            if (distanceCheck >= thisCollider.radius && distanceCheck < 5)
            {
                reactionLimit = 4;
                if (coin >= reactionLimit)
                {
                    targetScript.targetPos.transform.position = this.transform.position;
                    targetScript.forceChange = true;
                }
            }

            else if (distanceCheck >= 5 && distanceCheck < 8f)
            {
                reactionLimit = 6.5f;
                if (coin >= reactionLimit)
                {
                    targetScript.targetPos.transform.position = this.transform.position;
                    targetScript.forceChange = true;
                }
            }

            else if (distanceCheck >= 8 && distanceCheck <= 10)
            {
                reactionLimit = 9f;
                if (coin >= reactionLimit)
                {
                    targetScript.targetPos.transform.position = this.transform.position;
                    targetScript.forceChange = true;
                }
            }
        }
        enemyHit = true;
        yield return null;
    }
}
