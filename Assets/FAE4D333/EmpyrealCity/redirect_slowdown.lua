local ex_var = require("#ExtendedVariants.Module.LuaCutscenesUtils")

function onBegin()
    --ex_var.TriggerFloatVariant("GameSpeed", 0.5, true)
    --helpers.say(arguments)
    helpers.makeUnskippable()
    --<time>.<target_speed>
    --e.g. 10.005 = 10 frames to slow down the time uniformly from 1x to 0.05x
    time = arguments - arguments % 1
    target_speed = arguments - time
    if(target_speed == 0) then
        target_speed = 0.1
    end
    coef = target_speed ^ (1 / time)
    current_game_speed = 1.0
    player_speed1 = player.Speed
    --say(time)
    --say(target_speed)
    --say(player_speed1.X)
    for _ = 1, time do
        current_game_speed = current_game_speed * coef
        ex_var.TriggerFloatVariant("GameSpeed", current_game_speed, true)
        if(player_speed1 ~= player.Speed) then
            break
        end
        helpers.wait(engine.DeltaTime)
    end

    while(player_speed1 == player.Speed) do
        helpers.wait(engine.DeltaTime)
    end
    helpers.endCutscene()
end

function onEnd()
    ex_var.TriggerFloatVariant("GameSpeed", 1.0, true)
end

function onLeave(player)
    helpers.endCutscene()
end