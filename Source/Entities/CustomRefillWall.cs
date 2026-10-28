using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.EmpyrealCity.Module;
using System.Collections;

namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity("EmpyrealCity/CustomRefillWall")]
public class CustomRefillWall : Entity {

    private Level level;
    public string texturePath;
    private Sprite sprite;
    public int depth_idle;
    public int depth_OnPlayer;
    public ParticleType P_Shatter, P_Glow, P_Regen;
    private string[] P_Colors_string;
    private Color color_inside;
    private Color color_outside;
    //refill
    private int RefillDash;
    private float RefillStamina;
    private string DashRefillOperation;
    private string StaminaRefillOperation;
    private int FreezeFrame;
    private float FreezeTime;
    private string RefillMode;
    private float RespawnTime;
    private float respawn_timer;
    private bool OneUse;
    //conditions
    private int DashRequirementCount;
    private float StaminaRequirementCount;
    private string DashRequirementOperation;
    private string StaminaRequirementOperation;
    private string RequirementMode;

    public CustomRefillWall(EntityData data, Vector2 offset)
        : base(data.Position + offset)
    {
        texturePath = data.Attr("Directory", "objects/refill/").TrimEnd('/') + "/";
        base.Collider = new Hitbox(data.Width, data.Height);
        Add(new PlayerCollider(OnPlayer));

        Add(sprite = new Sprite(GFX.Game, texturePath + "idle"));
        sprite.AddLoop("idle", "", 0.1f);
        sprite.Play("idle");
        sprite.CenterOrigin();
        sprite.Position = Collider.Center;

        Add(new MirrorReflection());
        color_inside = Calc.HexToColor(data.Attr("FillColor", "207f20"));
        color_outside = Calc.HexToColor(data.Attr("OutlineColor", "93bd40"));
        //particle
        P_Shatter = new ParticleType(Refill.P_Shatter);
        P_Glow = new ParticleType(Refill.P_Glow);
        P_Regen = new ParticleType(Refill.P_Regen);
        P_Colors_string = data.Attr("ParticleColors", "d3ffd4,85fc87,a5fff7,6de081,a5fff7,6de081").Replace(" ", "").Split(",");
        Color?[] cc = new Color?[6] {Calc.HexToColor(P_Colors_string[0]), Calc.HexToColor(P_Colors_string[1]), Calc.HexToColor(P_Colors_string[2]), Calc.HexToColor(P_Colors_string[3]), Calc.HexToColor(P_Colors_string[4]), Calc.HexToColor(P_Colors_string[5])};
        P_Shatter.Color = cc?[0] ?? Calc.HexToColor("d3ffd4");
        P_Shatter.Color2 = cc?[1] ?? Calc.HexToColor("85fc87");
        P_Glow.Color = cc?[2] ?? Calc.HexToColor("a5fff7");
        P_Glow.Color2 = cc?[3] ?? Calc.HexToColor("6de081");
        P_Regen.Color = cc?[4] ?? Calc.HexToColor("a5fff7");
        P_Regen.Color2 = cc?[5] ?? Calc.HexToColor("6de081");

        depth_idle = data.Int("IdleDepth", 100);
        depth_OnPlayer = data.Int("OnPlayerDepth", 8999);
        base.Depth = depth_idle;
        //refill
        RefillDash = data.Int("RefillDashCount", 1);
        RefillStamina = data.Float("RefillStaminaAmount", 110f);
        DashRefillOperation = data.Attr("RefillDashOperation", "set").ToLower();//add, set
        StaminaRefillOperation = data.Attr("RefillStaminaOperation", "set").ToLower();//add, set
        DashRequirementCount = data.Int("DashCountRequirement", 1);
        StaminaRequirementCount = data.Float("StaminaCountRequirement", 20f);
        DashRequirementOperation = data.Attr("DashRequirementOperation", "less than").ToLower();//less than, equal to, greater than
        StaminaRequirementOperation = data.Attr("StaminaRequirementOperation", "less than").ToLower();//less than, equal to, greater than
        RequirementMode = data.Attr("RequirementMode", "either").ToLower();//both, either, stamina, dash, stamina only, dash only, always, never
        RefillMode = data.Attr("RefillMode", "both").ToLower();//both, neither, stamina only, dash only
        FreezeFrame = data.Int("FreezeFrames", 3);
        FreezeTime = FreezeFrame * Engine.DeltaTime;
        RespawnTime = data.Float("RespawnTime", 2.5f);
        OneUse = data.Bool("OneUse", false);
    }

