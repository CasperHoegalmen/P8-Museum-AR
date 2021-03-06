using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitAudioManager : MonoBehaviour
{
    [Header("Sword Exhibit")]
    [SerializeField] StoryPart[] swordStory = null;
    [SerializeField] QuestionsText[] swordQuestions = null;

    [Header("Tattoo Needles Exhibit")]
    [SerializeField] StoryPart[] needlesStory = null;
    [SerializeField] QuestionsText[] needlesQuestions = null;

    [Header("Tub Exhibit")]
    [SerializeField] StoryPart[] tubStory = null;
    [SerializeField] QuestionsText[] tubQuestions = null;

    [Header("Sign Exhibit")]
    [SerializeField] StoryPart[] signStory = null;
    [SerializeField] QuestionsText[] signQuestions = null;

    [Header("Skull Exhibit")]
    [SerializeField] StoryPart[] skullStory = null;
    [SerializeField] QuestionsText[] skullQuestions = null;

    [Header("Bank Exhibit")]
    [SerializeField] StoryPart[] bankStory = null;
    [SerializeField] QuestionsText[] bankQuestions = null;

    IEnumerator coroutine;
    bool triggerSwordStory, triggerNeedlesStory, triggerTubStory, 
        triggerSignStory, triggerSkullStory, triggerBankStory;
    
    StoryOptionsManager storyOptionsManager;
    StoryCompletionManager storyCompletion;
    AudioUIControlManager audioUIControlManager;

    public StoryPart[] SwordStory { get { return swordStory; } }
    public StoryPart[] NeedlesStory { get { return needlesStory; } }
    public StoryPart[] TubStory { get { return tubStory; } }
    public StoryPart[] SignStory { get { return signStory; } }
    public StoryPart[] SkullStory { get { return skullStory; } }
    public StoryPart[] BankStory { get { return bankStory; } }
    public AudioSource GetAudioSource { get; private set; }
    public StoryPart[] CurrentExhibitStory { get; private set; } = null;
    public QuestionsText[] CurrentStoryQuestions { get; private set; } = null;
    public int AudioClipIndex { get; set; }
    public bool IsDisplayQuestions { get; set; } = true;
    public bool TriggerSwordStory { set { triggerSwordStory = value; } }
    public bool TriggerNeedlesStory { set { triggerNeedlesStory = value; } }
    public bool TriggerTubStory { set { triggerTubStory = value; } }
    public bool TriggerSignStory { set { triggerSignStory = value; } }
    public bool TriggerSkullStory { set { triggerSkullStory = value; } }
    public bool TriggerBankStory { set { triggerBankStory = value; } }

    //public IEnumerator Coroutine { get { return coroutine; } }

    void Awake()
    {
        GetAudioSource = GetComponent<AudioSource>();
        storyOptionsManager = FindObjectOfType<StoryOptionsManager>();
        storyCompletion = FindObjectOfType<StoryCompletionManager>();
        audioUIControlManager = FindObjectOfType<AudioUIControlManager>();
        ResetScriptableObjectsProperties();
    }

    private void ResetScriptableObjectsProperties()
    {
        foreach (StoryPart story in swordStory)
        {
            story.hasFinished = false;
        }
        foreach (StoryPart story in needlesStory)
        {
            story.hasFinished = false;
        }
        foreach (StoryPart story in tubStory)
        {
            story.hasFinished = false;
        }
        foreach (StoryPart story in signStory)
        {
            story.hasFinished = false;
        }
        foreach(StoryPart story in skullStory)
        {
            story.hasFinished = false;
        }
        foreach(StoryPart story in bankStory)
        {
            story.hasFinished = false;
        }

        for (int i = 4; i < swordStory.Length; i++)
        {
            swordStory[i].numberOfOptions = 2;
            needlesStory[i].numberOfOptions = 2;
            tubStory[i].numberOfOptions = 2;
            signStory[i].numberOfOptions = 2;
            skullStory[i].numberOfOptions = 2;
            bankStory[i].numberOfOptions = 2;
        }
    }

    void Update()
    {
        ManualStoryTrigger();
        ExhibitStart();
    }

    private void ManualStoryTrigger()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            triggerSwordStory = true;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            triggerNeedlesStory = true;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            triggerTubStory = true;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            triggerSignStory = true;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            triggerSkullStory = true;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            triggerBankStory = true;
        }
    }

    private void ExhibitStart()
    {
        if (triggerSwordStory && !storyCompletion.IsSwordOver)
        {
            CurrentExhibitStory = swordStory;
            CurrentStoryQuestions = swordQuestions;
            ExhibitPreset();
            triggerSwordStory = false;
        }
        else if(triggerNeedlesStory && !storyCompletion.IsNeedlesOver)
        {
            CurrentExhibitStory = needlesStory;
            CurrentStoryQuestions = needlesQuestions;
            ExhibitPreset();
            triggerNeedlesStory = false;
        }
        else if (triggerTubStory && !storyCompletion.IsTubOver)
        {
            CurrentExhibitStory = tubStory;
            CurrentStoryQuestions = tubQuestions;
            ExhibitPreset();
            triggerTubStory = false;
        }
        else if (triggerSignStory && !storyCompletion.IsSignOver)
        {
            CurrentExhibitStory = signStory;
            CurrentStoryQuestions = signQuestions;
            ExhibitPreset();
            triggerSignStory = false;
        }
        else if(triggerSkullStory && !storyCompletion.IsSkullOver)
        {
            CurrentExhibitStory = skullStory;
            CurrentStoryQuestions = skullQuestions;
            ExhibitPreset();
            triggerSkullStory = false;
        }
        else if(triggerBankStory && !storyCompletion.IsBankOver)
        {
            CurrentExhibitStory = bankStory;
            CurrentStoryQuestions = bankQuestions;
            ExhibitPreset();
            triggerBankStory = false;
        }
    }

    private void ExhibitPreset()
    {
        storyOptionsManager.ResetOptionsButtons();
        audioUIControlManager.SkipButton.interactable = true;
        AudioClipIndex = 0;
        PlayAudio(CurrentExhibitStory, AudioClipIndex);
        audioUIControlManager.DetermineExhibitRandomVoiceLines();
    }

    public void PlayAudio(StoryPart[] exhibitStory, int audioClipIndex)
    {
        if(audioClipIndex >= exhibitStory.Length) { return; }

        if (!GetAudioSource.isPlaying)
        {
            GetAudioSource.PlayOneShot(exhibitStory[audioClipIndex].audioClip);
            coroutine = WaitThenDisplayQuestions(exhibitStory);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator WaitThenDisplayQuestions(StoryPart[] exhibitStory)
    {
        yield return new WaitUntil(() => !GetAudioSource.isPlaying && IsDisplayQuestions);
        storyOptionsManager.ShowOptions(exhibitStory);
    }
}