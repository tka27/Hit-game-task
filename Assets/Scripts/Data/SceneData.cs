using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData singleton { get; private set; }
    public GameObject playerGO;
    public List<Transform> waypoints;
    public List<EnemyData> enemies;
    public ParticleSystem hitParticles;
    [SerializeField] GameObject canvasText;


    private void Awake()
    {
        singleton = this;
    }

    public void HideCanvasText()
    {
        canvasText.SetActive(false);
    }
}
