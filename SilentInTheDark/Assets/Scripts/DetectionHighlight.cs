using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHighlight : MonoBehaviour
{
    public SphereCollider thisCollider;
    [SerializeField] bool onItem, onEnemy, onPlayer;


    void Start()
    {
        thisCollider = GetComponent<SphereCollider>();

        if (onItem)
            thisCollider.radius = 3;

        if (onEnemy)
            thisCollider.radius = 2;

        if (onPlayer)
            thisCollider.radius = 3;
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
            Target script = col.gameObject.GetComponent<Target>();
            //script.outline.SetActive(true);
            print(script.name);
            script.targetPos.transform.position = this.transform.position;
            script.forceChange = true;
        }
    }
}
