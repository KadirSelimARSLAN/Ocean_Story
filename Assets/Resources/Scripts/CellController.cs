using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public enum cellActivity
    {
        doNothing,
        setPlayer,
      
    }

    public cellActivity cell_Activity;

    public GameObject Player;
    

    private void OnEnable()
    {
       
        GameManager.ActionGameStart += SetPlayer;
    }

    private void SetPlayer()
    {
        if (cellActivity.setPlayer == cell_Activity)
        {
           
            Vector3 globalLoc = new Vector3(gameObject.transform.position.x,3.79f,gameObject.transform.position.z);
          
           // PlayerController.Instance.SetPlayerPosition(globalLoc);

            GameObject _Player = Instantiate(Player,globalLoc,Quaternion.identity);
        }
      

    }

    private void OnDisable()
    {
        GameManager.ActionGameStart -= SetPlayer;
    }

    #region Editor
    private void OnDrawGizmos()
    {
        if(cellActivity.setPlayer == cell_Activity)
        {
           Gizmos.color =  Color.green;
            Gizmos.DrawCube(transform.position,new Vector3(transform.localScale.x, transform.localScale.y+6.4f, transform.localScale.z));
        }
    }
    #endregion
}
