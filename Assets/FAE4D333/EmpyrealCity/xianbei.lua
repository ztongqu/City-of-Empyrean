
function onBegin()
	helpers.disableMovement()
	helpers.disableRetry()
	helpers.makeUnskippable()
	helpers.say("AAAAAA")
	helpers.playSound("event:/Muli/Roar")
	helpers.wait(27.0)
	helpers.say("AAAAAB")
end

function onEnd()
	helpers.playSound("event:/new_content/music/lvl10/cassette_rooms")
	helpers.enableMovement()
end