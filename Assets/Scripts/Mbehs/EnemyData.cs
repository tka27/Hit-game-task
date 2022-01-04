using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public bool isAlive { get; private set; }
    [SerializeField] Animator animator;
    [SerializeField] List<Rigidbody> ragdoll;
    public static event Action onKillEnemyEvent;

    private void Awake()
    {
        Projectile.onEnemyHitEvent += CheckRagdoll;
        SceneData.singleton.enemies.Add(this);
    }

    private void OnDestroy()
    {
        Projectile.onEnemyHitEvent -= CheckRagdoll;
    }

    private void Start()
    {
        SwitchComponents(true);
    }

    public void Kill()
    {
        SwitchComponents(false);
        onKillEnemyEvent.Invoke();
    }

    void SwitchComponents(bool isAlive)
    {
        this.isAlive = isAlive;
        foreach (var rb in ragdoll)
        {
            rb.isKinematic = isAlive;
        }
        animator.enabled = isAlive;
    }

    void CheckRagdoll(Rigidbody rb)
    {
        if (!isAlive) return;

        foreach (var ragdollRB in ragdoll)
        {
            if (rb == ragdollRB) Kill();
        }
    }
}
