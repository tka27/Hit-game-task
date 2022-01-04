using Leopotam.Ecs;
using UnityEngine.AI;
using UnityEngine;

sealed class GameInitSystem : IEcsInitSystem
{
    readonly EcsWorld _world = null;

    public void Init()
    {
        PlayerInit();
        EnemiesInit();
    }

    void PlayerInit()
    {
        var playerEntity = _world.NewEntity();
        ref var player = ref playerEntity.Get<Player>();
        player.playerGO = SceneData.singleton.playerGO;
        player.path = SceneData.singleton.waypoints;
        player.agent = player.playerGO.GetComponent<NavMeshAgent>();
        player.playerData = player.playerGO.GetComponent<PlayerData>();


        playerEntity.Get<PlayerIdleState>();

    }
    void EnemiesInit()
    {
        foreach (var enemyData in SceneData.singleton.enemies)
        {
            _world.NewEntity().Get<Enemy>().enemyData = enemyData;
        }
    }
}
