﻿// using UnityEngine;
// using System;
// using System.Collections;
// using System.Collections.Generic;

// public class Board : MonoBehaviour {

// 	public static Board Instance {set; get;}

// 	public ChessPiece[,] pieces = new ChessPiece[10,9];
// 	//Red gameobjects
// 	public GameObject chariotRed;
// 	public GameObject horseRed;
// 	public GameObject elephantRed;
// 	public GameObject advisorRed;
// 	public GameObject generalRed;
// 	public GameObject cannonRed;
// 	public GameObject soldierRed;
	
// 	//Blue gameobjects
// 	public GameObject chariotBlue;
// 	public GameObject horseBlue;
// 	public GameObject elephantBlue;
// 	public GameObject advisorBlue;
// 	public GameObject generalBlue;
// 	public GameObject cannonBlue;
// 	public GameObject soldierBlue;

// 	public GameObject redCurvyArrow;
// 	public GameObject blueCurvyArrow;
// 	float arrowStep = 1;

// 	public GameObject GeneralCheckedText;
// 	public GameObject invalidMoveText;
// 	public float invalidAlpha;
// 	public float generalAlpha;

// 	private float mouseOverX;
// 	private float mouseOverY;
// 	private Vector3 mousePosition;
// 	private Vector2 boardPosition;

// 	private ChessPiece selectedPiece;
// 	private Vector2 startDrag;
// 	private bool dragging = false;
// 	private Vector3 originalPosition;

// 	private bool moveCompleted;
// 	private bool isRedTurn = true;
// 	private bool isRed = true;

// 	private Vector2 generalRedPos;
// 	private Vector2 generalBluePos;
// 	private List<Vector2> redPiecesPos = new List<Vector2>();
// 	private List<Vector2> bluePiecesPos = new List<Vector2>();

// 	// private Client client;

// 	// Added variables
// 	private List<GameObject> indicators = new List<GameObject>();
// 	public GameObject indicator;
// 	private void Start()
// 	{
// 		Instance = this;
// 		// client = FindObjectOfType<Client>();
// 		// isRed = client.isHost;
// 		GenerateBoard();
// 		GeneralCheckedText = Instantiate(GeneralCheckedText);
// 		GeneralCheckedText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, 0f);
// 		invalidMoveText = Instantiate(invalidMoveText);
// 		invalidMoveText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, 0f);
// 		redCurvyArrow = Instantiate(redCurvyArrow);
// 		blueCurvyArrow = Instantiate(blueCurvyArrow);
// 		redCurvyArrow.transform.position = new Vector3(-139.9f, 81, 10f);
// 		blueCurvyArrow.transform.position = new Vector3(-139.9f, -81f, 300f);

// 		//ADDED
// 		// indicators = GameObject[];
// 	}

// 	private void Update()
// 	{
// 		UpdateMouseOver();

// 		//boardPosition : mouse position on board
// 		int x = (int) boardPosition.x;
// 		int y = (int) boardPosition.y;

// 		if(generalAlpha>0){
// 			generalAlpha -= 0.01f;
// 			GeneralCheckedText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, generalAlpha);
// 		}
// 		if(invalidAlpha>0){
// 			invalidAlpha -= 0.01f;
// 			invalidMoveText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, invalidAlpha);
// 		}

// 		//display arrow at the side of the playing one
// 		if(isRedTurn){
// 			if(redCurvyArrow.transform.position.x < -140){
// 				arrowStep = 1;
// 			}else if(redCurvyArrow.transform.position.x > -130){
// 				arrowStep = -1;
// 			}
// 			redCurvyArrow.transform.position = new Vector3(redCurvyArrow.transform.position.x + arrowStep, 81f, 10f);
// 		}else{
// 			if(blueCurvyArrow.transform.position.x < -140){
// 				arrowStep = 1;
// 			}else if(blueCurvyArrow.transform.position.x > -130){
// 				arrowStep = -1;
// 			}
// 			blueCurvyArrow.transform.position = new Vector3(blueCurvyArrow.transform.position.x + arrowStep, -81f, 10f);			
// 		}

