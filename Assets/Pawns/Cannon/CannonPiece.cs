using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CannonPiece : ChessPiece
{

	public GameObject beamParticleEffect;

	private GameObject currentBeam;
	private float timer = -1f;
	private LineRenderer lr;
	public CannonPiece(){
		this.Type = "cannon";
		this.LockRange = 2;
		this.MoveAttack = false;
	}


    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		for (int i = startX + 1; i < 10; i++){
			if(pieces[i, startY] != null){
				// if(GetRed() != pieces[i, startY].GetRed()){
				// 	possibleMoves.Add(new Vector2(i, startY));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startX - 1; i > -1; i--){
			if(pieces[i, startY] != null){
				// if(GetRed() != pieces[i, startY].GetRed()){
				// 	possibleMoves.Add(new Vector2(i, startY));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startY + 1; i < 9; i++){
			if(pieces[startX, i] != null){
				// if(GetRed() != pieces[startX, i].GetRed()){
				// 	possibleMoves.Add(new Vector2(startX, i));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		for (int i = startY - 1; i > -1; i--){
			if(pieces[startX, i] != null){
				// if(GetRed() != pieces[startX, i].GetRed()){
				// 	possibleMoves.Add(new Vector2(startX, i));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		return possibleMoves;
		/**
		for (int i = startX + 1; i < 10; i++){
			if(pieces[i, startY] != null){
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startX - 1; i > -1; i--){
			if(pieces[i, startY] != null){
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startY + 1; i < 9; i++){
			if(pieces[startX, i] != null){
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		for (int i = startY - 1; i > -1; i--){
			if(pieces[startX, i] != null){
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		int start;
		int end;
		//X attack
		for (int i = 0; i < 10; i++){
			if(pieces[i, startY] != null){
				int countBetween = 0;
				if(pieces[i, startY].GetRed() == GetRed()){}
				else{
					if(startX<i){start=startX; end=i;}else{start=i; end=startX;}
					for(int j=start+1; j<end; j++){
						if(pieces[j, startY] != null)countBetween++;
					}
					if(countBetween == 1){
						possibleMoves.Add(new Vector2(i, startY));
					}
				}
			}
		}
		//Y attack
		for (int i = 0; i < 9; i++){
			if(pieces[startX, i] != null){
				int countBetween = 0;
				if(pieces[startX, i].GetRed() ==GetRed()){}
				else{
					if(startY<i){start=startY; end=i;}else{start=i; end=startY;}
					for(int j=start+1; j<end; j++){
						if(pieces[startX, j] != null)countBetween++;
					}
					if(countBetween == 1){
						possibleMoves.Add(new Vector2(startX, i));
					}
				}
			}
		}
		**/
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX+1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX-1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));

		// return possibleMoves;
    }
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
			ArrayList possibleAttacks = new ArrayList();

			int start;
			int end;
			//X attack
			// String debug = $"Check Attack Position: ";	///////////	DEBUG
			for (int i = 0; i < 10; i++){
				if(pieces[i, startY] != null){
					int countBetween = 0;
					if(pieces[i, startY].GetRed() == GetRed()){}
					else{
						if(startX<i){start=startX; end=i;}else{start=i; end=startX;}
						for(int j=start+1; j<end; j++){
							if(pieces[j, startY] != null)countBetween++;
						}
						if(countBetween == 1){
							// debug += $"{new Vector2(i, startY)}, ";	/////////	DEBUG
							// Debug.Log()
							checkAndAddAttack(pieces, possibleAttacks, new Vector2(i, startY));
						}
					}
				}
			}
			// Debug.Log(debug);	/////////////////////////////	DEBUG

			//Y attack
			for (int i = 0; i < 9; i++){
				if(pieces[startX, i] != null){
					int countBetween = 0;
					if(pieces[startX, i].GetRed() == GetRed()){}
					else{
						if(startY<i){start=startY; end=i;}else{start=i; end=startY;}
						for(int j=start+1; j<end; j++){
							if(pieces[startX, j] != null)countBetween++;
						}
						if(countBetween == 1){
							checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX, i));
						}
					}
				}
			}
			RemoveOutRangeAttack(possibleAttacks, frontLines);
			return possibleAttacks;
		}
		
	public override void AttackAnimation(Board board, Vector3 startPosition, Vector2 start, ChessPiece targetPiece, int endX, int endY){
		int targetX = (int) targetPiece.GetBoardPosition().x;
		int targetY = (int) targetPiece.GetBoardPosition().y;
		currentBeam = Instantiate(beamParticleEffect, startPosition, Quaternion.identity);
		
		// Set the end point of the beam
        LineRenderer lineRenderer = currentBeam.GetComponentInChildren<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, startPosition + new Vector3((targetY - start.y) * 20f, (targetX - start.x) * 20f, 0.0f));
        }

		timer = 0f;
		board.RemovePieceAfterAnimation(targetPiece);
		if(MoveAttack){
			board.MovePieceAfterAnimation(this, endX, endY);
		}
	}

	public void Update(){
		if(currentBeam != null){
			LineRenderer lineRenderer = currentBeam.GetComponentInChildren<LineRenderer>();

			if(timer >= 0 && timer < 0.5){
				timer += Time.deltaTime;
			}else{
				timer = -1f;
				lineRenderer.enabled = false;
			}
		}
	}

    public override void CheckSpecial(Board board, ChessPiece[,] pieces)
    {
        // base.CheckSpecial();
		int selfX = (int)boardPosition.x;
		int selfY = (int)boardPosition.y;
		ChessPiece front = pieces[selfX+(red? -1: 1), selfY];
		String d = $"";
		// d += $"Checking {(selfX+(red? -1: 1), selfY)}";	///////////	DEBUG
		// Debug.Log(d);	///////////	DEBUG
		if(front != null){
			if(front.Type == "horse"){
				d = "Horse Cannon Special Attack!: ";
				// MoveAttack = true;
				// d += $"Checking {(selfX+1, selfY+(red? -2: 2))} and {(selfX-1, selfY+(red? -2: 2))}";	///////////	DEBUG

				// ChessPiece target = pieces[selfX+(red? -2: 2), selfY+1];
				int targetX = selfX+(red? -3: 3);
				int targetY = selfY+1;
				ChessPiece target = pieces[targetX, targetY];
				if(target != null){
					if(target.GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG

						pieces[targetX, targetY] = null;
						board.RemovePieceAfterAnimation(target);
					}
				}

				targetX = selfX+(red? -3: 3);
				targetY = selfY-1;
				target = pieces[targetX, targetY];
				if(target != null){
					if(target.GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG
						pieces[targetX, targetY] = null;
						board.RemovePieceAfterAnimation(target);
					}
				}
				Debug.Log(d);	///////////	DEBUG
			}
		}
    }
}
