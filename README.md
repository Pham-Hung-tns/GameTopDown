## GameTopDown

### Pipeline khởi động level & sinh dungeon

- **Entry**: Scene game chính nên có `GameManager`, `LevelManager`, `DungeonBuilder`, `EnemySpawner` (global), cùng asset `Resources/GameResources`.
- **LevelManager**:
  - Thuộc tính `startingDungeonLevel` trỏ tới một `DungeonLevelSO`.
  - `Awake`: spawn player từ `GameManager.playerPrefab`.
  - `Start`: gọi `DungeonBuilder.GenerateDungeon(startingDungeonLevel)` và đặt player vào phòng entrance (lấy qua `DungeonBuilder.GetEntranceRoom()`).

### Cấu hình DungeonLevelSO

- Mở một asset `DungeonLevelSO`:
  - **roomTemplateList**: thêm tất cả `RoomTemplateSO` sẽ xuất hiện trong level này (đảm bảo có đủ loại: Entrance, CorridorEW, CorridorNS, Normal, Boss…).
  - **roomNodeGraphList**: danh sách các `RoomNodeGraphSO` (layout graph); khi chạy sẽ random một graph trong danh sách.

### Cấu hình RoomTemplateSO (enemy & spawn vị trí)

- Trong mỗi `RoomTemplateSO`:
  - **prefab**: prefab phòng đầy đủ tilemap (ground, collision, minimap…).
  - **lowerBounds / upperBounds**: toạ độ grid bao ngoài tilemap (local).
  - **doorwayList**: danh sách `Doorway` (vị trí & hướng cửa).
  - **spawnPositionArray**:
    - Mảng `Vector2Int` (tọa độ grid cục bộ trên tilemap).
    - Các ô này phải là ô **walkable** (không nằm trên collision tile dành cho tường/chướng ngại).
    - Dùng chung cho cả spawn enemy và chest.
  - **enemiesByLevelList** (`List<SpawnableObjectsByLevel<EnemyDetailsSO>>`):
    - Mỗi phần tử gắn với một `DungeonLevelSO`.
    - Trong đó có `spawnableObjectRatioList` chứa nhiều `SpawnableObjectRatio<EnemyDetailsSO>`:
      - `dungeonObject`: trỏ tới một `EnemyDetailsSO`.
      - `ratio`: trọng số spawn (tăng giảm để test tỉ lệ xuất hiện).
  - **roomEnemySpawnParametersList** (`List<RoomEnemySpawnParameters>`):
    - Mỗi phần tử gắn với một `DungeonLevelSO`.
    - **minTotalEnemiesToSpawn / maxTotalEnemiesToSpawn**: tổng số lượng quái spawn trong phòng (random trong khoảng này).
    - **minConcurrentEnemies / maxConcurrentEnemies**: số quái tồn tại đồng thời (concurrent), EnemySpawner sẽ không spawn thêm nếu đang đạt trần.
    - **minSpawnInterval / maxSpawnInterval** (giây): khoảng delay giữa mỗi lần spawn (random trong khoảng này).

### Cấu hình EnemyDetailsSO

- Tạo asset `EnemyDetailsSO` trong menu: `Scriptable Objects/Enemy/Enemy Details`:
  - **enemyPrefab**: prefab enemy (có `EnemyController`, `EnemyVitality`, vũ khí…).
  - **chaseDistance**: khoảng cách detect đuổi theo player (được AI dùng).
  - **weapon**: `WeaponData` nếu enemy có bắn.
  - **firingIntervalMin/Max, firingDurationMin/Max, firingLineOfSightRequired**: tham số bắn (sẵn để AI/combat logic dùng).
  - **healthByLevel** (`EnemyHealthDetails[]`):
    - Mỗi phần tử map `DungeonLevelSO` -> `health`.
    - Khi spawn, `EnemySpawner` chọn health phù hợp level; nếu không có thì giữ health mặc định từ `EnemyVitality`.

### Hệ event & vòng đời Room

- `InstantiatedRoom`:
  - Có `BoxCollider2D` dạng trigger bao quanh phòng.
  - `OnTriggerEnter2D`:
    - Khi player (tag `Player`) vào phòng → `room.isPreviouslyVisited = true`, gọi `StaticEventHandler.CallRoomChangedEvent(room)`.
- `StaticEventHandler`:
  - `OnRoomChanged` (`RoomChangedEventArgs`) – bắn khi player vào phòng.
  - `OnRoomEnemiesDefeated` (`Room`) – bắn khi phòng đã clear hết enemy.

### EnemySpawner – luồng spawn quái

