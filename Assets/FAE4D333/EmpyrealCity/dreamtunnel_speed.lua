
function onBegin()
	local speed_x = player.Speed.X
	player.Speed.X = speed_x + 300
end
--[[
function onStay(player)
	local state = helpers.getPlayerState()
	local speed_x = player.Speed.X
	local speed_y = player.Speed.Y
	player.Speed.Y = -240
	if(state == 2) then
		player.Speed.X = speed_x
		player.Speed.Y = speed_y
		helpers.miniTextbox(speed_x)
		helpers.endCutscene()
	end
end
--]]

function onEnd()

end