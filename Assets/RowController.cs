using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    public List<GameObject> Rows;
    public List<float> DistanceRows;
    public GameObject RowPrefab;
    // public bool MakeRow;

    public GameObject PlacementPointPrefab;

    LineController MyLineController;

    // Start is called before the first frame update
    void Start()
    {

        MyLineController = GetComponentInParent<LineController>();
        // MakeRow = true;

    }

    // Update is called once per frame
    void Update()
    {
        // if (MakeRow)
        // {
        //     makeRow(0);
        //     MakeRow = false;
        // }
    }

    public void makeRow(float distance)
    {
        DistanceRows.Add(distance);
        float newDistance = 0;
        for (int i = 0; i < DistanceRows.Count; i++)
        {
            newDistance = newDistance + DistanceRows[i];
        }

        GameObject newRow = Instantiate(RowPrefab);
        newRow.transform.SetParent(gameObject.transform);
        RectTransform newRowTransform = newRow.GetComponent<RectTransform>();
        newRowTransform.position = new Vector3(gameObject.transform.position.x + newDistance, gameObject.transform.position.y, 0);
        Rows.Add(newRow);

        GameObject newPoint = Instantiate(PlacementPointPrefab);
        newPoint.transform.SetParent(newRow.transform);
        RectTransform newPointTransform = newPoint.GetComponent<RectTransform>();
        newPointTransform.position = new Vector3(gameObject.transform.position.x + newDistance, gameObject.transform.position.y);

        MyLineController.CheckLine(gameObject.transform.GetSiblingIndex());
        MyLineController.CheckPrevious(gameObject.transform.GetSiblingIndex());
    }

   
}
