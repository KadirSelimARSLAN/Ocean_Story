using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : MonoBehaviour
{

    public CellController cellPos;

    private void OnEnable()
    {
        GameManager.ActionGameStart += SetPosition; 
    }
    private void SetPosition()
    {
        Debug.Log(cellPos);
        transform.position = new Vector3(cellPos.transform.position.x, 3.8f, cellPos.transform.position.z);
    }
    private void Update()
    {
        if (PlayerController.Instance.isPlayerDeath)
        {

            gameObject.tag = "Untagged";
        }
    }

    private void OnDrawGizmos()
    {
        if(cellPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(cellPos.transform.position, new Vector3(cellPos.transform.localScale.x, cellPos.transform.localScale.y + 6.4f, cellPos.transform.localScale.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ocean")
        {
            Destroy(this.gameObject);

        }
    }

    private void OnDisable()
    {
        GameManager.ActionGameStart -= SetPosition;
    }


}
