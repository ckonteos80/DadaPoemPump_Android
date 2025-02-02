using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetersText : MonoBehaviour
{

    public float LetterNo;
    public RectTransform parentRectTransform;
    RectTransform myRect;
    Master myMaster;
    TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        // parentRectTransform = GetComponentInParent<RectTransform>();
        myRect = GetComponent<RectTransform>();
        myMaster = GetComponentInParent<Master>();
        myText = GetComponent<TextMeshProUGUI>();
        
         StartCoroutine(ChangeFontCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentRectTransform.rect.height / 4);
        myRect.anchoredPosition = new Vector2(0, -parentRectTransform.rect.height / 6 * LetterNo);

    }

    private IEnumerator ChangeFontCoroutine()
    {
        while (true)
        {
            // Pick a random font from the list
            int randomIndex = Random.Range(0, myMaster.fontList.Count);
            TMP_FontAsset selectedFont =  myMaster.fontList[randomIndex];

            // Apply the font to the TextMeshPro
            myText.font = selectedFont;

            // Wait for 1 second before changing the font again
            yield return new WaitForSeconds(1f);
        }
    }
}
