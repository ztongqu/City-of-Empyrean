local PlayerModifier = require("#Celeste.Mod.EmpyrealCity.Module.PlayerModifier")

function onEnter(player)
	PlayerModifier.ToggleAutoJump(player)
end

function onStay(player)

end

function onEnd()

end

function onLeave(player)
    PlayerModifier.ToggleAutoJump(player)
end