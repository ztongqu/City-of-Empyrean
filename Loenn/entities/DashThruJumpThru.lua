local drawSprite = require('structs.drawable_sprite')
local enums = require("consts.celeste_enums")
local utils = require("utils")

local textures = {"wood", "dream", "temple", "templeB", "cliffside", "reflection", "core", "moon", "ice", "city", "invisible", "shadow"}

local DashThruJumpThru = {
    name = "EmpyrealCity/DashThruJumpThru",
    placements = {
        {
            name = "DashThruJumpThru",
            data = {
                width=8, Depth=-60, Texture="shadow", SurfaceIndex=-1, delay=0, RedDashCountsAsDash=false;
            },
        },
    },
    fieldInformation = {
        Texture = {
            options = textures
        },
        SurfaceIndex = {
            options = enums.tileset_sound_ids,
            fieldType = "integer"
        }
    },


    sprite = function(room, entity)
        local texture = "objects/jumpthru/"..entity.Texture
        local sprites = {}

        local x, y = entity.x or 0, entity.y or 0
        local width = entity.width or 8

        local startX, startY = math.floor(x / 8) + 1, math.floor(y / 8) + 1
        local stopX = startX + math.floor(width / 8) - 1
        local len = stopX - startX

        for i = 0, len do
            local quadX = 8
            local quadY = 8

            if i == 0 then
                quadX = 0
                quadY = room.tilesFg.matrix:get(startX - 1, startY, "0") ~= "0" and 0 or 8

            elseif i == len then
                quadY = room.tilesFg.matrix:get(stopX + 1, startY, "0") ~= "0" and 0 or 8
                quadX = 16
            end

            local sprite = drawSprite.fromTexture(texture, entity)

            sprite:setJustification(0, 0)
            sprite:addPosition(i * 8, 0)
            sprite:useRelativeQuad(quadX, quadY, 8, 8)

            table.insert(sprites, sprite)
        end

        return sprites
    end,

    selection = function(room, entity)
        return utils.rectangle(entity.x, entity.y, entity.width, 8)
    end
}

return DashThruJumpThru