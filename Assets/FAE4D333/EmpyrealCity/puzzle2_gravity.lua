function onBegin()
	local gravity = 0
	for i=31, 0, -1 do
		if(helpers.getFlag("puzzle2_gravity"..i)==true) then
			gravity = gravity + 2^i
		end
    end
    if(player.Position.X > 15906.0 and player.Position.X < 15918.0) then
    	if(gravity < 4294967196) then
    		gravity = gravity + 100
    	end
    elseif(player.Position.X > 15938.0 and player.Position.X < 15950.0) then
    	if(gravity < 4294967286) then
    		gravity = gravity + 10
    	end
    elseif(player.Position.X > 15970.0 and player.Position.X < 15982.0) then
        if(gravity < 4294967295) then
            gravity = gravity + 1
        end
    elseif(player.Position.X > 16002.0 and player.Position.X < 16014.0) then
        if(gravity > 0) then
            gravity = gravity - 1
        end
    elseif(player.Position.X > 16034.0 and player.Position.X < 16046.0) then
    	if(gravity > 9) then
    		gravity = gravity - 10
    	end
    elseif(player.Position.X > 16066.0 and player.Position.X < 16078.0) then
    	if(gravity > 99) then
    		gravity = gravity - 100
    	end
    end
    for i=0, 31, 1 do
    	if(gravity & 1 == 1) then
    		helpers.setFlag("puzzle2_gravity"..i, true)
    	else
    		helpers.setFlag("puzzle2_gravity"..i, false)
    	end
    	gravity = gravity >> 1
    end 
end

function onEnd()

end