// 		if(dragging && selectedPiece != null){
// 			DragPiece(selectedPiece);
// 		}
		
// 		if(Input.GetMouseButtonDown(0)){
// 			// Debug.Log("RED" + isRedTurn + "mouse down 0");
// 			// if(isRed == isRedTurn){
// 				SelectPiece(x, y);//assgin startDrag pos and selected in here
// 				if(selectedPiece != null && selectedPiece.GetRed()==isRedTurn){
// 					dragging = true;
// 					originalPosition = selectedPiece.transform.position;
// 				}
// 			// }
// 		}
		
// 		if(Input.GetMouseButtonUp(0)){
// 			if(selectedPiece != null && selectedPiece.GetRed()==isRedTurn){
// 				TryMove((int) startDrag.x, (int) startDrag.y, x, y);
// 				dragging = false;
// 				selectedPiece = null;
// 				if(moveCompleted){
// 					// string msg = "CMOV|";
// 					// msg += ((int)startDrag.x).ToString() + "|";
// 					// msg += ((int)startDrag.y).ToString() + "|";
// 					// msg += x.ToString() + "|";
// 					// msg += y.ToString();

// 					// client.Send(msg);

// 					if(isRedTurn){
// 						redCurvyArrow.transform.position = new Vector3(-139.9f, 81, 10f);
// 						blueCurvyArrow.transform.position = new Vector3(-139.9f, -81f, 300f);
// 					}else{
// 						redCurvyArrow.transform.position = new Vector3(-139.9f, 81, 300f);
// 						blueCurvyArrow.transform.position = new Vector3(-139.9f, -81f, 10f);
// 					}
// 				}
// 			}
// 		}
		
// 	}

// 	private void UpdateMouseOver()
// 	{
// 		if(!Camera.main) //Check if camera exists
// 		{
// 			Debug.Log("Unable to find main camera.");
// 			return;
// 		}

// 		mouseOverX = (Input.mousePosition.x);
//         mouseOverY = (Input.mousePosition.y);
//         mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,186.75f));
// 		boardPosition = GetBoardPosition(mousePosition.x, mousePosition.y);
// 	}

// 	private void SelectPiece(int x, int y)
// 	{
// 		if(x < 0 || x > 9 || y < 0 || y > 8){
// 			return;
// 		}

// 		ChessPiece p = pieces[x, y];

// 		if(p != null){
// 			selectedPiece = p;
// 			startDrag = boardPosition;
// 		}
// 	}

// 	private void DragPiece(ChessPiece sP)
// 	{
// 		if(sP.GetComponent<ChessPiece>().Type=="horse"){
// 			sP.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,172.45f));
// 		}else{
// 			sP.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,177.75f));
// 		}
// 	}

