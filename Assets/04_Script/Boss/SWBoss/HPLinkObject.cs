using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPLinkObject : MonoBehaviour, IHitAble
{
    public FeedbackPlayer feedbackPlayer { get; set; }

    private IHitAble _linkObject;
    [SerializeField]
    private float _damagePer = 0.5f;

    private void Awake()
    {
        feedbackPlayer = GetComponent<FeedbackPlayer>();
    }

    public bool Hit(float damage)
    {
        feedbackPlayer?.Play(damage * _damagePer);

        if (_linkObject != null) 
        { 
            _linkObject.Hit(damage * _damagePer);
        }
        return true;
    }

    public void Link(IHitAble linkObj)
    {
        _linkObject = linkObj;
    }

}