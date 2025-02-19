using Godot;
using System;

public partial class Tile : Node2D
{
	[Signal] public delegate void SelectedEventHandler(Tile tile);
	[Export] protected Area2D area2D;
	protected Piece piece;
	protected bool listening = true;
	[Export] public Piece Piece
	{
		get => piece;
		set => piece = value;
	}
	protected int row, column;
	public int Row
	{
		get => row;
		set => row = value;
	}
	public int Column
	{
		get => column;
		set => column = value;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area2D.InputEvent += OnArea2DInputEvent;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnArea2DInputEvent(Node viewPort, InputEvent inputEvent, long shapeIndex)
	{
		if(inputEvent.IsActionPressed("select") && listening)
		{
			GD.Print($"Selected {Name}");
		}
		EmitSignal(SignalName.Selected, this);
	}
	public void UpdateArea2D()
	{
		if(piece == null)
		{
			listening = true;
		}
		else
		{
			listening = false;
		}
	}
}
