using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamController : MonoBehaviour
{
    public LayerMask layer;
    public float raycastDistance = 0.4f;

    [HideInInspector]
    public RaycastHit hit;
    
  
    [HideInInspector]
    public bool isMove = false;

    [HideInInspector]
    public bool isHitPiece=false;

    [HideInInspector]
    public char rotateDirection;

    [HideInInspector]
    public bool imStillTurn = false;

    private Rigidbody rb;
    [SerializeField]
    private float speed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
      if (!imStillTurn)
        {
            raycastController();
        }
      
    }

    private void FixedUpdate()
    {
            Move();
    }
    public void raycastController()
      {
          
          Ray ray = new Ray(transform.position, transform.forward);
          RaycastHit hit;

        
          Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);
 
          if (Physics.Raycast(ray, out hit, raycastDistance, layer))
          {
           
            if (hit.collider.gameObject.tag == "Piece")
            {
                isMove = false;
                isHitPiece = true;
                rotateDirection = 'f';
              
            }
           
             
              Debug.Log("Collider'a çarpıldı: " + hit.collider.name);
            
          }
          else
          {
           
            // Collider'a çarpmadı.
            Debug.Log("Collider'a çarpmadı.");
          }

      }

    public void TurnandMove(char rotate_Direction)
    {
        if (isMove == false)
        {
            if (rotate_Direction == 'r')
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (rotate_Direction == 'l')
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (rotate_Direction == 'u')
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (rotate_Direction == 'd')
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            
            isMove = true;

        }

        imStillTurn = false;

    }

    public void Move()
    {
        if (isMove)
        {
          
            rb.velocity = transform.forward * speed;
        }
        else
        {
            
            rb.velocity = Vector3.zero;
        }
    }

   
}

