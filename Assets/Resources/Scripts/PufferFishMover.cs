using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishMover : MonoBehaviour
{


    public LayerMask layer;
    public Transform[] targetPos;
    public int currentCellIndex = 0;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private bool isMove = true;
    private Rigidbody rb;
  

    private void OnEnable()
    {
        GameManager.ActionGameStart += SetPufferMoverFishPosition;
    }

    private void Start()
    {
       
        rb = GetComponent<Rigidbody>();
    }
    private void SetPufferMoverFishPosition()
    {
        transform.position = new Vector3(targetPos[0].transform.position.x, 3.8f, targetPos[0].transform.position.z);
    }
    private void Update()
    {
        if (PlayerController.Instance.isPlayerDeath)
        {

            gameObject.tag = "Untagged";
            isMove = false;
        }
    }
    private void FixedUpdate()
    {
        if (isMove)
        {

            PufferMove();
        }
    }
  
    public void PufferMove()
    {
       
        Vector3 direction = (targetPos[currentCellIndex].transform.position - rb.position).normalized;

        float distance = Vector3.Distance(rb.position, targetPos[currentCellIndex].transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(-direction);

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    
        transform.rotation = targetRotation;
       
     
        if (distance < 0.1f )
        {
            currentCellIndex++;
         
            if (currentCellIndex == targetPos.Length)
            {
                currentCellIndex = 0;
            }
        }

    }
  
    private void OnDrawGizmos()
    {
        if (targetPos.Length >1)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(targetPos[currentCellIndex].transform.position, new Vector3(targetPos[currentCellIndex].transform.localScale.x, targetPos[currentCellIndex].transform.localScale.y + 6.4f, targetPos[currentCellIndex].transform.localScale.z));
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
        GameManager.ActionGameStart -= SetPufferMoverFishPosition;
    }

}
