using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmController : MonoBehaviour
{
    public bool confirmed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoConfirm()
    {
        confirmed = true;
        Destroy(gameObject);
    }

    public void DoCancel()
    {
        Destroy(gameObject);
    }
}
