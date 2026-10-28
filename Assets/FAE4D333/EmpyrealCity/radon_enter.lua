
function onBegin()
	helpers.disableRetry()
	helpers.makeUnskippable()
	helpers.disablePause()
	helpers.wait(1/60)
	helpers.die({0, 0}, true, false)
	helpers.endCutscene()
end

function onEnd()
	helpers.enablePause()
	helpers.enableRetry()
end