

function onBegin()
	local rng = helpers.loadCelesteAsset("Assets/FAE4D333/EmpyrealCity/rng_functions")
	helpers.wait(3)
	local seed_prime = rng.init()
	if (seed_prime < 1000) then
		helpers.wait(0.1)
		seed_prime = (seed_prime * 1111 + rng.init()) % 10000
	end
	helpers.say(seed_prime)

	for i=0, 13, 1 do
		helpers.setFlag("rng_flag"..i, false)
		helpers.setFlag("rng_prime_flag"..i, false)
	end

	local counter = 0
	while(seed_prime > 1) do
		if(seed_prime & 1 == 1) then
			helpers.setFlag("rng_flag"..counter, true)
			helpers.setFlag("rng_prime_flag"..counter, true)
		else
			helpers.setFlag("rng_flag"..counter, false)
			helpers.setFlag("rng_prime_flag"..counter, false)
		end
		seed_prime = seed_prime >> 1
		counter = counter + 1
	end
	if(seed_prime & 1 == 1) then
		helpers.setFlag("rng_flag"..counter, true)
		helpers.setFlag("rng_prime_flag"..counter, true)
	else
		helpers.setFlag("rng_flag"..counter, false)
		helpers.setFlag("rng_prime_flag"..counter, false)
	end

end
