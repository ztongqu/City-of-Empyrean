local PlayerModifier = require("#Celeste.Mod.EmpyrealCity.Module.PlayerModifier")

function onEnter(player)
    --<dummy_digit><target_speed_X><target_speed_Y><tolerance_X><tolerance_Y><set_x><set_y>
    --target_speed and tolerance have equal length
    --dummy_digit prevents 0 from disappearing
    --e.g.
    --arguments = 900 not setting anything
    --arguments = 951410001 set y only
    --arguments = 91145020010 set x only
    --arguments = 9114502000514010011 set both x and y
    set_y = math.tointeger(arguments % 10)
    set_x = math.tointeger((arguments % 100 - set_y) / 10)
    len = math.tointeger((string.len(arguments) - 3) / (2 * (set_x + set_y)))
    set_x = set_x == 1
    set_y = set_y == 1
    if len then
        strarg = tostring(arguments)
        if set_x then
            target_speed_X = tonumber(string.sub(strarg, 2, len + 1))
            tolerance_X = tonumber(string.sub(strarg, len + 2, 2 * len + 1))
            if set_y then
                target_speed_Y = tonumber(string.sub(strarg, 2 * len + 2, 3 * len + 1))
                tolerance_Y = tonumber(string.sub(strarg, 3 * len + 2, 4 * len + 1))
            end
        elseif set_y then
            target_speed_Y = tonumber(string.sub(strarg, 2, len + 1))
            tolerance_Y = tonumber(string.sub(strarg, len + 2, 2 * len + 1))
        end
    end
    if set_x then
        if (math.abs(player.Speed.X - target_speed_X) <= tolerance_X) then
            PlayerModifier.SetSpeedX(player, target_speed_X, false)
        end
    end
    if set_y then
        if (math.abs(player.Speed.Y - target_speed_Y) <= tolerance_Y) then
            PlayerModifier.SetSpeedY(player, target_speed_Y, false)
        end
    end
end