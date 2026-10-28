using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.EmpyrealCity.Module;
using System.Collections;

namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity("EmpyrealCity/CustomRefill")]
public class CustomRefill : Entity {

    private Level level;
    public string texturePath;
    private Image outline;
    private Sprite sprite;
    private Sprite flash;
    public int depth_idle;
    public int depth_OnPlayer;
    public ParticleType P_Shatter, P_Glow, P_Regen;
    private string[] P_Colors_string;
    public string wobble;
    private Wiggler wiggler;
    private SineWave[] sine;
    private BloomPoint bloom;
    private VertexLight light;
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

    public CustomRefill(EntityData data, Vector2 offset)
        : base(data.Position + offset)
    {
        base.Collider = new Hitbox(16f, 16f, -8f, -8f);
        Add(new PlayerCollider(OnPlayer));
        //sprite
        texturePath = data.Attr("Directory", "objects/refill/").TrimEnd('/') + "/";
        Add(outline = new Image(GFX.Game[texturePath + "outline"]));
        outline.CenterOrigin();
        outline.Visible = false;
        Add(sprite = new Sprite(GFX.Game, texturePath + "idle"));
        sprite.AddLoop("idle", "", 0.1f);
        sprite.Play("idle");
        sprite.CenterOrigin();
        Add(flash = new Sprite(GFX.Game, texturePath + "flash"));
        flash.Add("flash", "", 0.05f);
        flash.OnFinish = delegate {
            flash.Visible = false;
        };
        flash.CenterOrigin();

        Add(bloom = new BloomPoint(0.8f, 16f));
        Add(light = new VertexLight(Calc.HexToColor("ffffff"), 1, 16, 48));
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


        Add(wiggler = Wiggler.Create(1f, 4f));

        depth_idle = data.Int("IdleDepth", -100);
        depth_OnPlayer = data.Int("OnPlayerDepth", 8999);
        base.Depth = depth_idle;
        //wobble
        wobble = data.Attr("WobbleType", "YOnly");
        sine = new SineWave[2];
        sine[0] = new SineWave(0.6f);
        sine[1] = new SineWave(0.6f);
        Add(sine[0]);
        Add(sine[1]);
        sine[0].Randomize();
        sine[1].Counter = (sine[0].Value + (MathF.PI / 2f)) % (MathF.PI / 2f);
        UpdateWobble();
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
    private void UpdateWobble()
    {
        if (wobble == "XOnly" || wobble == "Circle") { float num1 = flash.X = sprite.X = sine[0].TwoValue; }
        if (wobble == "YOnly" || wobble == "Circle") { float num1 = flash.Y = sprite.Y = sine[1].TwoValue; }
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
        sprite.Visible = (flash.Visible = RespawnTime == 0f);
        if (!OneUse) {
            outline.Visible = RespawnTime != 0f;
            respawn_timer = RespawnTime;
        }

        Depth = depth_OnPlayer;
        yield return 0.05f;
        float num = player.Speed.Angle();
        level.ParticlesFG.Emit(P_Shatter, 5, Position, Vector2.One * 4f, num - MathF.PI / 2f);
        level.ParticlesFG.Emit(P_Shatter, 5, Position, Vector2.One * 4f, num + MathF.PI / 2f);
        SlashFx.Burst(Position, num);
        if (OneUse) {
            RemoveSelf();
        }
    }
    public override void Added(Scene scene) {
        base.Added(scene);
        level = SceneAs<Level>();
    }
    public override void Update() {
        base.Update();
        if (respawn_timer > 0f) {
            respawn_timer = Math.Max(0f, respawn_timer - Engine.DeltaTime);
            if (respawn_timer == 0f) {
                Respawn();
            }
        } else if (base.Scene.OnInterval(0.1f)) {
            level.ParticlesFG.Emit(P_Glow, 1, Position, Vector2.One * 5f);
        }
        UpdateWobble();
        light.Alpha = Calc.Approach(light.Alpha, sprite.Visible ? 1f : 0f, 4f * Engine.DeltaTime);
        bloom.Alpha = light.Alpha * 0.8f;
        if (base.Scene.OnInterval(2f) && sprite.Visible) {
            flash.Play("flash", restart: true);
            flash.Visible = true;
        }
    }
    private void Respawn() {
        if (!Collidable && !OneUse)
        {
            Collidable = true;
            sprite.Visible = true;
            outline.Visible = false;
            base.Depth = depth_idle;
            wiggler.Start();
            Audio.Play("event:/game/general/diamond_return", Position);
            level.ParticlesFG.Emit(P_Regen, 16, Position, Vector2.One * 2f);
        }
    }
    public override void Render() {
        if (sprite.Visible) {
            sprite.DrawOutline();
        }
        base.Render();
    }

}