using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] ExhibitButtonManager exhibitButtonManager = null;
    [SerializeField] Canvas journalCanvas = null;
    [SerializeField] TextMeshProUGUI exhibitTitle = null;
    [SerializeField] Image exhibitImage = null;
    [SerializeField] TextMeshProUGUI exhibitText = null;

    private int pageNumber = 0;
    private string defaultExhibitText = "";

    private void Awake()
    {
        journalCanvas.GetComponent<Canvas>();
        exhibitTitle.GetComponent<TextMeshProUGUI>();
        exhibitImage.GetComponent<Image>();
        exhibitText.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        JournalUI.JournalUIClosedEvent += OnJournalClosed;
    }

    private void OnDisable()
    {
        JournalUI.JournalUIClosedEvent -= OnJournalClosed;
    }

    private void Start()
    {
        defaultExhibitText = exhibitText.text;
    }

    //Button OnClick event
    public void OpenJournal(ExhibitButton exhibitButton)
    {
        pageNumber = (int)exhibitButton.ExhibitTag;
        
        PopulateJournalInfo(exhibitButton);
    }

    private void PopulateJournalInfo(ExhibitButton exhibitButton)
    {
        exhibitTitle.text = exhibitButton.Title;
        exhibitImage.sprite = exhibitButton.ExhibitImage;

        if (exhibitButton.gameObject.tag == "Visited")
        {
            exhibitText.text = exhibitButton.ExhibitText;
        }
        else
        {
            exhibitText.text = defaultExhibitText;
        }

        if (!journalCanvas.gameObject.activeInHierarchy)
        {
            journalCanvas.gameObject.SetActive(true);
        }
    }

    //Button OnClick event
    public void NextPage()
    {
        int numberOfNextPage = pageNumber + 1;

        if (numberOfNextPage < exhibitButtonManager.ExhibitButtons.Length)
        {
            pageNumber++;

            for(int i = 0; i < exhibitButtonManager.ExhibitButtons.Length; i++)
            {
                if((int)exhibitButtonManager.ExhibitButtons[i].ExhibitTag == pageNumber)
                {
                    ExhibitButton exhibitButton = exhibitButtonManager.ExhibitButtons[i];
                    PopulateJournalInfo(exhibitButton);
                }
            }
        }
    }

    //Button OnClick event
    public void PreviousPage()
    {
        int numberOfPreviousPage = pageNumber - 1;

        if (numberOfPreviousPage >= 0)
        {
            pageNumber--;

            for (int i = 0; i < exhibitButtonManager.ExhibitButtons.Length; i++)
            {
                if ((int)exhibitButtonManager.ExhibitButtons[i].ExhibitTag == pageNumber)
                {
                    ExhibitButton exhibitButton = exhibitButtonManager.ExhibitButtons[i];
                    PopulateJournalInfo(exhibitButton);
                }
            }
        }
    }

    public void OnJournalClosed()
    {
        journalCanvas.gameObject.SetActive(false);
    }
}
