
function onBegin()
	helpers.disableRetry()
	helpers.makeUnskippable()
	helpers.disablePause()
	helpers.setMusicProgression(1)
	helpers.wait(17.3)
	helpers.die({0, 0}, true, true)
	helpers.wait(0.2)
	helpers.endCutscene()
end

function onEnd()
	helpers.enablePause()
	helpers.enableRetry()
end