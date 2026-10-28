local ex_var = require("#ExtendedVariants.Module.LuaCutscenesUtils")
function onBegin()
	local default_gravity = 1080
    local default_jump = 16300
    --solution 1: g420, j6400, low subpixel
    --solution 2: g1080, j16300, low subpixel
    ex_var.TriggerFloatVariant("Gravity", 1, false)
    ex_var.TriggerFloatVariant("JumpBoost", 1, false)
    for i=0, 31, 1 do
    	if(default_gravity & 1 == 1) then
    		helpers.setFlag("puzzle2_gravity"..i, true)
    	else
    		helpers.setFlag("puzzle2_gravity"..i, false)
    	end
        if(default_jump & 1 == 1) then
            helpers.setFlag("puzzle2_jump"..i, true)
        else
            helpers.setFlag("puzzle2_jump"..i, false)
        end
    	default_gravity = default_gravity >> 1
        default_jump = default_jump >> 1
    end
    helpers.setFlag("puzzle2_c1", true)

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

    helpers.setFlag("puzzle2_c1",false)
    helpers.setFlag("puzzle2_c2",false)
    helpers.setFlag("puzzle2_c3",false)
    helpers.setFlag("puzzle2_c4",false)
    helpers.setFlag("puzzle2_c5",false)
    if(d<0.2) then
        helpers.setFlag("puzzle2_c2",true)
    elseif(d>=0.2 and d<0.4) then
        helpers.setFlag("puzzle2_c2",true)
    elseif(d>=0.4 and d<0.6) then
        helpers.setFlag("puzzle2_c2",true)
    elseif(d>=0.6 and d<0.8) then
        helpers.setFlag("puzzle2_c2",true)
    elseif(d>=0.8) then
        helpers.setFlag("puzzle2_c2",true)
    end
end

function onEnd()

end