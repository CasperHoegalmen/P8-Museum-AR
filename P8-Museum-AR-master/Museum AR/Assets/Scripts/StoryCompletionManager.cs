using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryCompletionManager : MonoBehaviour
{
    [SerializeField] AudioClip swordMoveOn = null, needlesMoveOn = null, tubMoveOn = null,
        signMoveOn = null, skullMoveOn = null, bankMoveOn = null;
    [SerializeField] AudioClip finishExperience = null;
    [SerializeField] NPCManager npcManager;
    ExhibitAudioManager exhibitAudioManager;
    StoryOptionsManager storyOptionsManager;
    AudioUIControlManager audioUIControlManager;
    float waitBeforeEnding = 1f;

    public bool IsSwordOver { get; private set; }
    public bool IsNeedlesOver { get; private set; }
    public bool IsTubOver { get; private set; }
    public bool IsSignOver { get; private set; }
    public bool IsSkullOver { get; private set; }
    public bool IsBankOver { get; private set; }
    public bool IsEnd { get; private set; }
    public bool IsInteractive { get; private set; } = true;

    public static event Action<ExhibitTag> ExhibitVisitedEvent;

    void Awake()
    {
        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();
        storyOptionsManager = FindObjectOfType<StoryOptionsManager>();
        audioUIControlManager = FindObjectOfType<AudioUIControlManager>();
        npcManager.GetComponent<NPCManager>();
    }

    public void CheckExhibitCompletion()
    {
        if (!IsSwordOver)
        {
            if(exhibitAudioManager.SwordStory[1].hasFinished && exhibitAudioManager.SwordStory[2].hasFinished 
                && exhibitAudioManager.SwordStory[3].hasFinished)
            {
                IsSwordOver = true;
                ExhibitVisitedEvent(ExhibitTag.Sword);
                StartCoroutine(MoveOnToNextExhibit(swordMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
        if (!IsNeedlesOver)
        {
            if (exhibitAudioManager.NeedlesStory[1].hasFinished && exhibitAudioManager.NeedlesStory[2].hasFinished
                && exhibitAudioManager.NeedlesStory[3].hasFinished)
            {
                IsNeedlesOver = true;
                ExhibitVisitedEvent(ExhibitTag.Tattoo);
                StartCoroutine(MoveOnToNextExhibit(needlesMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
        if (!IsTubOver)
        {
            if (exhibitAudioManager.TubStory[1].hasFinished && exhibitAudioManager.TubStory[2].hasFinished
                && exhibitAudioManager.TubStory[3].hasFinished)
            {
                IsTubOver = true;
                ExhibitVisitedEvent(ExhibitTag.Bathtub);
                StartCoroutine(MoveOnToNextExhibit(tubMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
        if (!IsSignOver)
        {
            if (exhibitAudioManager.SignStory[1].hasFinished && exhibitAudioManager.SignStory[2].hasFinished
                && exhibitAudioManager.SignStory[3].hasFinished)
            {
                IsSignOver = true;
                ExhibitVisitedEvent(ExhibitTag.Petrea);
                StartCoroutine(MoveOnToNextExhibit(signMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
        if (!IsSkullOver)
        {
            if (exhibitAudioManager.SkullStory[1].hasFinished && exhibitAudioManager.SkullStory[2].hasFinished
                && exhibitAudioManager.SkullStory[3].hasFinished)
            {
                IsSkullOver = true;
                ExhibitVisitedEvent(ExhibitTag.Skull);
                StartCoroutine(MoveOnToNextExhibit(skullMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
        if (!IsBankOver)
        {
            if (exhibitAudioManager.BankStory[1].hasFinished && exhibitAudioManager.BankStory[2].hasFinished
                && exhibitAudioManager.BankStory[3].hasFinished)
            {
                IsBankOver = true;
                ExhibitVisitedEvent(ExhibitTag.Bank);
                StartCoroutine(MoveOnToNextExhibit(bankMoveOn));
                ImageTargetController.SwitchToNextImageTarget();
            }
        }
    }

    IEnumerator MoveOnToNextExhibit(AudioClip audioClip)
    {
        for(int i = 0; i < storyOptionsManager.StoryUIButtons.Length; i++)
        {
            storyOptionsManager.StoryUIButtons[i].interactable = false;
        }
        audioUIControlManager.SkipButton.interactable = false;
        IsInteractive = false;
        yield return Delay();
        exhibitAudioManager.GetAudioSource.PlayOneShot(audioClip);
        StartCoroutine(WaitThenCompleteExhibit());
    }

    IEnumerator WaitThenCompleteExhibit()
    {
        yield return new WaitUntil(() => !exhibitAudioManager.GetAudioSource.isPlaying && exhibitAudioManager.IsDisplayQuestions);
        IsInteractive = true;
        storyOptionsManager.DisableOptionsButtons();
        audioUIControlManager.HideAudioControlUI();
        audioUIControlManager.HideNPC();
        CheckForEnd();
    }

    private void CheckForEnd()
    {
        if (IsSwordOver && IsNeedlesOver && IsTubOver
            && IsSkullOver && IsSignOver && IsBankOver)
        {
            IsEnd = true;
            if (IsEnd)
            {
                StartCoroutine(CompleteJourney());
            }
        }
    }

    IEnumerator CompleteJourney()
    {
        yield return Delay();
        exhibitAudioManager.GetAudioSource.PlayOneShot(finishExperience);
        npcManager.NPCAnimator.Play("Default NPC Animation");
        audioUIControlManager.SkipButton.interactable = false;
        IsInteractive = false;
        yield return new WaitUntil(() => !exhibitAudioManager.GetAudioSource.isPlaying && exhibitAudioManager.IsDisplayQuestions);
        IsInteractive = true;
        audioUIControlManager.HideAudioControlUI();
        audioUIControlManager.HideNPC();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitBeforeEnding);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ExhibitVisitedEvent(ExhibitTag.Sword);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExhibitVisitedEvent(ExhibitTag.Bathtub);
        }
    }
}