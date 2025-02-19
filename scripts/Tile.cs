using Godot;
using System;

public partial class Tile : Node2D
{
	private Piece piece;
	[Export] public Piece Piece
	{
		get => piece;
		set => piece = value;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
