local CustomRefill = {
    name = "EmpyrealCity/CustomRefill",
    depth = -8500,
    fieldOrder = {
        "x", "y", 
        "Directory", "ParticleColors",
        "IdleDepth", "OnPlayerDepth",
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

CustomRefill.placements = {
    {
        name = "UltimateCustomRefill",
        data = {
            Directory = "objects/refill",
            ParticleColors = "d3ffd4,85fc87,a5fff7,6de081,a5fff7,6de081",
            WobbleType = "YOnly",
            IdleDepth = -100,
            OnPlayerDepth = 8999,
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
        name = "FreezeRefill",
        data = {
            Directory = "objects/refill/freeze",
            ParticleColors = "d3eff4, 85ecf7, a5f7ff, 6d81ee, a5f7ff, 6d81ee",
            WobbleType = "None",
            IdleDepth = -100,
            OnPlayerDepth = 8999,
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

CustomRefill.texture = function(room, entity)
    return entity.Directory.."/idle00"
end

return CustomRefill

--[[
local CustomRefill = {
    name = "EmpyrealCity/CustomRefill",
    depth = -8500,
    placements = {
        name = "UltimateCustomRefill",
        texture = "objects/refill/idle00",
        data = {
            Directory = "objects/refill",
            ParticleColors = "d3ffd4, 85fc87, a5fff7, 6de081, a5fff7, 6de081",
            WobbleType = "YOnly",
            IdleDepth = -100,
            OnPlayerDepth = 8999,
            FreezeFrames = 3,
            RespawnTime = 2.5,
            RequirementMode = "either",
            RefillMode = "both",
            DashRequirementOperation = "less than",
            StaminaRequirementOperation = "less than",
            DashCountRequirement = 1,
            StaminaCountRequirement = 10.0,
            RefillDashOperation = "set",
            RefillStaminaOperation = "set",
            RefillDashCount = 1,
            RefillStaminaAmount = 110.0,
            OneUse = false
        }
    }
}
return CustomRefill
--]]