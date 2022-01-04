using Leopotam.Ecs;
using UnityEngine;

sealed class PlayerRunStateSystem : IEcsRunSystem
{
    EcsFilter<Player, PlayerRunState> playerFilter;
    const float DESTINATION_POINT_RADIUS = .5f;
    public void Run()
    {
        foreach (var playerEntityIndex in playerFilter)
        {
            ref var player = ref playerFilter.Get1(playerEntityIndex);

            if (player.agent.remainingDistance > DESTINATION_POINT_RADIUS || player.agent.pathPending) return;

            player.playerData.animator.SetTrigger("Idle");

            var playerEntity = playerFilter.GetEntity(playerEntityIndex);
            playerEntity.Del<PlayerRunState>();
            playerEntity.Get<PlayerBattleState>();
        }
    }
}
