using System;

public class Turret : BaseItem
{
    public static event Action OnDestroyTurret;
    //child class 에서 각각 선언 static 이니까
    //부모에서 선언해서 자식에서 invoke해줄 수는 없음
    //부모에서 invoke해서 자식에서 이벤트 실행은 가능

    public override void FindNewTarget()
    {
        TargetTagName = "Enemy";
        base.FindNewTarget();
    }
    public override void Destroy()
    {
        OnDestroyTurret?.Invoke();

        base.Destroy();
    }

}
