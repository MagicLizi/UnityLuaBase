local UnitySceneManager = UnityEngine.SceneManagement.SceneManager
local SceneManager = BaseClass("SceneManager", Singleton)

local function __init(self)
    self.curScene = nil;
    self.isLoading = false;
    LuaMono:AddUpdater("SceneManager", self)
end

local function __delete(self)
    LuaMono:RemoveUpdater("SceneManager")
end

local function SwitchScene(self, sceneConf)
    Logger.Info(string.format("尝试加载 %s 场景", sceneConf.Name))
    if self.isLoading == false then
        self.isLoading = true
        -- 清理上个场景
        if self.curScene ~= nil then
            self.curScene:OnLeave()
            self.curScene = nil
        end
        
        -- 创建新场景对象
        self.curScene = SceneConf.Login.Type.New(sceneConf)
        self.curScene:OnEnter()
        
        -- 调用 Unity 异步加载场景
        local loadSceneFunc = XLuaUtil.cs_generator(function()
            self.curScene.sceneLoadOp = UnitySceneManager.LoadSceneAsync(sceneConf.Name)
            coroutine.yield(self.curScene.sceneLoadOp)
        end)
        LuaMono:StartCoroutine(CS.XLua.Cast.IEnumerator(loadSceneFunc))
        
    else
        Logger.Error("有场景正在加载中！请稍后再试！")
    end
end

local function OnUpdate(self,deltaTime, unscaledDeltaTime)
    if  self.curScene ~= nil and self.curScene.sceneLoadOp ~= nil then
        if(not self.curScene.sceneLoadOp.isDone) then
            Logger.Info(self.curScene.sceneLoadOp.progress)
        else
            Logger.Info(string.format("场景 %s 加载完成", self.curScene.sConf.Name))
            self.curScene.sceneLoadOp = nil
        end
    end
end


SceneManager.__init = __init
SceneManager.__delete = __delete
SceneManager.SwitchScene = SwitchScene
SceneManager.OnUpdate = OnUpdate
return SceneManager


