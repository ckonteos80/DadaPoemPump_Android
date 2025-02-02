using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeperBagController : MonoBehaviour
{

    public float ShakeMultiplier;
    public LineController MyLineController;
    public List<Rigidbody2D> PaperCutRBs;

    public float MinShake;
    public float MaxShake;

    Master MyMaster;

    public LineController myLineController;

    public Button FinishButton;

    public CleanContent myCleanContent;

    // Start is called before the first frame update
    void Start()
    {
        MyMaster = GetComponentInParent<Master>();

    }

    // Update is called once per frame
    void Update()
    {
        if (MyMaster.stage == 3)
        {
            if (PaperCutRBs.Count == 0)
            {
                myCleanContent.CheckAndDestroy();
                MyMaster.SetStage(4);
            }
            else
            {
                for (int i = 0; i < PaperCutRBs.Count; i++)
                {
                    if (PaperCutRBs[i] == null)
                    {
                        PaperCutRBs.RemoveAt(i);
                    }
                }
            }
        }

        if (myLineController.Lines.Count > 1)
        {
            FinishButton.interactable = true;
            // if (!FinishButton.activeSelf)
            // {
            //     FinishButton.SetActive(true);
            // }
        }
        else
        {
            FinishButton.interactable = false;
            // if (FinishButton.activeSelf)
            // {
            //     FinishButton.SetActive(false);
            // }
        }
    }

    public void ShakeRB(Vector3 DeviceAcceleration)
    {
        foreach (var Rigidbody in PaperCutRBs)
        {
            // Rigidbody.AddForce(DeviceAcceleration * ShakeMultiplier, ForceMode2D.Impulse);
            Rigidbody.AddForce(new Vector3(Random.Range(10, 50), Random.Range(10, 50), Random.Range(10, 50)), ForceMode2D.Impulse);
        }
    }

    public void ShakeRBrandom()
    {
        foreach (var Rigidbody in PaperCutRBs)
        {
            // Rigidbody.AddForce(DeviceAcceleration * ShakeMultiplier, ForceMode2D.Impulse);
            Rigidbody.AddForce(new Vector3(Random.Range(MinShake, MaxShake), Random.Range(MinShake, MaxShake), Random.Range(MinShake, MaxShake)), ForceMode2D.Impulse);
        }
        MyMaster.MyShake.finishShake();

    }

    public void FinishPlace()
    {
        if (PaperCutRBs.Count > 0)
        {
            for (int i = PaperCutRBs.Count - 1; i >= 0; i--)
            {
                Destroy(PaperCutRBs[i].gameObject);
            }
            myCleanContent.CheckAndDestroy();
        }
    }
    public void ClearScraps()
    {
        for (int i = 0; i < PaperCutRBs.Count; i++)
        {
            if (PaperCutRBs[i] != null)
            {
                Destroy(PaperCutRBs[i].gameObject);
            }


        }
        PaperCutRBs.Clear();
    }
}
