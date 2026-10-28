
function onBegin()
	local rng = helpers.loadCelesteAsset("Assets/FAE4D333/EmpyrealCity/rng_functions")
	local result = 0
	for i=0, 13, 1 do
		if(helpers.getFlag("rng_flag"..i)) then
			result = result + 2^i
		end
	end

	local deathcount = helpers.deathsInCurrentRoom()
	local seed_c = rng.iterate(result, deathcount)

	local d = rng.calculate(seed_c)

	helpers.setFlag("puzzle1_1",false)
	helpers.setFlag("puzzle1_2",false)
	helpers.setFlag("puzzle1_3",false)
	helpers.setFlag("puzzle1_4",false)
	helpers.setFlag("puzzle1_5",false)
	if(d<0.2) then
		helpers.setFlag("puzzle1_1",true)
	elseif(d>=0.2 and d<0.4) then
		helpers.setFlag("puzzle1_2",true)
	elseif(d>=0.4 and d<0.6) then
		helpers.setFlag("puzzle1_3",true)
	elseif(d>=0.6 and d<0.8) then
		helpers.setFlag("puzzle1_4",true)
	elseif(d>=0.8) then
		helpers.setFlag("puzzle1_5",true)
	end



	helpers.endCutscene()
end

function onEnd()

end