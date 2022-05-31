using System;

public class GameUpdateManager: Singleton<GameUpdateManager>
{
    protected override void Init()
    {
        
    }

    public void Check(Action callback)
    {
        callback();
    }
}
