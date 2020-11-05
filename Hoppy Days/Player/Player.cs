using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 _motion = new Vector2();
    const int _speed = 1000;
    const int _gravity = 300;
    const int _jumpSpeed = 3000;

    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;

        ApplyGravity();
        Jump();
        Move();
        MoveAndSlide(_motion, Vector2.Up);
    }

    public void ApplyGravity()
    {
        if (!IsOnFloor())
        {
            _motion.y += _gravity;
        }
        else
        {
            _motion.y = 0;
        }
    }

    public void Jump()
    {
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            _motion.y -= _jumpSpeed;
        }
    }

    public void Move()
    {
        if (Input.IsActionPressed("left"))
        {
            _motion.x -= _speed;
        }
        if (Input.IsActionPressed("right"))
        {
            _motion.x += _speed;
        }
    }
}
