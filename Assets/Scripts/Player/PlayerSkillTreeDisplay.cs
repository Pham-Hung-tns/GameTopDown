using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTreeDisplay : MonoBehaviour
{
    public PlayerConfig playerConfig;

    void Start()
    {
        if (playerConfig == null) return;

        List<TechNode> skillNodes = playerConfig.GetSkillNodes();

        foreach (TechNode node in skillNodes)
        {
            Debug.Log($"Tech: {node.tech}, Cost: {node.researchCost}, Level: {node.level}");
        }
        // TODO: Hiển thị lên UI
        // Ví dụ: foreach (TechNode node in skillNodes)
        // {
        //     Debug.Log($"Tech: {node.tech}, Cost: {node.researchCost}, Level: {node.level}, Icon: {node.icon}");
        //     // Tạo UI element với icon, cost, level tại UIposition
        // }
    }
}