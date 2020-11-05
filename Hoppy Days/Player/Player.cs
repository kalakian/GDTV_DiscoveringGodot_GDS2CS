using Godot;
using System;

public class Player : KinematicBody2D
{
    AnimatedSprite _animatedSprite;

    Vector2 _motion = new Vector2();
    const int _speed = 1500;
    const int _gravity = 300;
    const int _jumpSpeed = 4000;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;

        ApplyGravity();
        Jump();
        Move();
        Animate();

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

    public void Animate()
    {
        if (_motion.y < 0)
        {
            _animatedSprite.Play("jump");
        }
        else if (_motion.x != 0)
        {
            _animatedSprite.Play("walk");
        }
        else
        {
            _animatedSprite.Play("idle");
        }
        _animatedSprite.FlipH = (_motion.x < 0);
    }
}
