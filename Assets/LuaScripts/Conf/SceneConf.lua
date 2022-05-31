local SceneConf = {
    GameLaunch = {
        Level = 0,
        Name = "GameLaunch",
        Type = nil
    },
    Login = {
        Level = 1,
        Name = 'Login',
        Type = require "Game.Scene.LoginScene"
    },
    Main = {
        Level = 2,
        Name = "Main",
        Type = nil
    },
    Fight = {
        Level = 3,
        Name = "Fight",
        Type = nil
    }
}

return ConstClass("SceneConf", SceneConf)