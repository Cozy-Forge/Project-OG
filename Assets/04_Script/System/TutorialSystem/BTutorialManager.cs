using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTutorialManager : MonoSingleton<BTutorialManager>
{
    [Header("Guide UI Info")]
    [SerializeField]
    private GuideUI _guideUI;
    public GuideUI TutorialUI => _guideUI;

    [Header("Guide Line")]
    [SerializeField]
    private Transform _guideRoot;
    private List<GuideQuest> _guideLine;
    private int _currentGuideIndex = 0;

    private GuideQuest _currentGuide;
    private bool _tutorialClear = false;

    private void Awake()
    {
        if (_guideRoot == null)
        {
            Debug.LogError("TutorialManager's GuideRoot is null");
            return;
        }

        _guideLine = _guideRoot.GetComponentsInChildren<GuideQuest>().ToList<GuideQuest>();

        if (_guideLine.Count <= 0)
        {
            Debug.LogError("GuideRoot does not have Quests");
            return;
        }

        _currentGuide = _guideLine[_currentGuideIndex];
        _currentGuide?.SetQuestSetting();
    }

    private void LateUpdate()
    {

        if (_tutorialClear)
            return;

        if (_currentGuide.IsQuestComplete())
        {
            _currentGuide.QuestComplete();

            if (_currentGuideIndex == _guideLine.Count - 1)
            {
                _tutorialClear = true;
                enabled = false;
                return;
            }

            _currentGuide = _guideLine[++_currentGuideIndex];
            _currentGuide.SetQuestSetting();
        }

    }


}
