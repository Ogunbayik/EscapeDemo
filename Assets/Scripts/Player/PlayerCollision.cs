using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Check Settings")]
    public LayerMask groundLayer;
    public float radiusRange;

    private bool isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var platform = Physics.OverlapSphere(transform.position, radiusRange,groundLayer);

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
    public void StoppedPlayer()
    {
        //Need to refresh gravity for player
        rb.linearVelocity = Vector3.zero;
    }
    public void Dead()
    {
        //Activate Dead animation
    }

}
