using UnityEditor;
using UnityEngine;

public class TestDungeonBuilderEditor
{

    [MenuItem("Tools/Test Dungeon Builder")]
    public static void TestDungeonBuilder()
    {
        // Tìm DungeonBuilder trong scene
        DungeonBuilder dungeonBuilder = GameObject.FindObjectOfType<DungeonBuilder>();
        if (dungeonBuilder == null)
        {
            Debug.LogError("Không tìm thấy DungeonBuilder trong scene!");
            return;
        }

        // Tìm DungeonLevelSO để test (bạn có thể gán cứng đường dẫn hoặc chọn asset)
        DungeonLevelSO dungeonLevel = Resources.Load<DungeonLevelSO>("DungeonLevel_1-2");
        if (dungeonLevel == null)
        {
            Debug.LogError("Không tìm thấy DungeonLevelSO trong Resources!");
            return;
        }

        // Gọi hàm tạo map
        dungeonBuilder.LoadRoomNodeTypeList();
        bool result = dungeonBuilder.GenerateDungeon(dungeonLevel);
        Debug.Log("Kết quả tạo dungeon: " + result);
    }
}