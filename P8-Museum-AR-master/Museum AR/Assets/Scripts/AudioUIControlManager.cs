using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIControlManager : MonoBehaviour
{
    [SerializeField] Button skipButton = null, playPauseButton = null;
    [SerializeField] Image npcTalkingIndicator = null, npcTalkingIndicatorIcon = null, playPauseIcon = null;
    [SerializeField] Sprite playSprite = null, npcNotTalkingIndicator = null;

    [Header("Sword Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] swordPauseVoiceLines = null; [SerializeField] AudioClip[] swordUnpauseVoiceLines = null; [SerializeField] AudioClip[] swordSkipVoiceLines = null;
    [Header("Sign Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] signPauseVoiceLines = null; [SerializeField] AudioClip[] signUnpauseVoiceLines = null; [SerializeField] AudioClip[] signSkipVoiceLines = null;
    [Header("Tub Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] tubPauseVoiceLines = null; [SerializeField] AudioClip[] tubUnpauseVoiceLines = null; [SerializeField] AudioClip[] tubSkipVoiceLines = null;
    [Header("Needles Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] needlesPauseVoiceLines = null; [SerializeField] AudioClip[] needlesUnpauseVoiceLines = null; [SerializeField] AudioClip[] needlesSkipVoiceLines = null;
    [Header("Skull Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] skullPauseVoiceLines = null; [SerializeField] AudioClip[] skullUnpauseVoiceLines = null; [SerializeField] AudioClip[] skullSkipVoiceLines = null;
    [Header("Bank Exhibit Random Voice Lines")]
    [SerializeField] AudioClip[] bankPauseVoiceLines = null; [SerializeField] AudioClip[] bankUnpauseVoiceLines = null; [SerializeField] AudioClip[] bankSkipVoiceLines = null;

    AudioClip[] currentPausingVoiceLines = null, currentUspausingVoiceLines = null, currentSkippingVoiceLines = null;

    Sprite defaultPauseSprite, defaultTalkingIndicatorSprite;
    ExhibitAudioManager exhibitAudioManager;
    StoryCompletionManager storyCompletionManager;
    NPCManager npc;
    AudioSource audioSource;
    float shortDelay = 0.4f;

    enum AudioAction { Pause, Unpause, Skip}
    AudioAction audioAction;

    public Button SkipButton { get { return skipButton; } }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();
        npc = FindObjectOfType<NPCManager>();
        storyCompletionManager = FindObjectOfType<StoryCompletionManager>();
        defaultPauseSprite = playPauseIcon.GetComponent<Image>().sprite;
        defaultTalkingIndicatorSprite = npcTalkingIndicatorIcon.sprite;
        playPauseButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        npcTalkingIndicator.gameObject.SetActive(false);
        npc.gameObject.SetActive(false);
    }

    void Update()
    {
        TalkingIcon();
    }

    public void PlayPause()
    {
        if (playPauseIcon.GetComponent<Image>().sprite == defaultPauseSprite)
        {
            playPauseIcon.GetComponent<Image>().sprite = playSprite;
            audioAction = AudioAction.Pause;
            PlayRandomVoiceLine(currentPausingVoiceLines, audioAction);
        }
        else
        {
            playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
            audioAction = AudioAction.Unpause;
            PlayRandomVoiceLine(currentUspausingVoiceLines, audioAction);
        }
    }

    public void Skip()
    {
        playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
        audioAction = AudioAction.Skip;
        PlayRandomVoiceLine(currentSkippingVoiceLines, audioAction);
    }

    private void TalkingIcon()
    {
        if (exhibitAudioManager.GetAudioSource.isPlaying || audioSource.isPlaying)
        {
            npcTalkingIndicatorIcon.sprite = defaultTalkingIndicatorSprite;
            npcTalkingIndicator.gameObject.SetActive(true);
            playPauseButton.gameObject.SetActive(true);
            skipButton.gameObject.SetActive(true);
            npc.gameObject.SetActive(true);
            if (!storyCompletionManager.IsEnd)
            {
                npc.ChangeExhibitNPC();
            }
        }
        else
        {
            npcTalkingIndicatorIcon.sprite = npcNotTalkingIndicator;
        }
    }

    public void HideAudioControlUI()
    {
        playPauseButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
    }

    public void HideNPC()
    {
        npc.gameObject.SetActive(false);
        npcTalkingIndicator.gameObject.SetActive(false);
    }
    
    public void DetermineExhibitRandomVoiceLines()
    {
        if (exhibitAudioManager.CurrentExhibitStory == null || exhibitAudioManager.CurrentExhibitStory.Length == 0) { return; }

        switch (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag)
        {
            case "Sword":
                currentPausingVoiceLines = swordPauseVoiceLines;
                currentUspausingVoiceLines = swordUnpauseVoiceLines;
                currentSkippingVoiceLines = swordSkipVoiceLines;
                break;
            case "Needles":
                currentPausingVoiceLines = needlesPauseVoiceLines;
                currentUspausingVoiceLines = needlesUnpauseVoiceLines;
                currentSkippingVoiceLines = needlesSkipVoiceLines;
                break;
            case "Tub":
                currentPausingVoiceLines = tubPauseVoiceLines;
                currentUspausingVoiceLines = tubUnpauseVoiceLines;
                currentSkippingVoiceLines = tubSkipVoiceLines;
                break;
            case "Sign":
                currentPausingVoiceLines = signPauseVoiceLines;
                currentUspausingVoiceLines = signUnpauseVoiceLines;
                currentSkippingVoiceLines = signSkipVoiceLines;
                break;
            case "Skull":
                currentPausingVoiceLines = skullPauseVoiceLines;
                currentUspausingVoiceLines = skullUnpauseVoiceLines;
                currentSkippingVoiceLines = skullSkipVoiceLines;
                break;
            case "Bank":
                currentPausingVoiceLines = bankPauseVoiceLines;
                currentUspausingVoiceLines = bankUnpauseVoiceLines;
                currentSkippingVoiceLines = bankSkipVoiceLines;
                break;
            default:
                Debug.LogWarning("Exhibit random voice lines was not assigned.");
                break;
        }
    }

    private void PlayRandomVoiceLine(AudioClip[] randomAudioClip, AudioAction audioAction)
    {
        int randomIndex = Random.Range(0, randomAudioClip.Length);

        if (!audioSource.isPlaying && !storyCompletionManager.IsEnd)
        {
            audioSource.PlayOneShot(randomAudioClip[randomIndex]);
        }

        switch (audioAction)
        {
            case AudioAction.Pause:
                StartCoroutine(PauseExhibitAudioThenWait());
                break;
            case AudioAction.Unpause:
                StartCoroutine(WaitThenUnpauseExhibitAudio());
                break;
            case AudioAction.Skip:
                StartCoroutine(WaitThenSkipExhibitAudio());
                break;
            default:
                Debug.LogWarning("No audio action was executed!!");
                break;
        }
    }

    IEnumerator PauseExhibitAudioThenWait()
    {
        yield return new WaitForSeconds(shortDelay);
        exhibitAudioManager.GetAudioSource.Pause();
        exhibitAudioManager.IsDisplayQuestions = false;
        DisableControlInteraction();

        yield return new WaitUntil(() => !audioSource.isPlaying);
        AllowControlInteraction();
    }

    IEnumerator WaitThenUnpauseExhibitAudio()
    {
        DisableControlInteraction();
        yield return new WaitUntil(() => !audioSource.isPlaying);

        yield return new WaitForSeconds(shortDelay);
        exhibitAudioManager.GetAudioSource.UnPause();
        exhibitAudioManager.IsDisplayQuestions = true;
        AllowControlInteraction();
    }

    IEnumerator WaitThenSkipExhibitAudio()
    {
        exhibitAudioManager.GetAudioSource.Stop();
        exhibitAudioManager.IsDisplayQuestions = false;
        DisableControlInteraction();
        yield return new WaitUntil(() => !audioSource.isPlaying);

        exhibitAudioManager.IsDisplayQuestions = true;
        AllowControlInteraction();
    }

    private void DisableControlInteraction()
    {
        playPauseButton.interactable = false;
        skipButton.interactable = false;
    }

    private void AllowControlInteraction()
    {
        playPauseButton.interactable = true;
        if (storyCompletionManager.IsInteractive)
        {
            skipButton.interactable = true;
        }
    }
}