using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class CustomTrackableEventHandler : MonoBehaviour
{

    ExhibitAudioManager exhibitAudioManager;

    public enum TrackingStatusFilter
    {
        Tracked,
        Tracked_ExtendedTracked,
        Tracked_ExtendedTracked_Limited
    }

    /// <summary>
    /// A filter that can be set to either:
    /// - Only consider a target if it's in view (TRACKED)
    /// - Also consider the target if's outside of the view, but the environment is tracked (EXTENDED_TRACKED)
    /// - Even consider the target if tracking is in LIMITED mode, e.g. the environment is just 3dof tracked.
    /// </summary>
    public TrackingStatusFilter StatusFilter = TrackingStatusFilter.Tracked_ExtendedTracked_Limited;
    public UnityEvent OnTargetFound;
    public UnityEvent OnTargetLost;


    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    protected bool m_CallbackReceivedOnce = false;

    bool firstTrigger;

    //public bool FirstTrigger { set => firstTrigger = value; }

    protected virtual void Start()
    {
        firstTrigger = true;

        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
        }

    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.UnregisterOnTrackableStatusChanged(OnTrackableStatusChanged);
        }
    }

    void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult)
    {
        m_PreviousStatus = statusChangeResult.PreviousStatus;
        m_NewStatus = statusChangeResult.NewStatus;

        Debug.LogFormat("Trackable {0} {1} -- {2}",
            mTrackableBehaviour.TrackableName,
            mTrackableBehaviour.CurrentStatus,
            mTrackableBehaviour.CurrentStatusInfo);

        HandleTrackableStatusChanged();
    }

    protected virtual void HandleTrackableStatusChanged()
    {
        if (!ShouldBeRendered(m_PreviousStatus) &&
            ShouldBeRendered(m_NewStatus))
        {
            OnTrackingFound();
        }
        else if (ShouldBeRendered(m_PreviousStatus) &&
                 !ShouldBeRendered(m_NewStatus))
        {
            OnTrackingLost();
        }
        else
        {
            if (!m_CallbackReceivedOnce && !ShouldBeRendered(m_NewStatus))
            {
                // This is the first time we are receiving this callback, and the target is not visible yet.
                // --> Hide the augmentation.
                OnTrackingLost();
            }
        }

        m_CallbackReceivedOnce = true;
    }

    protected bool ShouldBeRendered(TrackableBehaviour.Status status)
    {
        if (status == TrackableBehaviour.Status.DETECTED ||
            status == TrackableBehaviour.Status.TRACKED)
        {
            // always render the augmentation when status is DETECTED or TRACKED, regardless of filter
            return true;
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                // also return true if the target is extended tracked
                return true;
            }
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked_Limited)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED ||
                status == TrackableBehaviour.Status.LIMITED)
            {
                // in this mode, render the augmentation even if the target's tracking status is LIMITED.
                // this is mainly recommended for Anchors.
                return true;
            }
        }

        return false;
    }

    protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            if (ImageTargetController.CurrentImageTarget == 0 && firstTrigger)
            {
                exhibitAudioManager.TriggerTubStory = true;
                firstTrigger = false;
            }
            else if (ImageTargetController.CurrentImageTarget == 1 && firstTrigger)
            {
                exhibitAudioManager.TriggerSwordStory = true;
                firstTrigger = false;
            }
            else if (ImageTargetController.CurrentImageTarget == 2 && firstTrigger)
            {
                exhibitAudioManager.TriggerSkullStory = true;
                firstTrigger = false;
            }
            else if (ImageTargetController.CurrentImageTarget == 3 && firstTrigger)
            {
                exhibitAudioManager.TriggerSignStory = true;
                firstTrigger = false;
            }
            else if (ImageTargetController.CurrentImageTarget == 4 && firstTrigger)
            {
                exhibitAudioManager.TriggerBankStory = true;
                firstTrigger = false;
            }
            else if (ImageTargetController.CurrentImageTarget == 5 && firstTrigger)
            {
                exhibitAudioManager.TriggerNeedlesStory = true;
                firstTrigger = false;
            }


            foreach (var highlight in HighlightController.Highlights)
            {
                Debug.Log("COMPONENTS LENGTH: " + rendererComponents.Length.ToString());
                Debug.Log("DEBUG: " + highlight.ToString());
                rendererComponents[highlight].enabled = true;
            }



        }

        if (OnTargetFound != null)
            OnTargetFound.Invoke();
    }

    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        };

        if (OnTargetLost != null)
            OnTargetLost.Invoke();
    }
}
