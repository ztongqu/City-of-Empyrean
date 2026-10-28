local drawSprite = require("structs.drawable_sprite")
local drawRect = require("structs.drawable_rectangle")


local CustomRefillWall = {
    name = "EmpyrealCity/CustomRefillWall",
    depth = -8500,
    fieldOrder = {
        "x", "y", 
        "width", "height",
        "Directory", "ParticleColors",
        "IdleDepth", "OnPlayerDepth",
        "FillColor", "OutlineColor",
        "WobbleType", "OneUse",
        "FreezeFrames", "RespawnTime",
        "RequirementMode", "RefillMode",
        "DashRequirementOperation", "DashCountRequirement",
        "StaminaRequirementOperation", "StaminaCountRequirement",
        "RefillDashOperation", "RefillDashCount",
        "RefillStaminaOperation", "RefillStaminaAmount"
    },
    fieldInformation = {
        Directory = {fieldType = "string"},
        ParticleColors = {fieldType = "string"},
        WobbleType = {fieldType = "string", options = {"XOnly", "YOnly", "Circle", "None"}, editable = false},
        IdleDepth = {fieldType = "integer"},
        OnPlayerDepth = {fieldType = "integer"},
        FillColor = {fieldType = 'color', allowXNAColors = true, allowEmpty = true},
        OutlineColor = {fieldType = 'color', allowXNAColors = true, allowEmpty = true},
        FreezeFrames = {fieldType = "integer"},
        RespawnTime = {fieldType = "number"},
        RequirementMode = {fieldType = "string", options = {"both", "either", "stamina", "dash", "stamina only", "dash only", "always", "never"}, editable = false},
        RefillMode = {fieldType = "string", options = {"both", "neither", "stamina only", "dash only"}, editable = false},
        DashRequirementOperation = {fieldType = "string", options = {"less than", "equal to", "greater than"}, editable = false},
        StaminaRequirementOperation = {fieldType = "string", options = {"less than", "equal to", "greater than"}, editable = false},
        DashCountRequirement = {fieldType = "integer"},
        StaminaCountRequirement = {fieldType = "number"},
        RefillDashOperation = {fieldType = "string", options = {"add", "set"}, editable = false},
        RefillStaminaOperation = {fieldType = "string", options = {"add", "set"}, editable = false},
        RefillDashCount = {fieldType = "integer"},
        RefillStaminaAmount = {fieldType = "number"}
    }
}

CustomRefillWall.placements = {
    {
        name = "UltimateCustomRefillWall",
        data = {
            width = 8,
            height = 8,
            Directory = "objects/refill",
            ParticleColors = "d3ffd4,85fc87,a5fff7,6de081,a5fff7,6de081",
            WobbleType = "YOnly",
            IdleDepth = 100,
            OnPlayerDepth = 8999,
            FillColor = "207f20",
            OutlineColor = "93bd40",
            FreezeFrames = 3,
            RespawnTime = 2.5,
            RequirementMode = "either",
            RefillMode = "both",
            DashRequirementOperation = "less than",
            StaminaRequirementOperation = "less than",
            DashCountRequirement = 1,
            StaminaCountRequirement = 20.0,
            RefillDashOperation = "set",
            RefillStaminaOperation = "set",
            RefillDashCount = 1,
            RefillStaminaAmount = 110.0,
            OneUse = false
        }
    },
    {
        name = "FreezeRefillWall",
        data = {
            width = 8,
            height = 8,
            Directory = "objects/refill/freeze",
            ParticleColors = "d3eff4, 85ecf7, a5f7ff, 6d81ee, a5f7ff, 6d81ee",
            WobbleType = "None",
            IdleDepth = 100,
            OnPlayerDepth = 8999,
            FillColor = "76a6d4",
            OutlineColor = "00d9ff",
            FreezeFrames = 3,
            RespawnTime = 2.5,
            RequirementMode = "always",
            RefillMode = "neither",
            DashRequirementOperation = "less than",
            StaminaRequirementOperation = "less than",
            DashCountRequirement = 1,
            StaminaCountRequirement = 20.0,
            RefillDashOperation = "set",
            RefillStaminaOperation = "set",
            RefillDashCount = 1,
            RefillStaminaAmount = 110.0,
            OneUse = false
        }
    }
}

CustomRefillWall.texture = function(room, entity)
    return entity.Directory.."/idle00"
end

CustomRefillWall.sprite = function(room, entity)
    sprtie_path = entity.Directory.."/idle00"
    incolor = entity.FillColor
    outcolor = entity.OutlineColor
    return {
    drawRect.fromRectangle("bordered", entity.x, entity.y, entity.width, entity.height, incolor, outcolor),
    drawSprite.fromTexture(sprtie_path, entity):setPosition(entity.x + entity.width/2, entity.y + entity.height/2)
    }
end

return CustomRefillWall