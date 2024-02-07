using Cinemachine;
using UnityEngine;

public class DungeonCM : Singleton<DungeonCM>
{
    public CinemachineVirtualCamera cmVC;
    private float timer;
    private float shakeTimeTotal;
    private float startingIntensity;

    protected override void Awake()
    {
        base.Awake();
        cmVC = GetComponent<CinemachineVirtualCamera>();

    }
    private void Start()
    {
        cmVC.Follow = LevelManager.Instance.SelectedPlayer.transform;
    }
    public void ShakeCM(float intensity, float shakeTimer)
    {
        CinemachineBasicMultiChannelPerlin obj = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        obj.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        timer = shakeTimer;
        shakeTimeTotal = shakeTimer;
    }

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin obj = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            obj.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1- (timer / shakeTimeTotal));

        }
    }

}
