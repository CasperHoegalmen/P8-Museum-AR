using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExhibitButton : MonoBehaviour
{
    [SerializeField] string title = "";
    [SerializeField] TextAsset exhibitTextAsset = null;
    [SerializeField] Sprite exhibitImage = null;
    [SerializeField] ExhibitTag exhibitTag;

    public string Title { get { return title; } }
    public string ExhibitText { get; private set; }
    public Sprite ExhibitImage { get { return exhibitImage; } }
    public ExhibitTag ExhibitTag { get { return exhibitTag; } }

    private void Start()
    {
        ExhibitText = exhibitTextAsset.text;
    }
}
