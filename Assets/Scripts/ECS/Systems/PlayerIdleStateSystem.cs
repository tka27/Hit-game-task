using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

sealed class PlayerIdleStateSystem : IEcsRunSystem
{
    EcsFilter<Player, PlayerIdleState> playerFilter;
    public void Run()
    {
        if (playerFilter.GetEntitiesCount() > 0 && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ref var player = ref playerFilter.Get1(0);
            player.MoveToNextWP();

            var playerEntity = playerFilter.GetEntity(0);

            playerEntity.Del<PlayerIdleState>();
            playerEntity.Get<PlayerRunState>();
        }
    }
}
