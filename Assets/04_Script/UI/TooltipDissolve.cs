using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipDissolve : MonoBehaviour
{
    [SerializeField] Material tooltipMat;
    [SerializeField] Material iconShellMat;
    [SerializeField] Material iconMat;
    [SerializeField] Material imageMat;
    [SerializeField] Material textMat;
    [SerializeField] float duration = 0.4f;
    [SerializeField] Ease ease = Ease.Linear;


    private readonly string sourceGlowFade = "_SourceGlowDissolveFade";
    private readonly string directionalGlowFade = "_DirectionalGlowFadeFade";
    private readonly string alphaFade = "_FullAlphaDissolveFade";

    private readonly float _tooltipDissolveStart = 1;
    private readonly float _tooltipDissolveEnd = 0f;

    private readonly float _iconShellDissolveStart = 1.6f;
    private readonly float _iconShellDissolveEnd = -0.2f;

    private readonly float _iconDissolveStart = 1.6f;
    private readonly float _iconDissolveEnd = -0.2f;

    private readonly float _imageDissolveStart = 1.6f;
    private readonly float _imageDissolveEnd = -0.2f;

    private readonly float _textDissolveStart = 0f;
    private readonly float _textDissolveEnd = 1f;

    DissolveParameters _tooltipParameters;
    DissolveParameters _iconShellParameters;
    DissolveParameters _iconParameters;
    DissolveParameters _imageParameters;
    DissolveParameters _textParameters;

    Sequence seq;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _tooltipParameters = new DissolveParameters(tooltipMat, _tooltipDissolveStart, _tooltipDissolveEnd, duration, ease, directionalGlowFade);
        _iconShellParameters = new DissolveParameters(iconShellMat, _iconShellDissolveStart, _iconShellDissolveEnd, duration, ease, sourceGlowFade);
        _iconParameters = new DissolveParameters(iconMat, _iconDissolveStart, _iconDissolveEnd, duration, ease, sourceGlowFade);
        _imageParameters = new DissolveParameters(imageMat, _imageDissolveStart, _imageDissolveEnd, duration, ease, directionalGlowFade);
        _textParameters = new DissolveParameters(textMat, _textDissolveStart, _textDissolveEnd, duration, ease, alphaFade);
    }

    public Sequence On()
    {
        seq.Kill();
        seq = DOTween.Sequence();

        seq.Append(Dissolver.Dissolve(_tooltipParameters, true));
        seq.Join(Dissolver.Dissolve(_iconShellParameters, true));
        seq.Join(Dissolver.Dissolve(_iconParameters, true));
        seq.Join(Dissolver.Dissolve(_imageParameters, true));
        seq.Join(Dissolver.Dissolve(_textParameters, true));
        return seq;  
    }

    public Sequence Off()
    {
        seq.Kill();
        seq = DOTween.Sequence();
        
        seq.Append(Dissolver.Dissolve(_tooltipParameters, false));
        seq.Join(Dissolver.Dissolve(_iconShellParameters, false));
        seq.Join(Dissolver.Dissolve(_iconParameters, false));
        seq.Join(Dissolver.Dissolve(_imageParameters, false));
        seq.Join(Dissolver.Dissolve(_textParameters, false));
        return seq;
    }
}