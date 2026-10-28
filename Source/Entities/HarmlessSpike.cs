using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.EmpyrealCity.Module;

namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity(
    "EmpyrealCity/HarmlessSpikeUp = up",
    "EmpyrealCity/HarmlessSpikeDown = down",
    "EmpyrealCity/HarmlessSpikeLeft = left",
    "EmpyrealCity/HarmlessSpikeRight = right")]
[TrackedAs(typeof(Spikes))]
public class HarmlessSpike : Spikes {

    private const string DefaultTexture = "HarmlessSpike/cliffside";
    private readonly Action<Player> origOnCollide;
    public HarmlessSpike(EntityData data, Vector2 offset, Directions dir)
        : base(data, offset, dir)
    {
        string texturePath = data.Attr("type", DefaultTexture);
        if (texturePath.Length == 0)
        {
            texturePath = DefaultTexture;
        }
        overrideType = texturePath;

        origOnCollide = pc.OnCollide;
        pc.OnCollide = OnPlayer;
    }
    private void OnPlayer(Player player) {return;}
    public static Entity up(Level level, LevelData levelData, Vector2 offset, EntityData entityData) => new HarmlessSpike(entityData, offset, Directions.Up);
    public static Entity down(Level level, LevelData levelData, Vector2 offset, EntityData entityData) => new HarmlessSpike(entityData, offset, Directions.Down);
    public static Entity left(Level level, LevelData levelData, Vector2 offset, EntityData entityData) => new HarmlessSpike(entityData, offset, Directions.Left);
    public static Entity right(Level level, LevelData levelData, Vector2 offset, EntityData entityData) => new HarmlessSpike(entityData, offset, Directions.Right);

}