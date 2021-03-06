using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
    [SerializeField] Image inputBlocker = null;
    [SerializeField] Image journalIconHolder = null;
    [SerializeField] Image background = null;
    [SerializeField] Sprite closeIcon = null;
    [SerializeField] ExhibitButton[] exhibitButtons = null;

    Animator animator = null;
    Sprite journalIcon = null;

    private bool closeIconIsShowing = false;

    public static event Action JournalUIClosedEvent;

    private void Awake()
    {
        journalIconHolder.GetComponent<Image>();
        background.GetComponent<Image>();
        animator = GetComponent<Animator>();
        journalIcon = journalIconHolder.GetComponent<Image>().sprite;
    }

    //Button OnClick event
    public void ExpandJournalUI()
    {
        inputBlocker.gameObject.SetActive(true);
        animator.SetTrigger("expandUI");

        if (closeIconIsShowing)
        {
            JournalUIClosedEvent();
        }
    }
    
    //Animation event
    public void SwitchSprite()
    {
        if (!closeIconIsShowing)
        {
            journalIconHolder.sprite = closeIcon;
        }
        else
        {
            journalIconHolder.sprite = journalIcon;
        }

        closeIconIsShowing = !closeIconIsShowing;
    }

    //Animation event
    public void EnableExhibitButtons()
    {
        for (int i = 0; i < exhibitButtons.Length; i++)
        {
            exhibitButtons[i].gameObject.SetActive(true);
        }

        animator.SetBool("enableButtons", true);
    }

    //Animation event
    public void DisableExhibitButtons()
    {
        animator.SetBool("enableButtons", false);

        for (int i = 0; i < exhibitButtons.Length; i++)
        {
            exhibitButtons[i].gameObject.SetActive(false);
        }

        animator.SetTrigger("shrinkBackground");
    }

    //Animation event
    public void EnableInput()
    {
        inputBlocker.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
