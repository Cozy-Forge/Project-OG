using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera cam;

    private CinemachineBasicMultiChannelPerlin perlin;

    [SerializeField]
    private Camera _minimapCamera;

    private void Awake()
    {
        Instance = this;
        cam = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator CameraShake(float shakeIntensity, float shakeTime)
    {
        perlin.m_AmplitudeGain = shakeIntensity; // �������� ����
        perlin.m_FrequencyGain = shakeIntensity; // �������� ���ļ�

        yield return new WaitForSeconds(shakeTime);

        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }

    public void SetMinimapCameraPostion(Vector3 worldPos)
    {
        worldPos.z = -10;
        if(_minimapCamera != null)
        {

            Debug.Log("�����");
            _minimapCamera.transform.position = worldPos;

        }
    }
}
