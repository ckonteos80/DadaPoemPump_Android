using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickPlacement : MonoBehaviour, IPointerDownHandler
{
    LineController myLineController;
    public bool hasClicked;
    public bool tooLong;

    Draggable myDraggable;

    public PaperScrapController ScrapToPlace;
    // Start is called before the first frame update
    void Start()
    {
        myLineController = GetComponentInParent<LineController>();
        myLineController.Hands.Add(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasClicked)
        {


            if (myLineController.MySelectedScrap.SelectedScrap == null)
            {
                BoxCollider2D MyCollider = GetComponent<BoxCollider2D>();
                MyCollider.enabled = false;

                Image MyImage = GetComponent<Image>();
                MyImage.enabled = false;
            }
            else
            {
                if (myLineController.MySelectedScrap.SelectedScrap.isOpen == false)
                {
                    BoxCollider2D MyCollider = GetComponent<BoxCollider2D>();
                    MyCollider.enabled = false;

                    Image MyImage = GetComponent<Image>();
                    MyImage.enabled = false;
                }
                else
                {
                    if (!tooLong)
                    {
                        BoxCollider2D MyCollider = GetComponent<BoxCollider2D>();
                        MyCollider.enabled = true;

                        Image MyImage = GetComponent<Image>();
                        MyImage.enabled = true;
                    }
                    else
                    {
                        BoxCollider2D MyCollider = GetComponent<BoxCollider2D>();
                        MyCollider.enabled = false;

                        Image MyImage = GetComponent<Image>();
                        MyImage.enabled = false;
                    }
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("click");
        if (myLineController.MySelectedScrap.SelectedScrap != null)
        {
            if (myLineController.MySelectedScrap.SelectedScrap.isOpen)
            {
                hasClicked = true;
                ScrapToPlace = myLineController.MySelectedScrap.SelectedScrap;
                myLineController.MySelectedScrap.SelectedScrap.MyDragable.setPoint = gameObject.transform;
                myLineController.MySelectedScrap.ReleaseSelected(false, gameObject.transform.parent.transform);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasClicked)
        {


            if (other.gameObject.GetComponent<Draggable>())
            {
                Draggable EnteringDraggable = other.gameObject.GetComponent<Draggable>();
                if (EnteringDraggable.selected)
                {
                    if (myDraggable == null)
                    {
                        myDraggable = other.gameObject.GetComponent<Draggable>();
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hasClicked)
        {


            if (myDraggable != null)
            {
                // Debug.Log("trigger");
                if (other.gameObject.GetComponent<Draggable>())
                {
                    Draggable StayingDraggable = other.gameObject.GetComponent<Draggable>();
                    if (StayingDraggable.MyPaperCut.aa == myDraggable.MyPaperCut.aa)
                    {
                        if (myDraggable.selected && myDraggable.MyPaperCut.isOpen)
                        {
                            float distance = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);

                            // if (distance < 1)
                            // {


                                myDraggable.MyPaperCut.openTheScrap = true;

                                DrawShape PlacedShape = myDraggable.GetComponentInChildren<DrawShape>();
                                PlacedShape.keepDraw = true;

                                PaperScrapController MyScrap = myDraggable.GetComponent<PaperScrapController>();
                                Transform setObj = myDraggable.MyPaperCut.MyOpenScrap.transform;
                                setObj.SetParent(gameObject.transform.parent);
                                setObj.position = new Vector3(gameObject.transform.position.x + (MyScrap.textWidth + 20) / 2, gameObject.transform.position.y, gameObject.transform.position.z);


                                RowController MyRowCont = GetComponentInParent<RowController>();
                                MyRowCont.makeRow(myDraggable.MyPaperCut.textWidth + 25);

                                Destroy(myDraggable.gameObject);
                                Destroy(gameObject);
                            // }
                        }
                    }
                }
            }
        }
    }
}


