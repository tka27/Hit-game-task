using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct Player
{
    public GameObject playerGO;
    public PlayerData playerData;
    public List<Transform> path;
    public NavMeshAgent agent;
    public int currentWaypointIndex;
    public void MoveToNextWP()
    {
        agent.SetDestination(path[currentWaypointIndex].position);
        path[currentWaypointIndex].gameObject.SetActive(false);
        currentWaypointIndex++;
        playerData.animator.SetTrigger("Run");
    }
}
public struct PlayerIdleState { }
public struct PlayerRunState { }
public struct PlayerBattleState { }

public struct Enemy
{
    public EnemyData enemyData;
}


