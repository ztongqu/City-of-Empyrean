using MonoMod.Utils;
using Monocle;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Celeste.Mod.EmpyrealCity.Module;
namespace Celeste.Mod.EmpyrealCity.States;

public static class LightningDreamDashState
{
/*
    public static int LightningDreamDash {get; private set;} = -1;

    public static void LightningDreamDashBegin(this Player player)
    {
        if (player.dreamSfxLoop is null)
        {
            player.Add(player.dreamSfxLoop = new SoundSource());
        }

        DynamicData playerData = DynamicData.For(player);

        float speed = Math.Max(player.Speed.Length(), Player.DashSpeed);
        Vector2 dir = player.DashDir;
        player.Speed = speed * dir;
        player.StartedDashing = false;
        player.TreatNaive = true;
        player.Depth = Depths.PlayerDreamDashing;
        playerData.Set("LightningDreamDashCanEndTimer", 0.1f);
        player.Stamina = Player.ClimbMaxStamina;
        playerData.Set("dreamJump", false);
        player.Play(SFX.char_mad_dreamblock_enter, null, 0f);
    }

    public static void LightningDreamDashEnd(this Player player)
    {
        DynamicData playerData = DynamicData.For(player);
        player.Depth = Depths.Player;
        if (!player.dreamJump)
        {
            player.AutoJump = true;
            player.AutoJumpTimer = 0f;
        }
        if (!player.Inventory.NoRefills)
        {
            player.RefillDash();
        }
        player.RefillStamina();
        player.TreatNaive = false;

        Lightning lightning = playerData.Get<Lightning>("Lightning");
        //missing
        if (player.DashDir.X != 0f)
        {
            player.jumpGraceTimer = 0.1f;
            player.dreamJump = true;
        }
        else
        {
            player.jumpGraceTimer = 0f;
        }
        player.Stop(player.dreamSfxLoop);
        player.Play(SFX.char_mad_dreamblock_exit, null, 0f);
        Input.Rumble(RumbleStrength.Medium, RumbleLength.Short);

    }

    public static int LightningDreamDashUpdate(this Player player)
    {
        DynamicData playerData = DynamicData.For(player);
        Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
        Vector2 position = player.Position;
        player.NaiveMove(player.Speed * Engine.DeltaTime);

        float lightningDreamDashCanEndTimer = playerData.Get<float>("LightningDreamDashCanEndTimer");
        if (lightningDreamDashCanEndTimer > 0f)
        {
            lightningDreamDashCanEndTimer -= Engine.DeltaTime;
        }

        Lightning lightning = player.CollideFirst<Lightning>();
        if (lightning is null)
        {
            if (player.DreamDashedIntoSolid())
            {
                if (SaveData.Instance.Assists.Invincible)
                {
                    player.Position = position;
                    player.Speed *= -1f;
                    player.Play("event:/game/general/assist_dreamblockbounce");
                }
                else
                {
                    player.Die(Vector2.Zero);
                }
            }
            else if (playerData.Get<float>("LightningDreamDashCanEndTimer") <= 0f)
            {
                Celeste.Freeze(0.05f);
                if (Input.Jump.Pressed && player.DashDir.X != 0f)
                {
                    player.dreamJump = true;
                    player.Jump(true, true);
                }
                else if (player.DashDir.Y >= 0f || player.DashDir.X != 0f)
                {
                    if (player.DashDir.X > 0f && player.CollideCheck<Solid>(player.Position - (Vector2.UnitX * 5f)))
                    {
                        player.MoveHExact(-5, null, null);
                    }
                    else if (player.DashDir.X < 0f && player.CollideCheck<Solid>(player.Position + (Vector2.UnitX * 5f)))
                    {
                        player.MoveHExact(5, null, null);
                    }
                    bool flag = player.ClimbCheck(-1, 0);
                    bool flag2 = player.ClimbCheck(1, 0);
                    int moveX = player.moveX;
                    if (Input.GrabCheck && ((moveX == 1 && flag2) || (moveX == -1 && flag)))
                    {
                        player.Facing = (Facings) moveX;
                        if (!SaveData.Instance.Assists.NoGrabbing)
                        {
                            return Player.StClimb;
                        }
                        player.ClimbTrigger(moveX);
                        player.Speed.X = 0f;
                    }
                }
                return Player.StNormal;
            }
        }
        else
        {
            playerData.Set("Lightning", lightning);
            if (player.Scene.OnInterval(0.1f))
            {
                player.CreateTrail();
            }
            Level level = player.level;
            if (level.OnInterval(0.04f))
            {
                DisplacementRenderer.Burst burst = level.Displacement.AddBurst(player.Center, 0.3f, 0f, 40f, 1f, null, null);
                burst.WorldClipCollider = lightning.Collider;
                burst.WorldClipPadding = 2;
            }
        }
        return LightningDreamDash;
    }
*/
}