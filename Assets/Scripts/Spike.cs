using UnityEngine;

public class Spike : MoveableObject
{
    [Header("Events")]
    public EventChannel eventPlayerDead;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCollision>())
            eventPlayerDead.Invoke(new Empty());

    }
}
