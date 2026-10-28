
function onBegin()
	local rng = helpers.loadCelesteAsset("Assets/FAE4D333/EmpyrealCity/rng_functions")
	local result = 0
	for i=0, 13, 1 do
		if(helpers.getFlag("rng_flag"..i)) then
			result = result + 2^i
		end
	end
	helpers.say(result)
		
	local deathcount = helpers.deathsInCurrentRoom()
	local seed_c = rng.iterate(result, deathcount)
	helpers.say(seed_c)




	helpers.endCutscene()
end

function onEnd()

end