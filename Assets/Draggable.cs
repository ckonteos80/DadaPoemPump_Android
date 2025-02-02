using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Master MyMaster;
    // private Vector3 screenPoint;
    // private Vector3 offset;
    // public bool dragging = false;
    public Rigidbody2D MyRb;
    public PaperScrapController MyPaperCut;
    public float speedY;
    public float speedX;
    // public bool OnPoint;

    public Transform setPoint;

    public bool selected;
    public Transform ImageTransform;
    public Transform TextColor;

    void Start()
    {
        MyMaster = GetComponentInParent<Master>();
        // MyPaperCut = GetComponent<PaperCutController>();
        MyRb = GetComponent<Rigidbody2D>();
        MyPaperCut = GetComponent<PaperScrapController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("has clicked");
        if (MyMaster.stage == 3)
        {

            if (MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.SelectedScrap == null)
            {
                MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.SelectedScrap = MyPaperCut;
                selected = true;
                gameObject.transform.SetParent(MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.transform);
                MyPaperCut.MyDragable.MyRb.freezeRotation = true;

            }
            else
            {
                if (MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.SelectedScrap.aa != MyPaperCut.aa)
                {
                    MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.ReleaseSelected(true, MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.cutsHolder);

                    MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.SelectedScrap = MyPaperCut;
                    selected = true;
                    gameObject.transform.SetParent(MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.transform);
                }
            }
            MyRb.freezeRotation = true;
            MyPaperCut.MyPolyColider.enabled = false;
            MyPaperCut.MyCircleColider.enabled = true;
            MyPaperCut.MainRectTransform.localEulerAngles = new Vector3( MyPaperCut.MainRectTransform.localEulerAngles.x,  MyPaperCut.MainRectTransform.localEulerAngles.y, 0f);
    

            gameObject.layer = LayerMask.NameToLayer("DragTrigger");
        }

        // screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        // offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        // dragging = true;
        // MyRb.gravityScale = 0;

        // if (!MyPaperCut.isOpen)
        // {
        //     MyPaperCut.openTheScrap = true;
        // }
    }

    private void FixedUpdate()
    {
        // if (MyPaperCut.isOpen)
        // {
        //     if (!MyRb.freezeRotation)
        //     {
        //         MyRb.freezeRotation = true;
        //     }
        // }
        // else
        // {
        //     if (MyRb.freezeRotation)
        //     {
        //         MyRb.freezeRotation = false;
        //     }
        // }
        if (selected && !MyPaperCut.isOpen)
        {
            speedX = MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.transform.position.x - MyRb.transform.position.x;
            // - (MyPaperCut.textWidth / 2)
            speedY = MyPaperCut.MyPaperBag.MyLineController.MySelectedScrap.transform.position.y - MyRb.transform.position.y;
            MyRb.linearVelocity = (new Vector2((speedX * 2) + 50, (speedY * 2) + 50));
            // MyRb.velocity = (new Vector2(MyRb.velocity.x,);
        }

        if (setPoint != null && MyPaperCut.isOpen)
        {
            speedX = setPoint.position.x - MyRb.transform.position.x;
            // - (MyPaperCut.textWidth / 2
            speedY = setPoint.position.y - MyRb.transform.position.y;
            MyRb.linearVelocity = new Vector2(speedX * 2, MyRb.linearVelocity.y);
            MyRb.linearVelocity = new Vector2(MyRb.linearVelocity.x, speedY * 2);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // if (!OnPoint)
        // {
        //     if (MyPaperCut.isOpen)
        //     {
        //         MyPaperCut.closeTheScrap = true;
        //     }
        // }
        // dragging = false;
        // MyRb.gravityScale = 3;
    }
}







// Start is called before the first frame update
// private RectTransform rectTransform;
// private Vector3 offset;
// public bool dragging = false;

// void Start()
// {
//     rectTransform = GetComponent<RectTransform>();
// }

// public void OnPointerDown(PointerEventData eventData)
// {
//     offset = transform.parent.parent.position - Camera.main.ScreenToWorldPoint(eventData.position);
//     dragging = true;
// }

// public void OnDrag(PointerEventData eventData)
// {
//     if (dragging)
//     {
//         rectTransform.parent.parent.position = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
//     }
// }

// public void OnPointerUp(PointerEventData eventData)
// {
//     dragging = false;
// }

