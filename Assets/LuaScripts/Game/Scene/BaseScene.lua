local BaseScene = BaseClass("BaseScene")

local function __init(self, conf)
    self.sConf = conf
    self.sceneLoadOp = nil
    self:OnCreate()
end

local function __delete(self)
    self:OnDestroy()
end

local function OnCreate(self)
    Logger.DInfo(string.format("正在创建场景 %s.", self.sConf.Name))
    local sceneUpdateName = string.format("Update_Scene_%s", self.sConf.Name)
    LuaMono:AddUpdater(sceneUpdateName, self)
end

local function OnEnter(self)
    Logger.DInfo(string.format("正在进入场景 %s..", self.sConf.Name))
end

local function OnLeave(self)
    Logger.DInfo(string.format("正在离开场景 %s", self.sConf.Name))
end

local function OnDestroy(self)
    Logger.DInfo(string.format("正在销毁场景 %s", self.sConf.Name))
    local sceneUpdateName = string.format("Update_Scene_%s", self.sConf.Name)
    LuaMono:RemoveUpdater(sceneUpdateName)
    self.sceneLoadOp = nil
end

BaseScene.__init = __init
BaseScene.__delete = __delete
BaseScene.OnCreate = OnCreate
BaseScene.OnEnter = OnEnter
BaseScene.OnLeave = OnLeave
BaseScene.OnDestroy = OnDestroy
return BaseScene