using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

sealed class PlayerBattleStateSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    LayerMask layer;
    Camera camera;
    EcsFilter<Player, PlayerBattleState> battlePlayerFilter;
    //EcsFilter<Player> playerFilter;
    EcsFilter<Enemy> enemyFilter;
    const int ENEMY_SCAN_RADIUS = 10;

    public void Init()
    {
        layer = LayerMask.GetMask("Hitable");
        camera = Camera.main;
        EnemyData.onKillEnemyEvent += TryMoveToNextWP;
    }
    public void Destroy()
    {
        EnemyData.onKillEnemyEvent -= TryMoveToNextWP;
    }

    public void Run()
    {
        if (battlePlayerFilter.GetEntitiesCount() == 0 || !Input.GetMouseButtonDown(0)) return;
        RaycastHit hit;
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(mouseRay, out hit, layer))
        {
            ref var player = ref battlePlayerFilter.Get1(0);

            player.playerData.weapon.Shoot(hit.point);
            player.playerData.animator.SetTrigger("Shoot");
        }
    }

    void TryMoveToNextWP()
    {
        if (!EnemiesIsDead()) return;

        ref var player = ref battlePlayerFilter.Get1(0);
        var playerEntity = battlePlayerFilter.GetEntity(0);

        playerEntity.Del<PlayerBattleState>();
        playerEntity.Get<PlayerRunState>();
        player.MoveToNextWP();
    }

    bool EnemiesIsDead()
    {
        Debug.Log("kill check");
        ref var player = ref battlePlayerFilter.Get1(0);

        foreach (var enemyEntityIndex in enemyFilter)
        {
            ref var enemy = ref enemyFilter.Get1(enemyEntityIndex);
            float distanceToEnemy = (enemy.enemyData.transform.position - player.playerGO.transform.position).magnitude;

            if (enemy.enemyData.isAlive && distanceToEnemy < ENEMY_SCAN_RADIUS)
            {
                Debug.Log("false");
                //enemyFilter.GetEntity(enemyEntityIndex).Del<Enemy>();
                return false;
            }
        }
        return true;
    }

    void SwitchState()
    {
        ref var player = ref battlePlayerFilter.Get1(0);
        var playerEntity = battlePlayerFilter.GetEntity(0);

        player.playerData.animator.SetTrigger("Run");
        playerEntity.Del<PlayerBattleState>();
        playerEntity.Get<PlayerRunState>();
    }


}
