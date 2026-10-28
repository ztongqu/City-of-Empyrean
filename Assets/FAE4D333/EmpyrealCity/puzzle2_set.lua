local ex_var = require("#ExtendedVariants.Module.LuaCutscenesUtils")
function onBegin()
	helpers.disableMovement()
	local gravity = 0
	local jump = 0
	for i=31, 0, -1 do
		if(helpers.getFlag("puzzle2_gravity"..i)==true) then
			gravity = gravity + 2^i
		end
		if(helpers.getFlag("puzzle2_jump"..i)==true) then
			jump = jump + 2^i
		end
    end
    gravity = gravity / 10
	jump = jump / 10
    ex_var.TriggerFloatVariant("Gravity", gravity/100, false)
    ex_var.TriggerFloatVariant("JumpBoost", jump/100, false)
    helpers.say("GravityValue:"..gravity.."%")
    helpers.say("JumpBoostValue:"..jump.."%")
end

function onEnd()
	helpers.enableMovement()
end