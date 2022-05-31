Main = {}

local function Start()
    --Test:XLuaCoTest()
    Logger.Info("初始化游戏完成，交给Lua执行游戏逻辑")
    -- 加载登录场景
    SceneManager:GetInstance():SwitchScene(SceneConf.Login)
end

local function OnApplicationQuit()
    
end

Start()

Main.OnApplicationQuit = OnApplicationQuit
return Main