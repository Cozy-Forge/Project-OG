using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuideQuest : MonoBehaviour
{
    public event Action OnQuestComplete;

    [Header("Guide Text")]
    public string guideText;
    public float guideTextTime;

    private TutorialManager _tutorialManager;
    private GuideUI _tutorialUI;

    protected virtual void Awake()
    {
        _tutorialManager = TutorialManager.Instance;

        if(_tutorialManager != null )
            _tutorialUI = _tutorialManager.TutorialUI;
    }

    public virtual void SetQuestSetting()
    {
        _tutorialUI.SetGuideText(guideText, guideTextTime);
    }

    public abstract bool IsQuestComplete();

    public virtual void QuestComplete()
    {
        OnQuestComplete?.Invoke();
    }

}
