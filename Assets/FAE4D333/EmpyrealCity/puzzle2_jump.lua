function onBegin()
	local jump = 0
	for i=31, 0, -1 do
		if(helpers.getFlag("puzzle2_jump"..i)==true) then
			jump = jump + 2^i
		end
    end
    if(player.Position.X > 16106.0 and player.Position.X < 16118.0) then
    	if(jump > 999) then
    		jump = jump - 1000
    	end
    elseif(player.Position.X > 16138.0 and player.Position.X < 16150.0) then
    	if(jump > 99) then
    		jump = jump - 100
    	end
    elseif(player.Position.X > 16170.0 and player.Position.X < 16182.0) then
        if(jump > 9) then
            jump = jump - 10
        end
    elseif(player.Position.X > 16202.0 and player.Position.X < 16214.0) then
    	if(jump < 4294967286) then
    		jump = jump + 10
    	end
    elseif(player.Position.X > 16234.0 and player.Position.X < 16246.0) then
    	if(jump < 4294967196) then
    		jump = jump + 100
    	end
    elseif(player.Position.X > 16266.0 and player.Position.X < 16278.0) then
        if(jump < 4294966296) then
            jump = jump + 1000
        end
    end
    for i=0, 31, 1 do
    	if(jump & 1 == 1) then
    		helpers.setFlag("puzzle2_jump"..i, true)
    	else
    		helpers.setFlag("puzzle2_jump"..i, false)
    	end
    	jump = jump >> 1
    end
end

function onEnd()

end