// 	public void TryMove(int startX, int startY, int endX, int endY)
// 	{
// 		startDrag = new Vector2(startX, startY);
// 		selectedPiece = pieces[startX, startY];
// 		Debug.Log($"Start is [{startX}, {startY}]. End is [{endX} {endY}] selected piece is "+ (selectedPiece.GetRed()? "red": "blue"));
// 		bool invalidMove = false;
// 		// if(isRed == isRedTurn){
// 		selectedPiece.transform.position = originalPosition;
// 		// }
// 		//endX endY is correct for blue
// 		Debug.Log($"{(selectedPiece.GetRed()? "red": "blue")}, endX {endX}, endY {endY}");
// 		//Check if in board
// 		if(endX >= 0 && endX < 10 && endY >= 0 && endY < 9 && selectedPiece != null){
// 			if(isValidMove(startX, startY, endX, endY, selectedPiece.Type)){
// 				if(!GeneralChecked(isRedTurn)){ //Normal nothing!
// 					// Debug.Log("Normal round");
// 					MovePiece(selectedPiece, endX, endY);
// 					moveCompleted = true;
// 					isRedTurn = ! isRedTurn;
// 					return;
// 				}else{
// 					ChessPiece[,] copyBoard = (ChessPiece[,]) pieces.Clone();
// 					Vector2 originalgeneralRedPos = new Vector2(generalRedPos.x, generalRedPos.y);
// 					Vector2 originalgeneralBluePos = new Vector2(generalBluePos.x, generalBluePos.y);
// 					Vector2 removePos = new Vector2(-1, -1);
// 					bool removePosIsRed = false;
// 					if(!isRedTurn){
// 						for(int i= 0; i<bluePiecesPos.Count; i++){
// 							if(bluePiecesPos[i].x == startX && bluePiecesPos[i].y == startY){
// 								if(bluePiecesPos[i].x == generalBluePos.x && bluePiecesPos[i].y == generalBluePos.y){
// 									generalBluePos = new Vector2(endX, endY);
// 								}
// 								if(pieces[endX, endY] != null){
// 									removePos = new Vector2(endX, endY);
// 									removePosIsRed = pieces[endX, endY].GetRed();
// 									RemovePiece(pieces[endX, endY].GetRed(), endX, endY);
// 								}
// 								bluePiecesPos[i] = new Vector2(endX, endY);
// 								pieces[endX, endY] = selectedPiece;
// 								pieces[startX, startY] = null;
// 							}
// 						}
// 					}else{
// 						for(int i= 0; i<redPiecesPos.Count; i++){
// 							if(redPiecesPos[i].x == startX && redPiecesPos[i].y == startY){
// 								if(redPiecesPos[i].x == generalRedPos.x && redPiecesPos[i].y == generalRedPos.y){
// 									generalRedPos = new Vector2(endX, endY);
// 								}
// 								if(pieces[endX, endY] != null){
// 									removePos = new Vector2(endX, endY);
// 									removePosIsRed = pieces[endX, endY].GetRed();
// 									RemovePiece(pieces[endX, endY].GetRed(), endX, endY);
// 								}
// 								redPiecesPos[i] = new Vector2(endX, endY);
// 								pieces[endX, endY] = selectedPiece;
// 								pieces[startX, startY] = null;
// 							}
// 						}
// 					}
// 					bool isGeneralChecked = GeneralChecked(isRedTurn);

// 					if(removePos.x != -1 && removePos.y != -1){
// 						if(removePosIsRed){
// 							redPiecesPos.Add(removePos);
// 						}else{
// 							bluePiecesPos.Add(removePos);
// 						}
// 					}

// 					if(isRedTurn){
// 						for(int i=0; i<redPiecesPos.Count; i++){
// 							if(redPiecesPos[i].x == endX && redPiecesPos[i].y == endY){
// 								redPiecesPos[i] = new Vector2(startX, startY);
// 							}
// 						}
// 					}else{
// 						for(int i=0; i<bluePiecesPos.Count; i++){
// 							if(bluePiecesPos[i].x == endX && bluePiecesPos[i].y == endY){
// 								bluePiecesPos[i] = new Vector2(startX, startY);
// 							}
// 						}
// 					}
// 					pieces = copyBoard;
// 					if(isGeneralChecked){
// 						generalAlpha = 1;
// 						GeneralCheckedText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, generalAlpha);
// 						moveCompleted = false;
// 						generalBluePos = originalgeneralBluePos;
// 						generalRedPos = originalgeneralRedPos;
// 						return;
// 					}else{
// 						generalBluePos = originalgeneralBluePos;
// 						generalRedPos = originalgeneralRedPos;
// 						MovePiece(selectedPiece, endX, endY);
// 						moveCompleted = true;
// 						isRedTurn = ! isRedTurn;
// 						return;
// 					}
// 				}
// 			}else{
// 				//Released at the orginal place
// 				invalidMove = true;

