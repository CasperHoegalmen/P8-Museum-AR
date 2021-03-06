using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryOptionsManager : MonoBehaviour
{
    [SerializeField] Button[] storyUIButtons = null;
    [SerializeField] TextMeshProUGUI[] storyButtonsText = null;

    ExhibitAudioManager exhibitAudioManager;
    StoryCompletionManager storyCompletion;
    AudioUIControlManager audioUIControlManager;

    public Button[] StoryUIButtons { get { return storyUIButtons; } }

    private void Awake()
    {
        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();
        storyCompletion = FindObjectOfType<StoryCompletionManager>();
        audioUIControlManager = FindObjectOfType<AudioUIControlManager>();
    }

    public void ShowOptions(StoryPart[] exhibitStory)
    {
        audioUIControlManager.HideAudioControlUI();

        DisplayStoryQuestionsUI(exhibitStory);
    }

    private void DisplayStoryQuestionsUI(StoryPart[] exhibitStory)
    {
        if (exhibitStory[exhibitAudioManager.AudioClipIndex].numberOfOptions == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                storyUIButtons[i].gameObject.SetActive(true);
            }
            storyButtonsText[0].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[0].question;
            storyButtonsText[1].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[1].question;
            storyButtonsText[2].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[2].question;
        }
        else if (exhibitStory[exhibitAudioManager.AudioClipIndex].numberOfOptions == 2)
        {
            if (exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[1].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[4].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[5].index)
            {
                for (int i = 3; i < 5; i++)
                {
                    storyUIButtons[i].gameObject.SetActive(true);
                }
                storyButtonsText[3].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[3].question;
                storyButtonsText[4].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[4].question;
            }
            else if (exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[2].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[6].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[7].index)
            {
                for (int i = 5; i < 7; i++)
                {
                    storyUIButtons[i].gameObject.SetActive(true);
                }
                storyButtonsText[5].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[5].question;
                storyButtonsText[6].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[6].question;
            }
            else if (exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[3].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[8].index || exhibitStory[exhibitAudioManager.AudioClipIndex].index == exhibitAudioManager.CurrentExhibitStory[9].index)
            {
                for (int i = 7; i < 9; i++)
                {
                    storyUIButtons[i].gameObject.SetActive(true);
                }
                storyButtonsText[7].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[7].question;
                storyButtonsText[8].GetComponent<TextMeshProUGUI>().text = exhibitAudioManager.CurrentStoryQuestions[8].question;
            }
        }

        storyCompletion.CheckExhibitCompletion();
    }

    public void DisableOptionsButtons()
    {
        for (int i = 0; i < storyUIButtons.Length; i++)
        {
            storyUIButtons[i].gameObject.SetActive(false);
        }
    }

    public void ResetOptionsButtons()
    {
        for (int i = 0; i < storyUIButtons.Length; i++)
        {
            storyUIButtons[i].GetComponent<Image>().color = Color.white;
            storyUIButtons[i].interactable = true;
        }
    }

    public void ChooseStoryOption1()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[1].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);  
    }

    public void ChooseStoryOption2()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[2].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
    }

    public void ChooseStoryOption3()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[3].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
    }

    public void ChooseStoryOption4()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[4].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionOne();
    }

    public void ChooseStoryOption5()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[5].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionOne();
    }

    public void ChooseStoryOption6()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[6].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionTwo();
    }

    public void ChooseStoryOption7()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[7].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionTwo();
    }

    public void ChooseStoryOption8()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[8].index;
        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionThree();
    }

    public void ChooseStoryOption9()
    {
        DisableOptionsButtons();
        exhibitAudioManager.AudioClipIndex = exhibitAudioManager.CurrentExhibitStory[9].index;

        OptionInteraction(exhibitAudioManager.AudioClipIndex);
        CheckMainQuestionThree();
    }

    private void OptionInteraction(int audioClipIndex)
    {
        storyUIButtons[audioClipIndex - 1].GetComponent<Image>().color = Color.gray;
        //storyUIButtons[audioClipIndex - 1].interactable = false;

        exhibitAudioManager.PlayAudio(exhibitAudioManager.CurrentExhibitStory, audioClipIndex);
        if(audioClipIndex >= 4)
        {
            exhibitAudioManager.CurrentExhibitStory[audioClipIndex].hasFinished = true;
        }
    }

    private void CheckMainQuestionOne()
    {
        if (exhibitAudioManager.CurrentExhibitStory[4].hasFinished && exhibitAudioManager.CurrentExhibitStory[5].hasFinished)
        {
            exhibitAudioManager.CurrentExhibitStory[exhibitAudioManager.AudioClipIndex].numberOfOptions = 3;
            exhibitAudioManager.CurrentExhibitStory[1].hasFinished = true;
        }
    }

    private void CheckMainQuestionTwo()
    {
        if (exhibitAudioManager.CurrentExhibitStory[6].hasFinished && exhibitAudioManager.CurrentExhibitStory[7].hasFinished)
        {
            exhibitAudioManager.CurrentExhibitStory[exhibitAudioManager.AudioClipIndex].numberOfOptions = 3;
            exhibitAudioManager.CurrentExhibitStory[2].hasFinished = true;
        }
    }
    private void CheckMainQuestionThree()
    {
        if (exhibitAudioManager.CurrentExhibitStory[8].hasFinished && exhibitAudioManager.CurrentExhibitStory[9].hasFinished)
        {
            exhibitAudioManager.CurrentExhibitStory[exhibitAudioManager.AudioClipIndex].numberOfOptions = 3;
            exhibitAudioManager.CurrentExhibitStory[3].hasFinished = true;
        }
    }
}