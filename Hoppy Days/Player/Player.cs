using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 motion = new Vector2();
    const float speed = 500;

    public override void _PhysicsProcess(float delta)
    {
        motion.x = 0;

        if (Input.IsActionPressed("left"))
        {
            motion.x -= speed;
        }
        if (Input.IsActionPressed("right"))
        {
            motion.x += speed;
        }

        MoveAndSlide(motion);

        base._PhysicsProcess(delta);
    }
}
