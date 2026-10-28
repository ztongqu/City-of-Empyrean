function onStay(player)
    if player.Dead then
        player.Dead = false
        helpers.die({0, 0}, true, true)
    end
end