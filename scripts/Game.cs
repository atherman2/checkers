using Godot;
using System;

public partial class Game : Node2D
{
	[Export] protected Board board;
	protected Piece selectedPiece;
	protected Piece.PlayerSide playerTurn = Piece.PlayerSide.RED;
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
			int rowDirection;
			if(playerTurn == Piece.PlayerSide.BLACK)
				rowDirection = 1;
			else
				rowDirection = -1;
			if(tile.Piece == null)
			{
				if(selectedPiece.IsKing)
				{
				}
				if
				(
					(tile.Row == selectedPiece.TileRow + rowDirection)
					&&
					(
						(tile.Column == selectedPiece.TileColumn + 1)
						|| (tile.Column == selectedPiece.TileColumn - 1)
					)
				)
				MoveSelectedPieceToTile(tile);
			}
		}
	}
	public void OnPieceSelected(Piece piece)
	{
		if(playerTurn == piece.Side)
			selectedPiece = piece;
		else
			selectedPiece = null;
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
