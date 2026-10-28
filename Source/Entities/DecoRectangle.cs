using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Cil;
namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity("EmpyrealCity/DecoRectangle")]
public class DecoRectangle : Entity {
    private Level level;
    public Color color;
    public Color inner_color;
    public float alpha;
    public bool DashLine;
    public int image_depth;
    public bool up_border, down_border, left_border, right_border;
    public DecoRectangle(EntityData data, Vector2 offset)
        : base(data.Position + offset) {
        base.Collider = new Hitbox(data.Width, data.Height);
        color = Calc.HexToColor(data.Attr("OutlineColor", "ff0000"));
        inner_color = Calc.HexToColor(data.Attr("FillColor", "ffaaaa"));
        alpha = Math.Min(Math.Max(data.Float("Alpha", 0.2f), 0.0f), 1.0f);
        image_depth = data.Int("Depth", 8999);
        base.Depth = image_depth;
        DashLine = data.Bool("DashLine", false);
        up_border = data.Bool("UpBorder", true);
        down_border = data.Bool("DownBorder", true);
        left_border = data.Bool("LeftBorder", true);
        right_border = data.Bool("RightBorder", true);
    }
    public override void Added(Scene scene) {
        base.Added(scene);
        level = SceneAs<Level>();
    }
    public override void Awake(Scene scene){base.Awake(scene);}
    public override void Update() {base.Update();}
    public override void Render() {
        Camera camera = SceneAs<Level>().Camera;
        if (base.Right < camera.Left || base.Left > camera.Right || base.Bottom < camera.Top || base.Top > camera.Bottom) {
            return;
        }

        Color color1, color2;
        if (DashLine)
        {
            color1 = inner_color * alpha;
            color2 = color;
            Draw.HollowRect(X, Y, Width, Height, color2 * 0.4f);
            int i;
            for (i = 0; i < Width; i += 8) {
                if (up_border) Draw.Line(TopLeft + Vector2.UnitX * (i + 2), TopLeft + Vector2.UnitX * (i + 6), color2);
                if (down_border) Draw.Line(BottomLeft - Vector2.UnitY + Vector2.UnitX * (i + 2), BottomLeft - Vector2.UnitY + Vector2.UnitX * (i + 6), color2);
            }
            for (i = 0; i < Height; i += 8) {
                if (left_border) Draw.Line(TopLeft + Vector2.UnitX + Vector2.UnitY * (i + 2), TopLeft + Vector2.UnitX + Vector2.UnitY * (i + 6), color2);
                if (right_border) Draw.Line(TopRight + Vector2.UnitY * (i + 2), TopRight + Vector2.UnitY * (i + 6), color2);
            }
        }
        else
        {
            color1 = inner_color * alpha;
            color2 = color;
            //Draw.HollowRect(X, Y, Width, Height, color2);
            if (up_border) Draw.Line(TopLeft, TopLeft + Vector2.UnitX * Width, color2);
            if (down_border) Draw.Line(BottomLeft - Vector2.UnitY, BottomLeft - Vector2.UnitY + Vector2.UnitX * Width, color2);
            if (left_border) Draw.Line(TopLeft + Vector2.UnitX, TopLeft + Vector2.UnitX + Vector2.UnitY * Height, color2);
            if (right_border) Draw.Line(TopRight, TopRight + Vector2.UnitY * Height, color2);
        }
        Draw.Rect(X + 1 - (left_border ? 0 : 1), Y + 1 - (up_border ? 0 : 1), Width - 2 + (left_border ? 0 : 1) + (right_border ? 0 : 1), Height - 2 + (up_border ? 0 : 1) + (down_border ? 0 : 1), color1);
        base.Render();
    }
}