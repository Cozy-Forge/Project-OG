using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUIManager : MonoBehaviour
{
    [Header("패널")]
    [SerializeField] GameObject _settingPanel;
    [SerializeField] GameObject _controlPanel;
    [SerializeField] GameObject _audioPanel;
    [SerializeField] GameObject _modePanel;

    [Header("오디오")]
    [SerializeField] AudioClip _btnClick;
    [SerializeField][Range(0f, 1f)] float volume;

    public void StartBtn(string s)
    {
        BtnSound();
        SceneManager.LoadScene(s);
    }

    public void SettingBtn()
    {
        BtnSound();
        _settingPanel.SetActive(!_settingPanel.activeSelf);
    }

    public void ControlBtn()
    {
        BtnSound();
        _controlPanel.SetActive(!_controlPanel.activeSelf);
    }

    public void AudioBtn()
    {
        BtnSound();
        _audioPanel.SetActive(!_audioPanel.activeSelf);
    }

    public void ModeBtn()
    {
        BtnSound();
        _modePanel.SetActive(!_modePanel.activeSelf);
    }

    public void QuitBtn()
    {
        BtnSound();
        Application.Quit();
    }

    public void BtnSound() => SoundManager.Instance.SFXPlay("Btn", _btnClick, volume);
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Input.GetKey(KeyCode.Z))
            SceneManager.LoadScene("UserTest");
    }
}