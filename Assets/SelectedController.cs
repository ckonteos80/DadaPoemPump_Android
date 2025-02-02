using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedController : MonoBehaviour
{
    public PaperScrapController SelectedScrap;
    PlacementPoint myPlacementPoint;
    public Transform cutsHolder;

    public Button BurnButton;

    public PeperBagController myBagController;

    Master MyMaster;

    public LineController myLineController;
    // Start is called before the first frame update
    void Start()
    {
        myPlacementPoint = GetComponent<PlacementPoint>();
        MyMaster = GetComponentInParent<Master>();
        myLineController= GetComponentInParent<LineController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SelectedScrap != null)
        {
            if (SelectedScrap.isOpen)
            {
                // float distance = Vector3.Distance(gameObject.transform.position, SelectedScrap.gameObject.transform.position);

                // if (distance < 1)
                // {
                RectTransform SelectedRect = SelectedScrap.GetComponent<RectTransform>();
                PaperScrapController MyScrap = SelectedRect.GetComponent<PaperScrapController>();
                SelectedScrap.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                // + MyScrap.textWidth / 2
                // SelectedScrap.transform.position = gameObject.transform.position;
                // }
                BurnButton.interactable = true;
                if (MyMaster.stage == 3)
                {
                    if (MyMaster.myTipsController.tipsText.text != "Tap on the hand icons to place")
                    {
                        List<Transform> newHands = new List<Transform>();
                        for (int i = 0; i < myLineController.Hands.Count; i++)
                        {
                            if (myLineController.Hands[i] != null)
                            {
                                newHands.Add(myLineController.Hands[i]);
                            }
                        }
                        MyMaster.myTipsController.changeFlashingText(null, newHands);

                    }
                     MyMaster.myTipsController.ChangeText("Tap on the hand icons to place", MyMaster.myTipsController.upPos);
               //     MyMaster.myTipsController.tipsText.text = "Tap on the hand icons to place";
                }
            }
            else
            {
                if (MyMaster.stage == 3)
                {
                    if (MyMaster.myTipsController.tipsText.text != "Wait for it")
                    {
                        MyMaster.myTipsController.changeFlashingText(null, null);
                    }
                    MyMaster.myTipsController.ChangeText("Wait for it", MyMaster.myTipsController.centerPos);
                 //   MyMaster.myTipsController.tipsText.text = "Wait for it";
                }
                BurnButton.interactable = false;
            }

            // if (!BurnButton.activeSelf)
            // {
            //     BurnButton.SetActive(true);
            // }

        }
        else
        {
            BurnButton.interactable = false;
            // if (BurnButton.activeSelf)
            // {
            //     BurnButton.SetActive(false);
            // }
            if (MyMaster.stage == 3)
            {
                if (MyMaster.myTipsController.tipsText.text != "Tap on the paper scraps to take out of the bag")
                {
                    List<Transform> newDraggables = new List<Transform>();
                    for (int i = 0; i < myBagController.PaperCutRBs.Count; i++)
                    {
                        PaperScrapController MyScrap = myBagController.PaperCutRBs[i].GetComponent<PaperScrapController>();
                        if (!MyScrap.isOpen)
                        {
                            Draggable newDraggable = myBagController.PaperCutRBs[i].GetComponent<Draggable>();
                            newDraggables.Add(newDraggable.TextColor);
                        }

                    }
                    MyMaster.myTipsController.changeFlashingText(null, newDraggables);

                }
                 MyMaster.myTipsController.ChangeText("Tap on the paper scraps to take out of the bag", MyMaster.myTipsController.scrapsButtons);
                // MyMaster.myTipsController.tipsText.text = "Tap on the paper scraps to take out of the bag";
            }
        }
    }

    public void ReleaseSelected(bool CloseScap, Transform newParrent)
    {
        myPlacementPoint.myDraggable = null;


        if (CloseScap)
        {
            if (SelectedScrap.isOpen)
            {
                SelectedScrap.closeTheScrap = true;
            }
            SelectedScrap.MyDragable.selected = false;
            SelectedScrap.gameObject.layer = LayerMask.NameToLayer("Default");
            SelectedScrap.transform.SetParent(newParrent);
            SelectedScrap.MyDragable.MyRb.freezeRotation = false;
            SelectedScrap.MyPolyColider.enabled = true;
            SelectedScrap.MyCircleColider.enabled = false;
        }

        SelectedScrap = null;

    }

    public void BurnScrap()
    {
        if (MyMaster.stage == 3)
        {
            if (SelectedScrap.isOpen)
            {
                myPlacementPoint.myDraggable = null;
                Destroy(SelectedScrap.gameObject);
                SelectedScrap = null;
            }
        }
    }
}
