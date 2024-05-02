using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuideUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _guideText;

    private Sequence _sequence;

    public void ResetGuideText()
    {
        _guideText.text = string.Empty;
    }

    public void SetGuideText(string text, float time)
    {
        if (_guideText == null)
            return;

        if (string.IsNullOrEmpty(text))
            ResetGuideText();

        if (time == 0)
            _guideText.text = text;

        // Tween Guide Text
        _guideText.text = "";

        if (_sequence != null && _sequence.active)
            _sequence.Kill();

        _sequence = DOTween.Sequence();
        _sequence.Append(_guideText.DOText(text, time));

    }
}
