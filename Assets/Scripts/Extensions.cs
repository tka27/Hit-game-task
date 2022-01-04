using UnityEngine;

public static class Extensions
{
    public static void HideRB(this Rigidbody rb)
    {
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.isKinematic = true;
        rb.gameObject.SetActive(false);
    }
}
