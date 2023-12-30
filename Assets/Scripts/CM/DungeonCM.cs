using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCM : MonoBehaviour
{
    public CinemachineVirtualCamera cmVC;

    private void Awake()
    {
        cmVC = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        cmVC.Follow = LevelManager.Instance.SelectedPlayer.transform;
    }
}
