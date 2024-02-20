using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchController : Singleton<TouchController> , IPointerDownHandler , IPointerUpHandler
{
    private Vector2 origin;
    private Vector2 direction;
    private bool touched = false;
    private int pointerID;


    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        touched = false;
        direction = Vector2.zero;

    }

    private void Update()
    {
        direction = Vector2.zero;

    }


    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            origin = data.position;
            pointerID = data.pointerId;


        }

    }
    
    public void OnPointerUp(PointerEventData data)
    {if(PlayerController.Instance.isPlayerDeath == false)
        {
            if (pointerID == data.pointerId)
            {
                Vector2 currentPosition = data.position;
                direction = currentPosition - origin;
                touched = false;

                float absX = Mathf.Abs(direction.x);
                float absY = Mathf.Abs(direction.y);

                if (absX > absY)
                {
                    if (direction.x > 0)
                    {

                        PlayerController.Instance.imStillTurn = true;
                        PlayerController.Instance.TurnandMove('r');
                    }
                    else
                    {
                        PlayerController.Instance.imStillTurn = true;
                        PlayerController.Instance.TurnandMove('l');
                    }

                }
                else
                {
                    if (direction.y > 0)
                    {
                        PlayerController.Instance.imStillTurn = true;
                        PlayerController.Instance.TurnandMove('u');
                    }
                    else
                    {

                        PlayerController.Instance.imStillTurn = true;
                        PlayerController.Instance.TurnandMove('d');
                    }

                }


            }

        }
        

    }

    public Vector2 GetDirection()
    {
        return direction;
    }

  
}
