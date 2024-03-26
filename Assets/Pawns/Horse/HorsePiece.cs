using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsePiece : ChessPiece
{
    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();
		
		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY+2));
			}
		}

		return possibleMoves;
    }
}
