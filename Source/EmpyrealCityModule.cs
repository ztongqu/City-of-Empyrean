using System;
using System.Collections.Generic;
using System.Linq;
using Celeste;
using Celeste.Mod.Entities;
using System.Reflection.Metadata;
using Celeste.Mod.EmpyrealCity.States;
using NLua;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.ModInterop;
namespace Celeste.Mod.EmpyrealCity.Module;

public class EmpyrealCityModule : EverestModule {
    public static EmpyrealCityModule Instance { get; private set; }

    public override Type SettingsType => typeof(EmpyrealCityModuleSettings);
    public static EmpyrealCityModuleSettings Settings => (EmpyrealCityModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(EmpyrealCityModuleSession);
    public static EmpyrealCityModuleSession Session => (EmpyrealCityModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(EmpyrealCityModuleSaveData);
    public static EmpyrealCityModuleSaveData SaveData => (EmpyrealCityModuleSaveData) Instance._SaveData;

    public EmpyrealCityModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(EmpyrealCityModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(EmpyrealCityModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        // TODO: apply any hooks that should always be active
        typeof(PlayerModifier).ModInterop();
    }

    public override void Unload() {
        // TODO: unapply any hooks applied in Load()
    }
}


//Alternative solution in case changing player's parameters doesn't work in lua assets
public static class PlayerModifier
{
    //Speed
    public static void SetSpeed(Player player, Vector2 Speed, bool add = false)
    {
        if (add) {
            player.Speed += Speed;
        } else {
            player.Speed = Speed;
        }
    }
    public static void SetSpeedX(Player player, float SpeedX, bool add = false)
    {
        if (add) {
            player.Speed.X += SpeedX;
        } else {
            player.Speed.X = SpeedX;
        }
    }
    public static void SetSpeedY(Player player, float SpeedY, bool add = false)
    {
        if (add) {
            player.Speed.Y += SpeedY;
        } else {
            player.Speed.Y = SpeedY;
        }
    }

    //Position
    public static void SetPos(Player player, Vector2 Pos, bool add = false)
    {
        if (add) {
            player.Position += Pos;
        } else {
            player.Position = Pos;
        }
    }
    public static void SetPosX(Player player, float PosX, bool add = false)
    {
        if (add) {
            player.Position.X += PosX;
        } else {
            player.Position.X = PosX;
        }
    }
    public static void SetPosY(Player player, float PosY, bool add = false)
    {
        if (add) {
            player.Position.Y += PosY;
        } else {
            player.Position.Y = PosY;
        }
    }

    //Subpixel
    public static void SetSubpixel(Player player, Vector2 Subpixel, bool add = false)
    {
        if (add) {
            player.movementCounter += Subpixel;
        } else {
            player.movementCounter = Subpixel;
        }
    }
    public static void SetSubpixelX(Player player, float SubpixelX, bool add = false)
    {
        if (add) {
            player.movementCounter.X += SubpixelX;
        } else {
            player.movementCounter.X = SubpixelX;
        }
    }
    public static void SetSubpixelY(Player player, float SubpixelY, bool add = false)
    {
        if (add) {
            player.movementCounter.Y += SubpixelY;
        } else {
            player.movementCounter.Y = SubpixelY;
        }
    }

    //Jumping
    public static void SetCoyoteTime(Player player, float Time, bool add = false)
    {
        if (add) {
            player.jumpGraceTimer += Time;
        } else {
            player.jumpGraceTimer = Time;
        }
    }
    public static void SetCoyoteFrames(Player player, int Frames, bool add = false)
    {
        float time = Frames * Engine.DeltaTime;
        SetCoyoteTime(player, time, add);
    }
    public static void SetAutoJumpTimer(Player player, int Frames, bool add = false)
    {
        float time = Frames * Engine.DeltaTime;
        player.AutoJump = true;
        player.AutoJumpTimer = add ? player.AutoJumpTimer + time : time;
    }
    public static void SetAutoJump(Player player, bool value)
    {
        player.AutoJump = value;
    }
    public static void ToggleAutoJump(Player player)
    {
        player.AutoJump = !player.AutoJump;
    }

    //🦆
    public static void SetDuckState(Player player, bool duck)
    {
        player.Ducking = duck;
    }
    public static void ToggleDuckState(Player player)
    {
        player.Ducking = !player.Ducking;
    }

    //Facing
    public static void FacingRight(Player player)
    {
        player.Facing = Facings.Right;
    }
    public static void FacingLeft(Player player)
    {
        player.Facing = Facings.Left;
    }
    public static void ChangeFacing(Player player)
    {
        player.Facing = player.Facing == Facings.Right ? Facings.Left : Facings.Right;
    }

    //Misc
    public static void ChangeRoom(Player player, Level level, String target)
    {
        LevelData data = level.Session.LevelData;
        Vector2 position = player.Position;
        level.Session.Level = target;
        player.CleanUpTriggers();
        int dashes = player.Dashes;
        Facings facing = player.Facing;
        Leader leader = player.Get<Leader>();
        foreach (Follower item in leader.Followers.Where((Follower f) => f.Entity != null))
        {
            item.Entity.AddTag(Tags.Global);
            level.Session.DoNotLoad.Add(item.ParentEntityID);
        }
        data = level.Session.LevelData;
        player.Facing = facing;
        player.Dashes = dashes;
        level.Session.RespawnPoint = data.Spawns.ClosestTo(player.Position);
        position = player.Position - position;
        foreach (Follower item2 in leader.Followers.Where((Follower f) => f.Entity != null))
        {
            Entity entity = item2.Entity;
            entity.Position += position;
            item2.Entity.RemoveTag(Tags.Global);
            level.Session.DoNotLoad.Remove(item2.ParentEntityID);
        }
        for (int num = 0; num < leader.PastPoints.Count; num++)
        {
            List<Vector2> pastPoints = leader.PastPoints;
            int index = num;
            pastPoints[index] += position;
        }
        leader.TransferFollowers();

    }
    public static double LogGamma(double z)
    {
        // log gamma function by James McCaffrey
        // Lanczos approximation g=5, n=7
        double[] coef = new double[7] { 1.000000000190015,
            76.18009172947146, -86.50532032941677,
            24.01409824083091, -1.231739572450155,
            0.1208650973866179e-2, -0.5395239384953e-5 };
    
        double LogSqrtTwoPi = 0.91893853320467274178;
    
        if (z < 0.5)
            return Math.Log(Math.PI / Math.Sin(Math.PI * z)) -
            LogGamma(1.0 - z);
    
        double zz = z - 1.0;
        double b = zz + 5.5; // g + 0.5
        double sum = coef[0];
    
        for (int i = 1; i < coef.Length; ++i)
            sum += coef[i] / (zz + i);
    
        return (LogSqrtTwoPi + Math.Log(sum) - b) +
            (Math.Log(b) * (zz + 0.5));
    }
    public static double Confusion(double z)
    {
        z += 166667;
        for (int i = 0; i < 1373; i ++){
		    z = (int)(LogGamma(z) % 2123231111 + 166667);
	    }
        return z - 166667;
    }
    public static void ModifiedTeleport(Player player, Level level, string room, float positionX, float positionY, bool sameRelativePosition)
    {
        if (level == null)
        {
            return;
        }

        if (String.IsNullOrEmpty(room))
        {
            Vector2 playerRelativeOffset = (new Vector2(positionX, positionY) - player.Position);

            player.Position = new Vector2(positionX, positionY);
            level.Camera.Position += playerRelativeOffset;
            player.Hair.MoveHairBy(playerRelativeOffset);

        }
        else
        {
            level.OnEndOfFrame += delegate
            {
                Vector2 levelOffset = level.LevelOffset;
                Vector2 playerOffset = player.Position - level.LevelOffset;
                Vector2 cameraOffset = level.Camera.Position - level.LevelOffset;
                Facings facing = player.Facing;

                level.Remove(player);
                level.UnloadLevel();
                level.Session.Level = room;
                level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
                level.Session.FirstLevel = false;
                level.LoadLevel(Player.IntroTypes.Transition);

                if (sameRelativePosition)
                {
                    level.Camera.Position = level.LevelOffset + cameraOffset;
                    level.Add(player);
                    player.Position = level.LevelOffset + playerOffset;
                    player.Facing = facing;
                    player.Hair.MoveHairBy(level.LevelOffset - levelOffset);
                }
                else
                {
                    Vector2 playerRelativeOffset  = (new Vector2(positionX, positionY) - level.LevelOffset - playerOffset);

                    level.Camera.Position = level.LevelOffset + cameraOffset + playerRelativeOffset;
                    level.Add(player);
                    player.Position = new Vector2(positionX, positionY);
                    player.Facing = facing;
                    player.Hair.MoveHairBy(level.LevelOffset - levelOffset + playerRelativeOffset);
                }

                if (level.Wipe != null)
                {
                    level.Wipe.Cancel();
                }
            };
        }
    }
}