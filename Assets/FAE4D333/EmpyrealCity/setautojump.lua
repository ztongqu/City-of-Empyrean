local PlayerModifier = require("#Celeste.Mod.EmpyrealCity.Module.PlayerModifier")

function onBegin()
end
function onEnter(player)

end

function onStay(player)
    PlayerModifier.SetAutoJump(player, true)
end

function onEnd()
    
end