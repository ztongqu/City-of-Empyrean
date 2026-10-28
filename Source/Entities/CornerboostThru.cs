using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System.Collections.Generic;


namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity("EmpyrealCity/CornerboostThru")]
public class CornerboostThru : Solid {
    private Level level;
    private int height;
    private int orig_width;
    private int cur_width;
    private int columns;
    private int rows;
    private int Direction;
    private String Texture;
    private int Spacing;
    private int extra_jumpthrus;

    //I might have chosen the wackiest way to do this...
    public CornerboostThru(EntityData data, Vector2 offset)
        : base(data.Position + offset, data.Width, data.Height, safe : true) {
        orig_width = data.Width;
        cur_width = data.Width;
        height = data.Height;
        base.Collider = new Hitbox(cur_width, height);
        base.Depth = data.Int("Depth", -12999);
        Direction = data.Bool("Left", false) ? 1 : -1; //1 = right, -1 = left
        Texture = data.Attr("Texture", "dream");
        base.SurfaceSoundIndex = data.Int("SurfaceIndex", -1);
        Spacing = data.Int("Spacing", 2);
        extra_jumpthrus = data.Int("ExtraJumpthrus", 0);
        columns = orig_width / Spacing + 1 + extra_jumpthrus;
        rows = height / 8;

    }

    public override void Added(Scene scene) {
        base.Added(scene);
        level = SceneAs<Level>();
        /*
        int tilesX = (int)base.Width / 8;
        int tilesY = (int)base.Height / 8;
        if (BlendIn)
        {
            Level level = SceneAs<Level>();
            Rectangle tileBounds = level.Session.MapData.TileBounds;
            VirtualMap<char> solidsData = level.SolidsData;
            int x = (int)base.X / 8 - tileBounds.Left;
            int y = (int)base.Y / 8 - tileBounds.Top;
            tiles = GFX.FGAutotiler.GenerateOverlay(TileType, x, y, tilesX, tilesY, solidsData).TileGrid;
        }
        else
        {
            tiles = GFX.FGAutotiler.GenerateBox(TileType, tilesX, tilesY).TileGrid;
        }

        Add(tiles);
        Add(new TileInterceptor(tiles, highPriority: true));
        */
        MTexture mTexture = GFX.Game["objects/jumpthru/" + Texture];
        int num = mTexture.Width / 8;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int num2, num3;
                if (i == 0)
                {
                    num2 = Direction == 1 ? 0 : num - 1;
                    num3 = (!CollideCheck<Solid, SwapBlock, ExitBlock>(Position + new Vector2(0f, -1f))) ? 1 : 0;
                }
                else if (i == rows - 1)
                {
                    num2 = Direction == 1 ? num - 1 : 0;
                    num3 = (!CollideCheck<Solid, SwapBlock, ExitBlock>(Position + new Vector2(0f, 1f))) ? 1 : 0;
                }
                else
                {
                    num2 = 1 + Calc.Random.Next(num - 2);
                    num3 = Calc.Random.Choose(0, 1);
                }

                Image image = new Image(mTexture.GetSubtexture(num2 * 8, num3 * 8, 8, 8));
                image.Rotation = (float)(Math.PI / 2f * Direction);
                image.Y = 8 * (Direction == 1 ? i : i + 1);
                image.X = Spacing * (Direction == 1 ? j - extra_jumpthrus : j);
                Add(image);
            }
        }
    }

    public override void Awake(Scene scene) {
        base.Awake(scene);
    }

    public override void Update()
    {
        base.Update();

        Player player = base.Scene.Tracker.GetEntity<Player>();
        if (player != null) {
            //not collidable if player is not aligned horizontally with the block (simulate vertical jumpthru)
            base.Collidable = !(player.Collider.AbsoluteBottom + player.Speed.Y * Engine.DeltaTime - 1f < base.Collider.AbsoluteTop || player.Collider.AbsoluteTop + player.Speed.Y * Engine.DeltaTime + 1f > base.Collider.AbsoluteBottom);


            //the hitbox of the block shrinks so the player is always few pixels away from the block but within the range of climb jump (need Soild Modifier from Viv Helper)
            float player_true_speed;
            float extra_space;
            if (Direction == -1) {
                player_true_speed = player.wallSpeedRetentionTimer == 0f ? player.Speed.X : Math.Max(player.wallSpeedRetained, player.Speed.X);
                extra_space = Math.Max(player_true_speed * Engine.DeltaTime, 2);
                cur_width = (int)(base.X + orig_width - player.Collider.AbsoluteRight - extra_space);
                if (cur_width > 0 && cur_width <= orig_width) {
                    base.Collider = new Hitbox(cur_width, height, orig_width - cur_width, 0);
                }
                else if (cur_width <= 0) {
                    base.Collider = new Hitbox(orig_width, height);
                    base.Collidable = false;
                }
                else {
                    base.Collider = new Hitbox(orig_width, height);
                }
            }
            else if (Direction == 1) {
                player_true_speed = player.wallSpeedRetentionTimer == 0f ? player.Speed.X : Math.Min(player.wallSpeedRetained, player.Speed.X);
                extra_space = Math.Max(-1 * player_true_speed * Engine.DeltaTime, 2);
                cur_width = (int)(player.Collider.AbsoluteLeft - base.X - extra_space);
                if (cur_width > 0 && cur_width <= orig_width) {
                    base.Collider = new Hitbox(cur_width, height, 0, 0);
                }
                else if (cur_width <= 0) {
                    base.Collider = new Hitbox(orig_width, height);
                    base.Collidable = false;
                }
                else {
                    base.Collider = new Hitbox(orig_width, height);
                }
            }
        }
        else
        {
            base.Collider = new Hitbox(orig_width, height);
            base.Collidable = true;
        }
        

    }

}