// 				// Show possible moving locations
// 				// indicators = 
// 			}
// 		}else{
// 			invalidMove = true;
// 		}
// 		if(invalidMove){
// 			invalidAlpha = 1;
// 			invalidMoveText.GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, invalidAlpha);
// 		}
// 		moveCompleted = false;

// 	}

// 	private void MovePiece(ChessPiece piece, int x, int y){
// 		if(piece==null){
// 			return;
// 		}
// 		int startX = (int) piece.GetBoardPosition().x;
// 		int startY = (int) piece.GetBoardPosition().y;
// 		int xDifference = x - startX;
// 		int yDifference = y - startY;
// 		Debug.Log($"Start in MovePiece is [{startX} {startY}]. Diff is [{xDifference} {yDifference}]. Current Pos is [{piece.transform.position}]");
// 		piece.transform.position = piece.transform.position + new Vector3(yDifference * 20f, xDifference * 20f, 0.0f);
// 		Debug.Log($"piece position = {piece.transform.position}");
// 		if(pieces[x, y]!=null){ //eat the pawn
// 			Debug.Log("EAT");
// 			RemovePiece(pieces[x, y].GetRed(), x, y);
// 			pieces[x, y].SetBoardPosition(-1, -1);
// 			pieces[x, y].transform.position = pieces[x, y].transform.position + new Vector3(0f, 0f, 300f);
// 		}
// 		pieces[startX, startY] = null;
// 		Debug.Log("Moving RED"+piece.GetRed());
// 		if(piece.GetRed()){
// 			for(int i=0; i<redPiecesPos.Count; i++){
// 				if(piece.GetBoardPosition() == redPiecesPos[i]){
// 					redPiecesPos[i] = new Vector2(x, y);
// 				}
// 			}
// 		}else{
// 			for(int i=0; i<bluePiecesPos.Count; i++){
// 				if(piece.GetBoardPosition() == bluePiecesPos[i]){
// 					bluePiecesPos[i] = new Vector2(x, y);
// 				}
// 			}
// 		}
// 		if(piece.Type == "general"){
// 			if(piece.GetRed()){
// 				generalRedPos = new Vector2(x, y);
// 			}else{
// 				generalBluePos = new Vector2(x, y);
// 			}
// 		}
// 		piece.SetBoardPosition(x, y);
// 		pieces[x, y] = piece;
// 	}

// 	private void RemovePiece(bool isRed, int x, int y){
// 		int removePos=-1;
// 		if(isRed){
// 			for(int i=0; i < redPiecesPos.Count; i++){
// 				if(redPiecesPos[i].x == x && redPiecesPos[i].y == y){
// 					removePos = i;
// 				}
// 			}
// 			redPiecesPos.RemoveAt(removePos);
// 		}else{
// 			for(int i=0; i < bluePiecesPos.Count; i++){
// 				if(bluePiecesPos[i].x == x && bluePiecesPos[i].y == y){
// 					removePos = i;
// 				}
// 			}
// 			bluePiecesPos.RemoveAt(removePos);
// 		}
// 	}

// 	private Vector2 GetBoardPosition(float x, float y){
// 		int yResult = (int) Math.Round((x+80.0)/20.0);
// 		int xResult = (int) Math.Round((y+90.0)/20.0);
// 		Vector2 boardPos = new Vector2(xResult, yResult);
// 		return boardPos;
// 	}

// 	private void GenerateBoard()
// 	{

// 		//Generate Blue Team
// 		GenerateChariot(0, 0, -80f, -90f, 9.0f, false);
// 		GenerateChariot(0, 8, 80f, -90f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(0, 0));
// 		bluePiecesPos.Add(new Vector2(0, 8));

// 		GenerateHorse(0, 1, -56.7f, -90f, 14.3f, false);
// 		GenerateHorse(0, 7, 61.56f, -90f, 14.3f, false);

