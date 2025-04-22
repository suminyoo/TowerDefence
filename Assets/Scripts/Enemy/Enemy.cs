using System;


public class Enemy : BaseItem
{
    public static event Action OnDestroyEnemy;

    public override void FindNewTarget()
    {
        Target = "Turret";
        base.FindNewTarget();
    }

    public override void Destroy()
    {
        OnDestroyEnemy?.Invoke();
        base.Destroy();
    }

}
