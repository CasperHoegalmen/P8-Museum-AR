using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardText : MonoBehaviour
{
    [SerializeField] Text highlightText;


    void Update()
    {
        CheckParentRenderer();
    }

    private void CheckParentRenderer()
    {
        if (gameObject.GetComponentInParent<Renderer>().enabled == true)
        {
            highlightText.gameObject.SetActive(true);
            ActivateBillboardText();
        }
        else
        {
            highlightText.gameObject.SetActive(false);
        }
    }

    private void ActivateBillboardText()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        highlightText.transform.position = namePos;
    }
}
