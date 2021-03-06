using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelTargetController : MonoBehaviour
{
    [SerializeField] GameObject modelTarget;

    ModelTargetBehaviour mModelTarget;

    ObjectTracker objectTracker;

    string[] dataSetNames;

    int guideViewCounter;
    int dataSetCounter;

    void Start()
    {
        mModelTarget = modelTarget.GetComponent<ModelTargetBehaviour>();

        guideViewCounter = 0;
        dataSetCounter = 0;

        dataSetNames = new string[2]{ "Spinner", "iPhone6_Color" };

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(InitializeObjectTracker);
    }

    private void InitializeObjectTracker()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
    }

    void Update()
    {
        SwitchDatabase(dataSetNames);
        SwitchGuideView();
    }

    private void SwitchDatabase(string[] dataSetsToBeActivated)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            IEnumerable<DataSet> activeDataSets = objectTracker.GetActiveDataSets();
            List<DataSet> activeDataSetsToBeRemoved = new List<DataSet>();
            activeDataSetsToBeRemoved.AddRange(activeDataSets);

            foreach (DataSet set in activeDataSetsToBeRemoved)
            {
                objectTracker.DeactivateDataSet(set);
            }

            objectTracker.Stop();


            DataSet dataSet = objectTracker.CreateDataSet();

            if (DataSet.Exists(dataSetNames[dataSetCounter]))
            {
                dataSet.Load(dataSetNames[dataSetCounter]);
                objectTracker.ActivateDataSet(dataSet);

                if (dataSetCounter == dataSetNames.Length - 1)
                {
                    dataSetCounter = 0;
                }
                else
                {
                    dataSetCounter++;
                }
            }


            IEnumerable<TrackableBehaviour> trackableBehaviours = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
            foreach (TrackableBehaviour tb in trackableBehaviours)
            {
                if (tb is ModelTargetBehaviour && tb.isActiveAndEnabled)
                {
                    Debug.Log("TrackableName: " + tb.TrackableName);
                    (tb as ModelTargetBehaviour).GuideViewMode = ModelTargetBehaviour.GuideViewDisplayMode.GuideView2D;
                    mModelTarget = tb.GetComponent<ModelTargetBehaviour>();
                }
            }

            objectTracker.Start();

        }
    }

    private void SwitchGuideView()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (guideViewCounter == mModelTarget.ModelTarget.GetNumGuideViews() - 1)
            {
                guideViewCounter = 0;
            }
            else
            {
                guideViewCounter++;
            }

            mModelTarget.ModelTarget.SetActiveGuideViewIndex(guideViewCounter);

        }
    }
}
