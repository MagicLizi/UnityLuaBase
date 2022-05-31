public class GameSingleManager : MonoSingleton<GameSingleManager>
{
    protected override void Init()
    {
        base.Init();
        //(1) 初始化Loom
        gameObject.AddComponent<Loom>();
        //(2) 初始化AppState
        gameObject.AddComponent<GameAppState>();
    }
}