- Đặt một `EnemySpawner` trong scene (ví dụ trên một empty GameObject `Systems`).
- Logic chính (`EnemySpawner.cs`):
  - Lắng nghe `StaticEventHandler.OnRoomChanged`:
    - Bỏ qua **corridor**, **entrance**, hoặc phòng đã `isClearedOfEnemies == true`.
    - Lấy `DungeonLevelSO` hiện tại từ `LevelManager.Instance.GetCurrentDungeonLevel()`.
    - Lấy `RoomEnemySpawnParameters` & `spawnPositionArray` từ `Room`.
    - Tạo `SpawnState` và bắt đầu coroutine `SpawnRoutine`.
  - **SpawnRoutine**:
    - Random `totalToSpawn` trong `[minTotalEnemiesToSpawn, maxTotalEnemiesToSpawn]`.
    - Random `maxConcurrent` trong `[minConcurrentEnemies, maxConcurrentEnemies]`.
    - Vòng lặp:
      - Chờ cho đến khi `AliveCount < maxConcurrent`.
      - Gọi `SpawnEnemy` → tăng `AliveCount`.
      - Đợi `interval` random trong `[minSpawnInterval, maxSpawnInterval]`.
    - Sau khi spawn đủ:
      - Chờ đến khi `AliveCount == 0`.
      - Đánh dấu `room.isClearedOfEnemies = true`, gọi `StaticEventHandler.CallRoomEnemiesDefeated(room)`.
  - **SpawnEnemy**:
    - Chọn loại quái bằng weighted random từ `room.enemiesByLevelList` (theo level hiện tại).
    - Chọn 1 ô random từ `spawnPositionArray`, convert sang world bằng `grid.CellToWorld` + offset (0.5, 0.5).
    - Instantiate prefab dưới `room.instantiatedRoom.transform`.
    - Tìm `EnemyVitality` và gán health theo `EnemyDetailsSO.healthByLevel` (nếu cấu hình).
    - Thêm component `SpawnContext` để biết enemy thuộc phòng nào.
  - **HandleEnemyKilled**:
    - Lắng nghe `EnemyVitality.OnEnemyKilledEvent`.
    - Lấy `SpawnContext.SourceRoom`, giảm `AliveCount` tương ứng.

> **Kiểm thử nhanh**  
> - Vào một **room thường** (không phải entrance/corridor) có cấu hình spawn:
>   - Quan sát kẻ địch spawn lần lượt theo `concurrent` và `interval` đã set trong `RoomEnemySpawnParameters`.
>   - Khi giết hết quái: event `OnRoomEnemiesDefeated` được bắn, `room.isClearedOfEnemies` = true.  
> - Có thể quan sát/tune tỉ lệ quái bằng cách chỉnh `ratio` trong `enemiesByLevelList` của `RoomTemplateSO`:
>   - Tăng `ratio` của một `EnemyDetailsSO` → thấy loại đó xuất hiện nhiều hơn.

> **Lưu ý**: Hiện tại việc **lock/mở cửa vật lý** (Door prefab) chưa được nối lại với event spawn – behavior này phụ thuộc vào script `Door`/collider trong prefab. Bạn có thể dùng `OnRoomChanged` để:
> - Gọi `instantiatedRoom.LockDoors()` trước khi spawn (khi có sẵn logic).
> - Sau `OnRoomEnemiesDefeated` thì unlock và chuyển lại ambient music.

### ChestSpawner – luồng spawn rương & loot

- Thêm `ChestSpawner` làm child trong prefab phòng (hoặc prefab riêng).
- Cấu hình trong Inspector:
  - **chestPrefab**: prefab `Chest` hiện tại (sử dụng script `Chest` để hiển thị loot khi mở).
  - **chestSpawnChanceMin / chestSpawnChanceMax**: khoảng xác suất spawn rương (0–1).  
    - Ví dụ: 0.3–0.6 → mỗi lần vào/clear phòng sẽ roll ngẫu nhiên trong khoảng đó.
  - **chestSpawnEvent**:
    - `OnRoomEntry`: rương roll & spawn ngay khi player vào phòng (lắng nghe `OnRoomChanged`).
    - `OnEnemiesDefeated`: rương chỉ spawn sau khi quái trong phòng chết hết (lắng nghe `OnRoomEnemiesDefeated`).
  - **chestSpawnPosition**:
    - `AtSpawnerPosition`: spawn ngay tại transform của `ChestSpawner`.
    - `AtPlayerPosition`: spawn tại vị trí player lúc sự kiện xảy ra.
  - **Loot fields** (`weaponSpawnByLevel`, `healthSpawnByLevel`, `ammoSpawnByLevel`, `numberOfItemsToSpawnMin/Max`):
    - Các struct đã sẵn sàng để bạn nối với hệ loot thực tế (ví dụ tạo item từ `WeaponData`, heal % máu, ammo %).
    - Mặc định, logic loot vẫn do script `Chest` hiện tại xử lý (spawn 1 predefined item hoặc `RandomItemInEachChest` từ `LevelManager`).

> **Kiểm thử nhanh Chest**  
> - Với `ChestSpawnEvent = OnRoomEntry`:
>   - Vào phòng → nếu random roll trúng thì thấy rương xuất hiện tại spawner/player đúng như `ChestSpawnPosition`.  
> - Với `ChestSpawnEvent = OnEnemiesDefeated`:
>   - Vào phòng, giết hết quái → rương spawn sau khi phòng clear.  
> - Chỉnh `chestSpawnChanceMin/Max` (ví dụ 0.9–1.0 vs 0–0.1) để kiểm thử xác suất xuất hiện thay đổi như mong đợi.

### Checklist để scene chạy đúng

- `GameResources` đặt trong `Resources/GameResources`, đã cấu hình:
  - `roomNodeTypeList`, materials, enemy tiles, preferred path tile, mixer groups…
- Trong scene:
  - `GameManager` (có `playerPrefab`, màu weapon, lưu/đọc `GameData`).
  - `LevelManager` (set `startingDungeonLevel`).
  - `DungeonBuilder`.
  - `EnemySpawner` (global).
  - (Tuỳ chọn) các `ChestSpawner` trong prefab phòng.

Với cấu hình trên, bạn có thể:
- Điều chỉnh tỉ lệ spawn enemy bằng `SpawnableObjectsByLevel<EnemyDetailsSO>.ratio`.
- Điều chỉnh tổng số & tốc độ spawn bằng `RoomEnemySpawnParameters`.
- Thử 2 chế độ spawn rương (`OnRoomEntry` / `OnEnemiesDefeated`) và xác suất min/max để xác nhận behavior mong muốn.