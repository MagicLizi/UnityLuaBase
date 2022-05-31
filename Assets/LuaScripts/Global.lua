-- common
require "FrameWork.Util.LuaUtil"
require "FrameWork.Util.TableUtil"
require "FrameWork.Util.StringUtil"
require "FrameWork.Util.List"
require "Framework.Base.BaseClass"
require "Framework.Base.ConstClass"
Singleton = require "Framework.Base.Singleton"
Spb = (require "FrameWork.Util.Serpent").block
rapidjson = require 'rapidjson'
pb = require 'pb'
Logger = CS.Logger
XLuaUtil = require 'xlua.util'

-- 处理LuaMono
LuaMono = require "Framework.Engine.LuaMono"

function Update(deltaTime, unscaledDeltaTime)
    LuaMono:Update(deltaTime, unscaledDeltaTime)
end

function LateUpdate()
    LuaMono:LateUpdate()
end

function FixedUpdate(fixedDeltaTime)
    LuaMono:FixedUpdate(fixedDeltaTime)
end

-- Unity
UnityEngine = CS.UnityEngine
GFind = UnityEngine.GameObject.Find
Application = UnityEngine.Application

-- Game
Test = require "Test"
BaseScene = require "Game.Scene.BaseScene"
SceneConf = require "Conf.SceneConf"
SceneManager = require "Framework.Engine.SceneManager"