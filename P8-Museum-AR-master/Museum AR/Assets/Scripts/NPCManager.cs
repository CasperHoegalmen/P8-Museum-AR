using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    ExhibitAudioManager exhibitAudioManager;
    float animationSpeed = 0.5f;

    public Animator NPCAnimator { get; set; }

    private void Awake()
    {
        NPCAnimator = GetComponent<Animator>();
        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();

        NPCAnimator.speed = animationSpeed;
    }

    public void ChangeExhibitNPC()
    {
        if (exhibitAudioManager.CurrentExhibitStory == null || exhibitAudioManager.CurrentExhibitStory.Length == 0) { return; }
  
        if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Sword")
        {
            NPCAnimator.SetTrigger("Sword");
        }
        else if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Needles")
        {
            NPCAnimator.SetTrigger("Needles");
        }
        else if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Tub")
        {
            NPCAnimator.SetTrigger("Tub");
        }
        else if(exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Sign")
        {
            NPCAnimator.SetTrigger("Sign");
        }
        else if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Skull")
        {
            NPCAnimator.SetTrigger("Skull");
        }
        else if(exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Bank")
        {
            NPCAnimator.SetTrigger("Bank");
        }
    }
}