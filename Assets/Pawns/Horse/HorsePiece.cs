using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class HorsePiece : ChessPiece
{
	public GameObject missle;
	public HorsePiece(){
		this.Type = "horse";
		this.LockRange = 2;
	}
    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX+1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX-1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));

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
	
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
		ArrayList possibleAttacks = new ArrayList();
		
		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY+2));
			}
		}

		RemoveOutRangeAttack(possibleAttacks, frontLines);
		return possibleAttacks;
	}

    public override void CheckSpecial(Board board, ChessPiece[,] pieces)
    {
        // base.CheckSpecial();
		int selfX = (int)boardPosition.x;
		int selfY = (int)boardPosition.y;
		ChessPiece front = pieces[selfX+(red? 1: -1), selfY];
		String d = $"";
		// d += $"Checking {(selfX+(red? -1: 1), selfY)}";	///////////	DEBUG
		// Debug.Log(d);	///////////	DEBUG
		if(front != null){
			if(front.Type == "cannon"){
				d = "Horse Cannon Special Attack!: ";
				// MoveAttack = true;
				// d += $"Checking {(selfX+1, selfY+(red? -2: 2))} and {(selfX-1, selfY+(red? -2: 2))}";	///////////	DEBUG

				// ChessPiece target = pieces[selfX+(red? -2: 2), selfY+1];
				int targetX = selfX+(red? -2: 2);
				int targetY = selfY+1;
				if(pieces[targetX, targetY] != null){
					if(pieces[targetX, targetY].GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG
						board.RemovePiece(targetX, targetY);
					}
				}

				targetX = selfX+(red? -2: 2);
				targetY = selfY-1;
				if(pieces[targetX, targetY] != null){
					if(pieces[targetX, targetY].GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG
						board.RemovePiece(targetX, targetY);
					}
				}
				Debug.Log(d);	///////////	DEBUG
			}
		}

    }
	
	public override void AttackAnimation(Vector3 startPosition, Vector2 start, Vector2 target){
		Vector3 targetPosition = startPosition + new Vector3((target.y - start.y) * 20f, (target.x - start.x) * 20f, 0.0f);
		LaunchMissle(startPosition, targetPosition);
	}

	IEnumerator LaunchMissle(Vector3 startPosition, Vector3 targetPosition){
		GameObject missleInstance = Instantiate(missle, startPosition, UnityEngine.Quaternion.identity);
		missleInstance.transform.LookAt(targetPosition);
		missleInstance.transform.Rotate(90, 0, 0);
		float time = 0;
		float duration = 1f;
		while(time < duration){
			missleInstance.transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(missleInstance);

	}
}
