using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCollision : MonoBehaviour
{
    public LayerMask layerMask;
    public float radiusRange;

    private bool isGround;

    private void Update()
    {
        var platform = Physics.OverlapSphere(transform.position, radiusRange,layerMask);

        if (platform.Length > 0)
            isGround = true;
        else
            isGround = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusRange);
    }
    public bool IsGround()
    {
        return isGround;
    }

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }
}
