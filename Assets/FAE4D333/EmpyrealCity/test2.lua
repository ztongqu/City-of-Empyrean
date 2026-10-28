local PlayerModifier = require("#Celeste.Mod.EmpyrealCity.Module.PlayerModifier")
function onBegin()
	local a = player.Speed.X
	local t1 = {"a"}
	--helpers.say(arguments)
end

function onEnter(player)

	--player.Speed = player.Speed + Vector2(0.0, 200.0)
	PlayerModifier.SetSpeedX(player, 0, true)
	PlayerModifier.SetSpeedY(player, 0, true)

	if(helpers.getFlag("test1")) then
		helpers.setFlag("test1",false)
	else
		helpers.setFlag("test1",true)
	end
end

function onStay(player)
	PlayerModifier.SetCoyoteFrames(player, 5)
	PlayerModifier.SetSpeedX(player, player.Speed.X*1.1, false)
	PlayerModifier.FacingLeft(player)
end

function onEnd()

end