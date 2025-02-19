using Godot;
using System;

public partial class Game : Node2D
{
	[Export] protected Board board;
	protected Piece selectedPiece;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		board.TileCreated += OnTileCreated;
		board.PieceCreated += OnPieceCreated;
		board.LoadBoard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnTileCreated(Tile tile)
	{
		tile.Selected += OnTileSelected;
	}
	public void OnPieceCreated(Piece piece)
	{
		piece.Selected += OnPieceSelected;
	}
	public void OnTileSelected(Tile tile)
	{
		if(selectedPiece != null)
		{
			MoveSelectedPieceToTile(tile);
		}
	}
	public void OnPieceSelected(Piece piece)
	{
		selectedPiece = piece;
	}
	public void MoveSelectedPieceToTile(Tile tile)
	{
		tile.Piece = selectedPiece;
		tile.UpdateArea2D();

		Tile oldTile = board.GetTile(selectedPiece.TileColumn, selectedPiece.TileRow);
		oldTile.Piece = null;
		oldTile.UpdateArea2D();

		selectedPiece.TileColumn = tile.Column;
		selectedPiece.TileRow = tile.Row;
		selectedPiece.Position = board.TilePositionToBoardPosition(tile.Column, tile.Row);
	}
}
