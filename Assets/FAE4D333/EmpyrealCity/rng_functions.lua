
local rng_funcitons = {}

function rng_funcitons.calculate(seed, mul_factor, add_factor, div_factor)
	if type(mul_factor) == "nil" then
		mul_factor = 314159
	end
	if type(add_factor) == "nil" then
		add_factor = 271829
	end
	if type(div_factor) == "nil" then
		div_factor = 178187
	end

	return ((seed*mul_factor+add_factor)%div_factor)/div_factor

end

function rng_funcitons.init()
    local position_x = player.Position.X
	local position_y = player.Position.Y
	local speed_x = player.Speed.X
	local speed_y = player.Speed.Y
	return ((rng_funcitons.abs(speed_y)%11 + (rng_funcitons.abs(speed_x)%11)*11 + (rng_funcitons.abs(position_y)%11)*121 + (rng_funcitons.abs(position_x)%11)*1331) // 1) % 10000
end

function rng_funcitons.abs(num)
	if (num < 0) then
 		return num * (-1)
 	else
 		return num
    end
end

function rng_funcitons.iterate(seed, count)
    if type(count) == "nil" then
    	count = 1
    end
    local current_seed = ((seed * seed) // 100) % 10000
    if count > 1 then
    	for i=2, count do
    		current_seed = ((current_seed * current_seed) // 100) % 10000
    	end
	end
    return current_seed
end






return rng_funcitons




