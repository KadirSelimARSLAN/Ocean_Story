using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public LayerMask layer;
    [HideInInspector]
    public RaycastHit hit;


    public float raycastDistance = 0.4f;
    public bool isMove = false;
   
    public char rotateDirection;
    public bool imStillTurn = false;
    public bool isGameWin = false;
    public bool leftTheGround = false;
    public bool ontheGround = false;
    public bool isPlayerDeath = false;


    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private Animator animator;



    private void Start()
    {
       
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.SetBool("Move", false);
    }
    void Update()
    {

        if (!imStillTurn && isPlayerDeath == false)
        {
            raycastController();

        }

      
    }

    private void FixedUpdate()
    {
        if (isPlayerDeath == false)
        {
            if (leftTheGround)
            {
                animator.SetBool("Move", true);
                goDeep();

            }
            else
            {

                Move();
            }
        }
    
    }
    public void Death()
    {
   
        GameManager.ActionLevelLosed.Invoke();

    }

    public void SetPlayerPosition(Vector3 cellPos)
    {

        transform.position = cellPos;
    }

    #region Move
    public void TurnandMove(char rotate_Direction)
    {
        if (isMove == false  && ontheGround == true)
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
            animator.SetBool("Move", true);
            AudioManager.Instance.hitVoiceCount = 0;
        }
        else
        {
            
            rb.velocity = Vector3.zero;
        }
    }

    public void goDeep()
    {
        // rb.velocity = -transform.up *(speed * 0.8f);
      
    }

    public void callLevelPassedScreen()
    {
        GameManager.ActionLevelPassed.Invoke();
    }
    #endregion
    
    #region Detection
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
             
                rotateDirection = 'f';
                animator.SetBool("Move", false);

                AudioManager.Instance.playHitVoice();
            }

        }
   

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
        
      
            leftTheGround = true;
           
            rb.velocity = rb.velocity / 5;
            rb.AddForce(transform.forward * 6, ForceMode.Impulse);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {


            ontheGround = true;
           


        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if(isPlayerDeath != true)
            {
                isPlayerDeath = true;
                isMove = false;
                rb.velocity = Vector3.zero;
                Vector3 collisionNormal = collision.contacts[0].normal;
                rb.AddForce(collisionNormal * 80, ForceMode.Impulse);
                animator.SetBool("Move", false);
                AudioManager.Instance.playGameLose();

              
                Invoke("Death", 1f);
            }
       

        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ocean")
        {
            AudioManager.Instance.playDropOcean();
            isGameWin = true;
           
           Invoke("callLevelPassedScreen", 1f);

        }
       
    }
    #endregion
}
