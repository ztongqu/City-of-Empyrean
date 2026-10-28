using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System.Collections;
using System.Collections.Generic;


namespace Celeste.Mod.EmpyrealCity.Entities;

[CustomEntity("EmpyrealCity/DashThruJumpThru")]
public class DashThruJumpThru : JumpthruPlatform {
    private Level level;
    public bool red_dash_counts_as_dash;
    public int delay;
    bool analyser, old_analyser;
    bool[] ActionList;
    int ActionIndex, ActionIndexDelayed;
    public DashThruJumpThru(EntityData data, Vector2 offset)
        : base(data.Position + offset, data.Width, data.Attr("Texture", "dream"), data.Int("SurfaceIndex", -1)) {
        base.Collider = new Hitbox(data.Width, 5f);
        base.Depth = data.Int("Depth", -60);
        base.Collidable = true;
        red_dash_counts_as_dash = data.Bool("RedDashCountsAsDash", false);
        delay = Math.Max(data.Int("delay", 0), 0);
        ActionList = new bool[delay + 1];
        for (int i = 0; i < delay + 1; i++)
        {
            ActionList[i] = false;
        }
        ActionIndex = 0;
        ActionIndexDelayed = delay;
    }

    public override void Added(Scene scene) {
        base.Added(scene);
        level = SceneAs<Level>();
    }

    public override void Awake(Scene scene) {
        analyser = false;
        old_analyser = false;
        base.Awake(scene);
    }

    public override void Update()
    {
        base.Update();

        Player player = base.Scene.Tracker.GetEntity<Player>();
        if (player != null)
        {
            int state = player.StateMachine.state;
            old_analyser = analyser;
            analyser = state == 2 || (red_dash_counts_as_dash && state == 5);
            if (delay == 0) {
                base.Collidable = !analyser;
            }
            else if (delay == 1) {
                base.Collidable = !old_analyser;
            }
            /*
            else {
                if (old_analyser != analyser) {
                    Add(new Coroutine(ChangeCollide(analyser, delay)));
                }
            }
            */
            else {
                ActionList[ActionIndexDelayed] = analyser;
                base.Collidable = !ActionList[ActionIndex];

                ActionIndex = (ActionIndex + 1) % (delay + 1);
                ActionIndexDelayed = (ActionIndex + delay) % (delay + 1);
            }
        }
        else
        {
            base.Collidable = true;
        }
    }
    /*
    public IEnumerator ChangeCollide(bool analyser, int delay)
    {
        yield return Engine.DeltaTime * (delay - 2 - (delay > 8 && delay < 20 ? 1 : 0));
        //It delays one extra frame when the delay between 9 and 19 if no adjustments are made. I don't know what other values could cause this.
        base.Collidable = !analyser;
    }
    */
}