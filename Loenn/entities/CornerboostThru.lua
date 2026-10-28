local drawSprite = require('structs.drawable_sprite')
local enums = require("consts.celeste_enums")

local CornerboostThru = {
    name = "EmpyrealCity/CornerboostThru",
    placements = {
        {
            name = "WackyCornerboostThru",
            data = {
                width=8, height=8, Depth=-12999, Spacing=2, ExtraJumpthrus=0, Texture="dream", SurfaceIndex=-1, Left=false
            },
        },
    },
    fieldInformation = {
        SurfaceIndex = {
            options = enums.tileset_sound_ids,
            fieldType = "integer"
        }
    },


    sprite = function(room, entity)
        local texture = "objects/jumpthru/"..entity.Texture
        local sprites = {}

        local startX = math.floor(entity.x / entity.Spacing) + 1
        local startY = math.floor(entity.y / 8) + 1
        local stopX = startX + math.floor(entity.width / entity.Spacing) - 1
        local stopY = startY + math.floor(entity.height / 8) - 1
        local columns = stopX - startX + entity.ExtraJumpthrus + 1
        local rows = stopY - startY
        if columns > 0 then
            for j = 0, columns, 1 do
                for i = 0, rows, 1 do
                    local quadX = 8
                    local quadY = 8

                    if i == 0 then
                        quadX = entity.Left and 0 or 16
                    elseif i == rows then
                        quadX = entity.Left and 16 or 0
                    end

                    local sprite = drawSprite.fromTexture(texture, entity)

                    sprite:setJustification(0, 0)
                    sprite:addPosition(j * entity.Spacing - (entity.Left and entity.ExtraJumpthrus * entity.Spacing or 0), i * 8 + (entity.Left and 0 or 8))
                    sprite:useRelativeQuad(quadX, quadY, 8, 8)
                    sprite.rotation = entity.Left and  math.pi / 2 or math.pi / -2

                    table.insert(sprites, sprite)
                end
            end
        elseif columns == 0 then
            for i = 0, rows, 1 do
                local quadX = 8
                local quadY = 8

                if i == 0 then
                    quadX = entity.Left and 0 or 16
                elseif i == rows then
                    quadX = entity.Left and 16 or 0
                end

                local sprite = drawSprite.fromTexture(texture, entity)

                sprite:setJustification(0, 0)
                sprite:addPosition(entity.Left and entity.ExtraJumpthrus * entity.Spacing * -1 or 0, i * 8 + (entity.Left and 0 or 8))
                sprite:useRelativeQuad(quadX, quadY, 8, 8)
                sprite.rotation = entity.Left and  math.pi / 2 or math.pi / -2

                table.insert(sprites, sprite)
            end
        end

        return sprites
    end
}

return CornerboostThru