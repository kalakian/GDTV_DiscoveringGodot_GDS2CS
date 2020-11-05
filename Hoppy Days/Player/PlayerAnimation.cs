using Godot;
using System;

public class PlayerAnimation : AnimatedSprite
{
    public void Animate(Vector2 motion)
    {
        if (motion.y < 0)
        {
            Play("jump");
        }
        else if (motion.x != 0)
        {
            Play("walk");
        }
        else
        {
            Play("idle");
        }
        FlipH = (motion.x < 0);
    }
}
