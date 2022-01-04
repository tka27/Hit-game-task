using Leopotam.Ecs;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;


    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
        _systems
            .Add(new GameInitSystem())
            .Add(new PlayerIdleStateSystem())
            .Add(new PlayerRunStateSystem())
            .Add(new PlayerBattleStateSystem())


            .Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}