// 		bluePiecesPos.Add(new Vector2(0, 1));
// 		bluePiecesPos.Add(new Vector2(0, 7));

// 		GenerateElephant(0, 2, -40f, -90f, 9.0f, false);
// 		GenerateElephant(0, 6, 40f, -90f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(0, 2));
// 		bluePiecesPos.Add(new Vector2(0, 6));

// 		GenerateAdvisor(0, 3, -20f, -90f, 9.0f, false);
// 		GenerateAdvisor(0, 5, 20f, -90f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(0, 3));
// 		bluePiecesPos.Add(new Vector2(0, 5));

// 		GenerateGeneral(0, 4, 0f, -90f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(0, 4));
// 		generalBluePos = new Vector2(0, 4);

// 		GenerateCannon(2, 1, -58.96f, -50f, 9.0f, false);
// 		GenerateCannon(2, 7, 59.14f, -50f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(2, 1));
// 		bluePiecesPos.Add(new Vector2(2, 7));

// 		GenerateSoldier(3, 0, -80f, -30f, 9.0f, false);
// 		GenerateSoldier(3, 2, -40f, -30f, 9.0f, false);
// 		GenerateSoldier(3, 4, 0f, -30f, 9.0f, false);
// 		GenerateSoldier(3, 6, 40f, -30f, 9.0f, false);
// 		GenerateSoldier(3, 8, 80f, -30f, 9.0f, false);

// 		bluePiecesPos.Add(new Vector2(3, 0));
// 		bluePiecesPos.Add(new Vector2(3, 2));
// 		bluePiecesPos.Add(new Vector2(3, 4));
// 		bluePiecesPos.Add(new Vector2(3, 6));
// 		bluePiecesPos.Add(new Vector2(3, 8));


// 		//Generate Red Team
// 		GenerateChariot(9, 0, -80f, 90f, 9.0f, true);
// 		GenerateChariot(9, 8, 80f, 90f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(9, 0));
// 		redPiecesPos.Add(new Vector2(9, 8));

// 		GenerateHorse(9, 1, -61.56f, 90f, 14.3f, true);
// 		GenerateHorse(9, 7, 56.7f, 90f, 14.3f, true);

// 		redPiecesPos.Add(new Vector2(9, 1));
// 		redPiecesPos.Add(new Vector2(9, 7));

// 		GenerateElephant(9, 2, -40f, 90f, 9.0f, true);
// 		GenerateElephant(9, 6, 40f, 90f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(9, 2));
// 		redPiecesPos.Add(new Vector2(9, 6));

// 		GenerateAdvisor(9, 3, -20f, 90f, 9.0f, true);
// 		GenerateAdvisor(9, 5, 20f, 90f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(9, 3));
// 		redPiecesPos.Add(new Vector2(9, 5));

// 		GenerateGeneral(9, 4, 0f, 90f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(9, 4));
// 		generalRedPos = new Vector2(9, 4);

// 		GenerateCannon(7, 1, -59.14f, 50f, 9.0f, true);
// 		GenerateCannon(7, 7, 58.96f, 50f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(7, 1));
// 		redPiecesPos.Add(new Vector2(7, 7));

// 		GenerateSoldier(6, 0, -80f, 30f, 9.0f, true);
// 		GenerateSoldier(6, 2, -40f, 30f, 9.0f, true);
// 		GenerateSoldier(6, 4, 0f, 30f, 9.0f, true);
// 		GenerateSoldier(6, 6, 40f, 30f, 9.0f, true);
// 		GenerateSoldier(6, 8, 80f, 30f, 9.0f, true);

// 		redPiecesPos.Add(new Vector2(6, 0));
// 		redPiecesPos.Add(new Vector2(6, 2));
// 		redPiecesPos.Add(new Vector2(6, 4));
// 		redPiecesPos.Add(new Vector2(6, 6));
// 		redPiecesPos.Add(new Vector2(6, 8));
// 	}

