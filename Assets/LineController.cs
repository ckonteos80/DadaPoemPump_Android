using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    public List<GameObject> Lines;
    public List<Transform> Hands;
    public GameObject LinePrefab;
    public GameObject PlacementPointPrefab;

    public RectTransform Content;

    public SelectedController MySelectedScrap;

    public Transform GuideLine;

    // public bool MakeLine;

    RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        // myRect.sizeDelta = new Vector2(Screen.width - 100, Screen.height - 400);
        MySelectedScrap = GetComponentInChildren<SelectedController>();
        //   setFirstLine();
         setFirstLine();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFirstLine()
    {
        GameObject newLine = Instantiate(LinePrefab);
        newLine.transform.SetParent(Content.transform);
        RectTransform NewLineTransform = newLine.GetComponent<RectTransform>();
        NewLineTransform.position = new Vector3(gameObject.transform.position.x, GuideLine.position.y, 0);
        Lines.Add(newLine);

        GameObject newPoint = Instantiate(PlacementPointPrefab);
        newPoint.transform.SetParent(newLine.transform);
        RectTransform newPointTransform = newPoint.GetComponent<RectTransform>();
        newPointTransform.position = new Vector3(newLine.transform.position.x, newLine.transform.position.y);

        Content.sizeDelta = new Vector2(Content.sizeDelta.x, ((Lines.Count * 75)) * 1.5f);
    }

    void makeLine()
    {
        GameObject newLine = Instantiate(LinePrefab);
        newLine.transform.SetParent(Content.transform);
        RectTransform NewLineTransform = newLine.GetComponent<RectTransform>();
        NewLineTransform.position = new Vector3(gameObject.transform.position.x, Lines[Lines.Count - 1].transform.position.y - 75, 0);
        Lines.Add(newLine);

        GameObject newPoint = Instantiate(PlacementPointPrefab);
        newPoint.transform.SetParent(newLine.transform);
        RectTransform newPointTransform = newPoint.GetComponent<RectTransform>();
        newPointTransform.position = new Vector3(newLine.transform.position.x, newLine.transform.position.y);

        Content.sizeDelta = new Vector2(Content.sizeDelta.x, ((Lines.Count * 75)) * 1.5f);

    }
    public void CheckLine(int currentLine)
    {
        int noLinesLess = Content.transform.childCount - currentLine;
        int noLinesToMake = 3 - noLinesLess;
        if (noLinesToMake > 0)
        {
            for (int i = 0; i < noLinesToMake; i++)
            {
                makeLine();
            }
        }
    }
    public void CheckPrevious(int currentLine)
    {
        for (int i = 0; i < currentLine; i++)
        {
            if (Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>())
            {
                Destroy(Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().gameObject);
            }
        }
    }
    public void CheckSize(float sizeX)
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            if (Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>())
            {
                ClickPlacement LongClick = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>();
                RectTransform pointToCheck = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().GetComponent<RectTransform>();
                if (pointToCheck.position.x + sizeX > myRect.sizeDelta.x)
                {

                    LongClick.tooLong = true;
                    // BoxCollider2D MyCollider = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().GetComponent<BoxCollider2D>();
                    // MyCollider.enabled = false;

                    // Image MyImage = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().GetComponent<Image>();
                    // MyImage.enabled = false;
                }
                else
                {
                    LongClick.tooLong = false;
                }
            }
        }
    }

    public void EnablePoints(bool on)
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            if (Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>())
            {

                BoxCollider2D MyCollider = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().GetComponent<BoxCollider2D>();
                MyCollider.enabled = on;

                Image MyImage = Content.transform.GetChild(i).GetComponentInChildren<ClickPlacement>().GetComponent<Image>();
                MyImage.enabled = on;

            }
        }
    }

    public void ClearLines()
    {
        for (int i = 0; i < Lines.Count; i++)
        {
            if (Lines[i] != null)
            {
                Destroy(Lines[i].gameObject);
            }


        }
        Lines.Clear();
    }
}
