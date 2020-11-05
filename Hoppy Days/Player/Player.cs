using Godot;
using System;

public class Player : KinematicBody2D
{
    PlayerAnimation _playerAnimation;

    Vector2 _motion = new Vector2();
    const int _speed = 1500;
    const int _gravity = 300;
    const int _jumpSpeed = 4000;

    public override void _Ready()
    {
        _playerAnimation = GetNode<PlayerAnimation>("PlayerAnimation");
    }


    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;

        ApplyGravity();
        Jump();
        Move();
        _playerAnimation.Animate(_motion);

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
        if (Input.IsActionPressed("jump") && IsOnFloor())
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
