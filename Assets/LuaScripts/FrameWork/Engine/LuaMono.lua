local LuaMono = {
   UpdaterList = {}
}

function StartCoroutine(self, co)
   Mono:StartCoroutine(co) 
end

function StopCoroutine(self,co)
   Mono:StopCoroutine(co)
end

function Update(self,deltaTime, unscaledDeltaTime)
   for _,v in pairs(self.UpdaterList) do
      if v.OnUpdate ~= nil then
         v:OnUpdate(deltaTime, unscaledDeltaTime)
      end
   end
end

function LateUpdate(self)

end

function FixedUpdate(self,fixedDeltaTime)

end

-- 一般来说只要实现 OnUpdate方法就行了,懒的再写个类去继承了
function AddUpdater(self, name, updater)
   if self.UpdaterList[name] ~= nil then
      Logger.Error(string.format("当前已经存在key为%s 的Updater了", name))
   else
      Logger.Info(string.format("添加Updater %s", name))
      self.UpdaterList[name] = updater
   end
end

function RemoveUpdater(self, name)
   self.UpdaterList[name] = nil
end

function YieldReturn(self, obj)
   --Logger.Info(Spb(Mono.YieldReturn))
   Logger.Info(string.format("lua yield return " .. obj))
   return Mono:YieldReturn(obj)
end

LuaMono.Update = Update
LuaMono.LateUpdate = LateUpdate
LuaMono.FixedUpdate = FixedUpdate
LuaMono.StartCoroutine = StartCoroutine
LuaMono.StopCoroutine = StopCoroutine
LuaMono.AddUpdater = AddUpdater
LuaMono.RemoveUpdater = RemoveUpdater
LuaMono.YieldReturn = YieldReturn
return LuaMono