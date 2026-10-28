function onBegin()
    level = helpers.engine.Scene
    current_room = level.Session.LevelData.Name

    current_dashes = player.Dashes
    current_stamina = player.Stamina
    target_room = ""
    if string.sub(current_room, string.len(current_room), string.len(current_room)) == 'a' then
        target_room = string.sub(current_room, 1, string.len(current_room) - 1) .. 'b'
    else
        target_room = string.sub(current_room, 1, string.len(current_room) - 1) .. 'a'
    end
    if player.Dead then
        player.Dead = false
        helpers.die({0, 0}, true, true)
    else
        --level.Session.Level = target_room
        helpers.instantTeleport(target_room)
    end



end

function onEnd()
    --player.Dashes = current_dashes + 1
    --player.Stamina = current_stamina
    helpers.endCutscene() 
end