// 	private bool GeneralChecked(bool isRed){
// 		if(isRed){
// 			for(int i=0; i<bluePiecesPos.Count; i++){
// 				Vector2 pos = bluePiecesPos[i];
// 				if(isValidMove((int)pos.x, (int)pos.y, (int)generalRedPos.x, (int)generalRedPos.y, pieces[(int)pos.x, (int)pos.y].Type)){
// 					return true;
// 				}
// 			}
// 		}else{
// 			for(int i=0; i<redPiecesPos.Count; i++){
// 				Vector2 pos = redPiecesPos[i];
// 				if(isValidMove((int)pos.x, (int)pos.y, (int)generalBluePos.x, (int)generalBluePos.y, pieces[(int)pos.x, (int)pos.y].Type)){
// 					return true;
// 				}
// 			}
// 		}
// 		return false;
// 	}

// 	private bool isValidMove(int startX, int startY, int endX, int endY, string type){
// 		if(pieces[endX, endY]!=null){
// 			if(pieces[startX, startY].GetRed() ==pieces[endX, endY].GetRed()){
// 				return false;
// 			}
// 		}
// 		ArrayList possibleMoves = new ArrayList();
// 		bool isRed = pieces[startX, startY].GetRed();
// 		if(type=="chariot"){
// 			if(startX!=endX && startY!=endY)return false;
// 			if(startX==endX && startY==endY)return false;
// 			int start;
// 			int end;
// 			if(startY!=endY){
// 				if(endY>startY){
// 					start = startY;
// 					end = endY;
// 				}else{
// 					start = endY;
// 					end = startY;
// 				}
// 				for(int i=start+1; i<end; i++){
// 					if(pieces[startX, i] != null)return false;
// 				}
// 				return true;
// 			}else if(startX!=endX){
// 				if(endX>startX){
// 					start = startX;
// 					end = endX;
// 				}else{
// 					start = endX;
// 					end = startX;
// 				}
// 				for(int i=start+1; i<end; i++){
// 					if(pieces[i, startY] != null)return false;
// 				}
// 				return true;
// 			}			
// 		}else if(type=="horse"){
// 			if(isInBounds(startX+1, startY)){
// 				if(pieces[startX+1, startY]==null){
// 					possibleMoves.Add(new Vector2(startX+2, startY-1));
// 					possibleMoves.Add(new Vector2(startX+2, startY+1));
// 				}
// 			}
// 			if(isInBounds(startX-1, startY)){
// 				if(pieces[startX-1, startY]==null){
// 					possibleMoves.Add(new Vector2(startX-2, startY-1));
// 					possibleMoves.Add(new Vector2(startX-2, startY+1));
// 				}
// 			}
// 			if(isInBounds(startX, startY-1)){
// 				if(pieces[startX, startY-1]==null){
// 					possibleMoves.Add(new Vector2(startX+1, startY-2));
// 					possibleMoves.Add(new Vector2(startX-1, startY-2));
// 				}
// 			}
// 			if(isInBounds(startX, startY+1)){
// 				if(pieces[startX, startY+1]==null){
// 					possibleMoves.Add(new Vector2(startX+1, startY+2));
// 					possibleMoves.Add(new Vector2(startX-1, startY+2));
// 				}
// 			}
// 		}else if(type=="elephant"){
// 			if(isInBounds(startX+1, startY+1)){
// 				if(pieces[startX+1, startY+1] == null){
// 					if(!isRed){
// 						if(startX+2 <= 4){
// 							possibleMoves.Add(new Vector2(startX+2, startY+2));
// 						}
// 					}else{
// 						possibleMoves.Add(new Vector2(startX+2, startY+2));
// 					}
// 				}
// 			}
// 			if(isInBounds(startX+1, startY-1)){
// 				if(pieces[startX+1, startY-1] == null){
// 					if(!isRed){
// 						if(startX+2 <= 4){
// 							possibleMoves.Add(new Vector2(startX+2, startY-2));
// 						}
// 					}else{
// 						possibleMoves.Add(new Vector2(startX+2, startY-2));
// 					}
// 				}
// 			}
// 			if(isInBounds(startX-1, startY+1)){
// 				if(pieces[startX-1, startY+1]==null){
// 					if(isRed){
// 						if(startX-2 >= 5){
// 							possibleMoves.Add(new Vector2(startX-2, startY+2));
// 						}
// 					}else{
// 						possibleMoves.Add(new Vector2(startX-2, startY+2));
// 					}
// 				}
// 			}
// 			if(isInBounds(startX-1, startY-1)){
// 				if(pieces[startX-1, startY-1]==null){
// 					if(isRed){
// 						if(startX-2 >= 5){
// 							possibleMoves.Add(new Vector2(startX-2, startY-2));
// 						}
// 					}else{
// 						possibleMoves.Add(new Vector2(startX-2, startY-2));
// 					}
// 				}
// 			}
// 		}else if(type=="advisor"){
// 			ArrayList advisorBox = new ArrayList();
// 			if(isRed){
// 				advisorBox.Add(new Vector2(7, 3));
// 				advisorBox.Add(new Vector2(7, 5));
// 				advisorBox.Add(new Vector2(8, 4));
// 				advisorBox.Add(new Vector2(9, 3));
// 				advisorBox.Add(new Vector2(9, 5));
// 			}else{
// 				advisorBox.Add(new Vector2(0, 3));
// 				advisorBox.Add(new Vector2(0, 5));
// 				advisorBox.Add(new Vector2(1, 4));
// 				advisorBox.Add(new Vector2(2, 3));
// 				advisorBox.Add(new Vector2(2, 5));
// 			}
// 			possibleMoves.Add(new Vector2(startX+1, startY+1));
// 			possibleMoves.Add(new Vector2(startX+1, startY-1));
// 			possibleMoves.Add(new Vector2(startX-1, startY+1));
// 			possibleMoves.Add(new Vector2(startX-1, startY-1));
// 			foreach (Vector2 pos in possibleMoves){
// 				if(pos.x == endX && pos.y == endY && advisorBox.Contains(pos)){
// 					if(isInBounds(endX, endY)){
// 						return true;
// 					}
// 				}
// 			}
// 			return false;
// 		}else if(type=="general"){
// 			ArrayList generalBox = new ArrayList();
// 			if(isRed){
// 				for(int i=7; i<10; i++){
// 					for(int j=3; j<6; j++){
// 						generalBox.Add(new Vector2(i, j));
// 					}
// 				}
// 			}else{
// 				for(int i=0; i<3; i++){
// 					for(int j=3; j<6; j++){
// 						generalBox.Add(new Vector2(i, j));
// 					}
// 				}
// 			}
// 			possibleMoves.Add(new Vector2(startX+1, startY));
// 			possibleMoves.Add(new Vector2(startX-1, startY));
// 			possibleMoves.Add(new Vector2(startX, startY+1));
// 			possibleMoves.Add(new Vector2(startX, startY-1));
// 			foreach (Vector2 pos in possibleMoves){
// 				if(pos.x == endX && pos.y == endY && generalBox.Contains(pos)){
// 					if(isInBounds(endX, endY)){
// 						return true;
// 					}
// 				}
// 			}
// 			return false;
// 		}else if(type=="cannon"){
// 			if(startX!=endX && startY!=endY){return false;} //cannot move diagonally
// 			if(startX==endX && startY==endY){return false;} //cannot move to the same current place.
// 			int start;
// 			int end;
// 			if(pieces[endX, endY]!=null){
// 				if(pieces[endX, endY].GetRed() == pieces[startX, startY].GetRed()){return false;}
// 				else{
// 					int countBetween = 0;
// 					if(startX!=endX){
// 						if(startX<endX){start=startX; end=endX;}else{start=endX; end=startX;}
// 						for(int i=start+1; i<end; i++){
// 							if(pieces[i, startY] != null)countBetween++;
// 						}
// 					}else if(startY!=endY){
// 						if(startY<endY){start=startY; end=endY;}else{start=endY; end=startY;}
// 						for(int i=start+1; i<end; i++){
// 							if(pieces[startX, i] != null)countBetween++;
// 						}
// 					}
// 					return countBetween==1;
// 				}
// 			}else{
// 				if(startX!=endX){
// 					if(startX<endX){start=startX; end=endX;}else{start=endX; end=startX;}
// 					for(int i=start+1; i<end; i++){
// 						if(pieces[i, startY] != null)return false;
// 					}
// 				}else if(startY!=endY){
// 					if(startY<endY){start=startY; end=endY;}else{start=endY; end=startY;}
// 					for(int i=start+1; i<end; i++){
// 						if(pieces[startX, i] != null)return false;
// 					}
// 				}
// 				return true;
// 			}
// 		}else if(type=="soldier"){
// 			bool crossedRiver = false;
// 			if(isRed){
// 				possibleMoves.Add(new Vector2(startX-1, startY));
// 				if(startX<=4){crossedRiver=true;}
// 			}else{
// 				possibleMoves.Add(new Vector2(startX+1, startY));
// 				if(startX>=5){crossedRiver=true;}
// 			}
// 			if(crossedRiver){
// 				possibleMoves.Add(new Vector2(startX, startY-1));
// 				possibleMoves.Add(new Vector2(startX, startY+1));
// 			}
// 		}

