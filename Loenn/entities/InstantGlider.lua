local utils = require('utils')
local drawableSprite = require("structs.drawable_sprite")
local drawableLine = require("structs.drawable_line")
local drawing = require("utils.drawing")
local atlases = require('atlases')


local InstantGlider = {
    name = "EmpyrealCity/InstantGlider",
    depth = -8500,
    fieldOrder = {
        "x","y",
        "Directory",
        "Depth"
    },
    fieldInformation = {
        GlowColor1 = {fieldType = 'color', allowXNAColors = true, allowEmpty = true },
        GlowColor2 = {fieldType = 'color', allowXNAColors = true, allowEmpty = true },
        GlideColor1 = {fieldType = 'color', allowXNAColors = true, allowEmpty = true },
        GlideColor2 = {fieldType = 'color', allowXNAColors = true, allowEmpty = true }
    },
    placements = {
        {
            name = "InstantGlider",
            data = {
                Directory = "objects/yellow_glider",
                GlowColor1 = "fff3b7",
                GlowColor2 = "fff3ef",
                GlideColor1 = "fff34f",
                GlideColor2 = "a9b6ff",
                Depth = -5,
                bubble = false,
                tutorial = false,
                invulnerable = false
            }
        },
    },
}

InstantGlider.sprite = function(room, entity)
    local val = atlases.getResource(entity.Directory.."/idle0", "Gameplay")
    local sprite = drawableSprite.fromMeta(val, entity)
    if entity.bubble then
        local x, y = entity.x or 0, entity.y or 0
        local points = drawing.getSimpleCurve({x - 11, y - 1}, {x + 11, y - 1}, {x - 0, y - 6})
        local lineSprites = drawableLine.fromPoints(points):getDrawableSprite()

        table.insert(lineSprites, sprite)

        return lineSprites
    else
        return sprite 
    end
end

return InstantGlider