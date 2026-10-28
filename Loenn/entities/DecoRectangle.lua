local drawRect = require("structs.drawable_rectangle")
local drawLine = require("structs.drawable_line")


local DecoRectangle = {
    name = "EmpyrealCity/DecoRectangle",
    depth = -8500,
    fieldOrder = {
        "x", "y", 
        "width", "height",
        "Depth", "Alpha",
        "FillColor", "OutlineColor",
        "DashLine", "UpBorder", "DownBorder", "LeftBorder", "Right Border"
    },
    fieldInformation = {
        Depth = {fieldType = "integer"},
        FillColor = {fieldType = 'color', allowXNAColors = true, allowEmpty = true},
        OutlineColor = {fieldType = 'color', allowXNAColors = true, allowEmpty = true},
    }
}

DecoRectangle.placements = {
    {
        name = "DecoRectangle",
        data = {
            width = 8,
            height = 8,
            Depth = 8999,
            FillColor = "ffaaaa",
            OutlineColor = "ff0000",
            Alpha = 0.2,
            DashLine = false,
            UpBorder = true,
            DownBorder = true,
            LeftBorder = true,
            RightBorder = true
        }
    },
}

DecoRectangle.sprite = function(room, entity)
    incolor = entity.FillColor
    outcolor = entity.OutlineColor
    hex = "0123456789abcdef"
    Temp_alpha = entity.Alpha * 256
    if (Temp_alpha > 255) then
        Temp_alpha = 255
    elseif (Temp_alpha < 0) then
        Temp_alpha = 0
    end
    hex_1 = Temp_alpha % 16
    hex_2 = (Temp_alpha - hex_1) / 16
    transparency = string.sub(hex, hex_2 + 1, hex_2 + 1) .. string.sub(hex, hex_1 + 1, hex_1 + 1)
    incolor = incolor .. transparency
    if (entity.UpBorder and entity.DownBorder and entity.LeftBorder and entity.RightBorder) then
        return {
            drawRect.fromRectangle("bordered", entity.x, entity.y, entity.width, entity.height, incolor, outcolor)
        }
    else
        output = {}
        table.insert(output, drawRect.fromRectangle("fill", entity.x, entity.y, entity.width, entity.height, incolor))
        if (entity.UpBorder) then
            table.insert(output, drawLine.fromPoints({entity.x, entity.y, entity.x + entity.width, entity.y}, outcolor))
        end
        if (entity.DownBorder) then
            table.insert(output, drawLine.fromPoints({entity.x, entity.y + entity.height, entity.x + entity.width, entity.y + entity.height}, outcolor))
        end
        if (entity.LeftBorder) then
            table.insert(output, drawLine.fromPoints({entity.x, entity.y, entity.x, entity.y + entity.height}, outcolor))
        end
        if (entity.RightBorder) then
            table.insert(output, drawLine.fromPoints({entity.x + entity.width, entity.y, entity.x + entity.width, entity.y + entity.height}, outcolor))
        end
        return output
    end
end

return DecoRectangle