local drawSprite = require('structs.drawable_sprite')

--Up
local HarmlessSpikeUp = {
    name = "EmpyrealCity/HarmlessSpikeUp",
    depth = -8500,
    placements = {
        {
            name = "HarmlessSpikeUp",
            data = {width = 8, type = "HarmlessSpike/blue"},
        },
    },
    fieldInformation = {
        type = {fieldType = "string", options = {"HarmlessSpike/blue", "default", "outline", "cliffside", "reflection"}, editable = true}
    },
    texture = function(room, entity)
        return "danger/spikes/"..entity.type.."_up00"
    end,
    sprite = function(room, entity)
        local single_sprite = "danger/spikes/"..entity.type.."_up00"
        local sprites = {}
        for i=1, entity.width/8, 1 do
            sprites[i] = drawSprite.fromTexture(single_sprite, entity):setPosition(entity.x - 4 + 8*i, entity.y - 4)
        end
        return sprites
    end
}


--Down
local HarmlessSpikeDown = {
    name = "EmpyrealCity/HarmlessSpikeDown",
    depth = -8500,
    placements = {
        {
            name = "HarmlessSpikeDown",
            data = {width = 8, type = "HarmlessSpike/blue"},
        },
    },
    fieldInformation = {
        type = {fieldType = "string", options = {"HarmlessSpike/blue", "default", "outline", "cliffside", "reflection"}, editable = true}
    },
    texture = function(room, entity)
        return "danger/spikes/"..entity.type.."_down00"
    end,
    sprite = function(room, entity)
        local single_sprite = "danger/spikes/"..entity.type.."_down00"
        local sprites = {}
        for i=1, entity.width/8, 1 do
            sprites[i] = drawSprite.fromTexture(single_sprite, entity):setPosition(entity.x - 4 + 8*i, entity.y + 4)
        end
        return sprites
    end
}


--Left
local HarmlessSpikeLeft = {
    name = "EmpyrealCity/HarmlessSpikeLeft",
    depth = -8500,
    placements = {
        {
            name = "HarmlessSpikeLeft",
            data = {height = 8, type = "HarmlessSpike/blue"},
        },
    },
    fieldInformation = {
        type = {fieldType = "string", options = {"HarmlessSpike/blue", "default", "outline", "cliffside", "reflection"}, editable = true}
    },
    texture = function(room, entity)
        return "danger/spikes/"..entity.type.."_left00"
    end,
    sprite = function(room, entity)
        local single_sprite = "danger/spikes/"..entity.type.."_left00"
        local sprites = {}
        for i=1, entity.height/8, 1 do
            sprites[i] = drawSprite.fromTexture(single_sprite, entity):setPosition(entity.x - 4, entity.y - 4 + 8*i)
        end
        return sprites
    end
}


--Right
local HarmlessSpikeRight = {
    name = "EmpyrealCity/HarmlessSpikeRight",
    depth = -8500,
    placements = {
        {
            name = "HarmlessSpikeRight",
            data = {height = 8, type = "HarmlessSpike/blue"},
        },
    },
    fieldInformation = {
        type = {fieldType = "string", options = {"HarmlessSpike/blue", "default", "outline", "cliffside", "reflection"}, editable = true}
    },
    texture = function(room, entity)
        return "danger/spikes/"..entity.type.."_right00"
    end,
    sprite = function(room, entity)
        local single_sprite = "danger/spikes/"..entity.type.."_right00"
        local sprites = {}
        for i=1, entity.height/8, 1 do
            sprites[i] = drawSprite.fromTexture(single_sprite, entity):setPosition(entity.x + 4, entity.y - 4 + 8*i)
        end
        return sprites
    end
}

return {HarmlessSpikeUp, HarmlessSpikeDown, HarmlessSpikeLeft, HarmlessSpikeRight}