module EmpyrealCitySampleTrigger

using ..Ahorn, Maple

@mapdef Trigger "EmpyrealCity/SampleTrigger" SampleTrigger(
    x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight,
    sampleProperty::Integer=0
)

const placements = Ahorn.PlacementDict(
    "Sample Trigger (EmpyrealCity)" => Ahorn.EntityPlacement(
        SampleTrigger,
        "rectangle",
    )
)

end