// 		ChessPiece this_piece = pieces[startX, startY];
// 		// ALSO show possible moves TODO
// 		foreach (Vector2 pos in possibleMoves){
// 			if(pos.x == endX && pos.y == endY){
// 				if(isInBounds(endX, endY)){
// 					return true;
// 				}
// 			}
			
// 			GameObject ind = Instantiate(indicator);
// 			ind.transform.position = this_piece.transform.position + new Vector3((pos.y - startY) * 20f, (pos.x - startX) * 20f, 0.0f);
// 			indicators.Add(ind);
// 			// piece.transform.position = piece.transform.position + new Vector3(yDifference * 20f, xDifference * 20f, 0.0f);
// 		}
// 		return false;
// 	}

// 	private bool isInBounds(int x, int y){
// 		if(x >= 0 && x < 10 && y >= 0 && y < 9){
// 			return true;
// 		}else{
// 			return false;
// 		}
// 	}
	
// 	private void GenerateChariot(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(chariotRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "chariot";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(chariotBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "chariot";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateHorse(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(horseRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "horse";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(horseBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "horse";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateElephant(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(elephantRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "elephant";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(elephantBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "elephant";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateAdvisor(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(advisorRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "advisor";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(advisorBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "advisor";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateGeneral(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(generalRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "general";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(generalBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "general";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateCannon(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(cannonRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "cannon";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(cannonBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "cannon";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}
// 	private void GenerateSoldier(int x, int y, float px, float py, float pz, bool red)
// 	{
// 		GameObject go;
// 		if(red){
// 			go = Instantiate(soldierRed) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "soldier";
// 			go.GetComponent<ChessPiece>().SetRed(true);
// 		}else{
// 			go = Instantiate(soldierBlue) as GameObject;
// 			go.AddComponent<ChessPiece>();
// 			go.GetComponent<ChessPiece>().Type = "soldier";
// 			go.GetComponent<ChessPiece>().SetRed(false);
// 		}
// 		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
// 		go.transform.position = new Vector3(px, py, pz);
// 		ChessPiece p = go.GetComponent<ChessPiece>();
// 		pieces[x, y] = p;
// 	}

// }