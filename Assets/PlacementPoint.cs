using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPoint : MonoBehaviour
{
    public Draggable myDraggable;
    // public Transform PointToPlace;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (myDraggable != null)
        {
            // Debug.Log("trigger");
            if (other.gameObject.GetComponent<Draggable>())
            {
                Draggable StayingDraggable = other.gameObject.GetComponent<Draggable>();
                if (StayingDraggable.MyPaperCut.aa == myDraggable.MyPaperCut.aa)
                {
                    if (myDraggable.selected && !myDraggable.MyPaperCut.isOpen)
                    {
                        myDraggable.MyPaperCut.openTheScrap = true;

                        //     myDraggable.MyPaperCut.TextField.color = new Color(255, 0, 0);
                        //     myDraggable.OnPoint = true;
                        //     // myDraggable.MyPaperCut.openTheScrap = true;

                        // }

                        // if (!myDraggable.dragging)
                        // {
                        //     Transform setObj = myDraggable.MyPaperCut.MyOpenScrap.transform;
                        //     setObj.SetParent(gameObject.transform.parent);
                        //     setObj.position = gameObject.transform.position;

                        //     RowController MyRowCont = GetComponentInParent<RowController>();
                        //     MyRowCont.makeRow(myDraggable.MyPaperCut.textWidth + 50);

                        //     Destroy(myDraggable.gameObject);
                        //     Destroy(gameObject);
                        // }
                    }
                }
            }
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.GetComponent<Draggable>())
    //     {
    //         if (myDraggable != null)
    //         {
    //             Draggable ExitingDraggable = other.gameObject.GetComponent<Draggable>();
    //             if (ExitingDraggable.MyPaperCut.aa == myDraggable.MyPaperCut.aa)
    //             {
    //                 //     if (myDraggable.dragging && myDraggable.MyPaperCut.isOpen)
    //                 //     {
    //                 //         // myDraggable.MyPaperCut.closeTheScrap = true;
    //                 //         myDraggable.MyPaperCut.TextField.color = new Color(0, 0, 0);
    //                 //         myDraggable.OnPoint = false;
    //                 myDraggable = null;
    //                 // }
    //             }
    //         }
    //     }
    // }
}