    private void OnPlayer(Player player) {
        if (AnalyzeRefill(player)) {
            Audio.Play("event:/game/general/diamond_touch", Center);
            Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
            Collidable = RespawnTime == 0f;
            Add(new Coroutine(RefillRoutine(player)));
        }
    }
    private bool AnalyzeRefill(Player player)
    {
        bool refillC = false, dashC = false, staminaC = false;
        if(DashRequirementOperation == "less than"){if(player.Dashes < DashRequirementCount){dashC = true;}}
        if(DashRequirementOperation == "equal to"){if(player.Dashes == DashRequirementCount){dashC = true;}}
        if(DashRequirementOperation == "greater than"){if(player.Dashes > DashRequirementCount){dashC = true;}}
        if(StaminaRequirementOperation == "less than"){if(player.Stamina < StaminaRequirementCount){staminaC = true;}}
        if(StaminaRequirementOperation == "equal to"){if(player.Stamina == StaminaRequirementCount){staminaC = true;}}
        if(StaminaRequirementOperation == "greater than"){if(player.Stamina > StaminaRequirementCount){staminaC = true;}}
        if(RequirementMode == "both"){refillC = dashC && staminaC;}
        if(RequirementMode == "either"){refillC = dashC || staminaC;}
        if(RequirementMode == "stamina"){refillC = staminaC;}
        if(RequirementMode == "dash"){refillC = dashC;}
        if(RequirementMode == "stamina only"){refillC = !dashC && staminaC;}
        if(RequirementMode == "dash only"){refillC = dashC && !staminaC;}
        if(RequirementMode == "always"){refillC = true;}
        if(RequirementMode == "never"){refillC = false;}
        if (refillC)
        {
            if(RefillMode == "both" || RefillMode == "dash only"){
                if(DashRefillOperation == "add"){player.Dashes += RefillDash;}
                else if(DashRefillOperation == "set"){player.Dashes = RefillDash;}
            }
            if(RefillMode == "both" || RefillMode == "stamina only")
            {
                if(StaminaRefillOperation == "add"){player.Stamina += RefillStamina;}
                else if(StaminaRefillOperation == "set"){player.Stamina = RefillStamina;}
            }
        }
        return refillC;
    }
    private IEnumerator RefillRoutine(Player player) {
        if (FreezeTime > 0)
            Celeste.Freeze(FreezeTime);
        yield return null;
        level.Shake();
        if (!OneUse) {
            respawn_timer = RespawnTime;
        }

        Depth = depth_OnPlayer;
        yield return 0.05f;
        float num = player.Speed.Angle();
        level.ParticlesFG.Emit(P_Shatter, 5, Position + Collider.Center, Vector2.One * 4f, num - MathF.PI / 2f);
        level.ParticlesFG.Emit(P_Shatter, 5, Position + Collider.Center, Vector2.One * 4f, num + MathF.PI / 2f);
        SlashFx.Burst(player.Position, num);
        if (OneUse) {
            RemoveSelf();
        }
    }
    public override void Added(Scene scene) {
        base.Added(scene);
        level = SceneAs<Level>();
    }
    public override void Awake(Scene scene){base.Awake(scene);}
    public override void Update() {
        base.Update();
        if (respawn_timer > 0f) {
            respawn_timer = Math.Max(0f, respawn_timer - Engine.DeltaTime);
            if (respawn_timer == 0f) {
                Respawn();
            }
        } else if (base.Scene.OnInterval(0.1f)) {
            level.ParticlesFG.Emit(P_Glow, 1, Position + Collider.Center, Vector2.One * 5f);
        }
    }
    private void Respawn() {
        if (!Collidable && !OneUse)
        {
            Collidable = true;
            sprite.Visible = true;
            base.Depth = depth_idle;
            Audio.Play("event:/game/general/diamond_return", Position);
            level.ParticlesFG.Emit(P_Regen, 16, Position + Collider.Center, Vector2.One * 2f);
        }
    }
    public override void Render() {
        Camera camera = SceneAs<Level>().Camera;
        if (base.Right < camera.Left || base.Left > camera.Right || base.Bottom < camera.Top || base.Top > camera.Bottom) {
            return;
        }
        Color color1, color2;
        if (respawn_timer > 0) 
        {
            color1 = Color.Transparent;
            color2 = color_outside * 0.25f;
            sprite.Color = Color.White * 0.25f;
            int i;
            for (i = 0; i < Width; i += 8) {
                Draw.Line(TopLeft + Vector2.UnitX * (i + 2), TopLeft + Vector2.UnitX * (i + 6), color2);
                Draw.Line(BottomLeft + Vector2.UnitX * (i + 2), BottomLeft + Vector2.UnitX * (i + 6), color2);
            }
            for (i = 0; i < Height; i += 8) {
                Draw.Line(TopLeft + Vector2.UnitY * (i + 2), TopLeft + Vector2.UnitY * (i + 6), color2);
                Draw.Line(TopRight + Vector2.UnitY * (i + 2), TopRight + Vector2.UnitY * (i + 6), color2);
            }
        } else if (OneUse)
        {
            color1 = color_inside;
            color2 = color_outside;
            sprite.Color = Color.White;
            Draw.HollowRect(X - 1, Y - 1, Width + 2, Height + 2, color2 * 0.6f);
            int i;
            for (i = 0; i < Width; i += 8) {
                Draw.Line(TopLeft - Vector2.UnitY + Vector2.UnitX * (i + 2), TopLeft - Vector2.UnitY + Vector2.UnitX * (i + 6), color2);
                Draw.Line(BottomLeft + Vector2.UnitX * (i + 2), BottomLeft + Vector2.UnitX * (i + 6), color2);
            }
            for (i = 0; i < Height; i += 8) {
                Draw.Line(TopLeft + Vector2.UnitY * (i + 2), TopLeft + Vector2.UnitY * (i + 6), color2);
                Draw.Line(TopRight + Vector2.UnitX + Vector2.UnitY * (i + 2), TopRight + Vector2.UnitX + Vector2.UnitY * (i + 6), color2);
            }
            Draw.Rect(X + 1, Y + 1, Width - 2, Height - 2, color1);
            sprite.Render();
        } else
        {
            color1 = color_inside;
            color2 = color_outside;
            sprite.Color = Color.White;
            Draw.HollowRect(X - 1, Y - 1, Width + 2, Height + 2, color2);
            Draw.Rect(X + 1, Y + 1, Width - 2, Height - 2, color1);
        }
        sprite.Render();
        base.Render();
    }

}