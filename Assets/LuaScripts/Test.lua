local Test = {}

local function XLuaCoTest(self)
    local co
    local t_fun = XLuaUtil.cs_generator(function()
        print("StartCoroutine ")
        for i = 1, 10 do
            coroutine.yield(CS.UnityEngine.WaitForSeconds(1))
            print('Wait for 1 seconds')

            if i == 3 then
                print("StopCoroutine")
                print(co)
                LuaMono:StopCoroutine(co)
            end
        end
    end)

    co = CS.XLua.Cast.IEnumerator(t_fun)
    LuaMono:StartCoroutine(co)
end


Test.XLuaCoTest = XLuaCoTest
return Test

