using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagControll : MonoBehaviour
{

    public GameObject ScrapPrefab;
    public GameObject BagPrefab;

    public bool MakingBag;

    public Transform BagBottom;
    public Transform BagLeft;
    public Transform BagRight;
    public Transform BagTop;

    public float increment;
    public Transform BagColliders;
    float aa = 0;
    float startY;
    public float ratio;
    // Start is called before the first frame update
    void Start()
    {
        startY = BagBottom.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (MakingBag)
        {

            aa = aa + 1;
            startY = startY * ratio;
            Vector3 NewStitchToRight = new Vector3(BagBottom.position.x + (increment * aa), startY, BagBottom.position.z);
            Vector3 NewStitchToLeft = new Vector3(BagBottom.position.x - (increment * aa), startY, BagBottom.position.z);
            if (NewStitchToRight.x < BagRight.position.x)
            {
                Instantiate(BagPrefab, NewStitchToRight, gameObject.transform.rotation).transform.SetParent(BagColliders);
                Instantiate(BagPrefab, NewStitchToLeft, gameObject.transform.rotation).transform.SetParent(BagColliders);

            }
            else
            {
                MakingBag = false;
                startY = BagBottom.position.y;
                aa = 0;
            }
        }
    }

    public void CreateBag()
    {

    }
}
