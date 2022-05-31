local LoginScene = BaseClass("LoginScene", BaseScene)
local base = BaseScene

local function OnCreate(self)
    base.OnCreate(self)
end

local function OnEnter(self)
    base.OnEnter(self)
end

local function OnLeave(self)
    base.OnLeave(self)
end

local function OnDestroy(self)
    base.OnDestroy(self)
end


LoginScene.OnCreate = OnCreate
LoginScene.OnEnter = OnEnter
LoginScene.OnLeave = OnLeave
LoginScene.OnDestroy = OnDestroy
return LoginScene