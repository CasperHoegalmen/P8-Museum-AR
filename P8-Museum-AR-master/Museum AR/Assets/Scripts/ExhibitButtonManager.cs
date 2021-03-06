using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ExhibitButtonManager : MonoBehaviour
{
    public ExhibitButton[] ExhibitButtons { get; private set; }

    private void OnEnable()
    {
        StoryCompletionManager.ExhibitVisitedEvent += OnExhibitVisited;
        //Subscribe to event that triggers once an exhibit has been fully explored/visited
    }

    private void OnDisable()
    {
        StoryCompletionManager.ExhibitVisitedEvent -= OnExhibitVisited;
        //Unsubscribe to event that triggers once an exhibit has been fully explored/visited
    }

    private void Start()
    {
        ExhibitButtons = FindObjectsOfType<ExhibitButton>();
        
        for (int i = 0; i < ExhibitButtons.Length; i++)
        {
            ExhibitButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnExhibitVisited(ExhibitTag exhibitTag)
    {
        for(int i = 0; i < ExhibitButtons.Length; i++)
        {
            ExhibitButton exhibitButton = ExhibitButtons[i];

            if(exhibitButton.ExhibitTag == exhibitTag)
            {
                Image iconImage = exhibitButton.transform.GetChild(0).GetComponent<Image>();
                Color temporaryColor = iconImage.color;
                temporaryColor.a = 1f;
                iconImage.color = temporaryColor;
                exhibitButton.gameObject.tag = "Visited";
            }
        }
    }
}
