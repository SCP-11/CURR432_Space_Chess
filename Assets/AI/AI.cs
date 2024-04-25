using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.MLAgents;
using UnityEngine;
// using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.ShaderGraph;
using System;
using Unity.VisualScripting;
public class AI : Agent
{
    public Board board;
    public bool isRed;

    private int preRedCount = 16;
    private int preBlueCount = 16;
    private bool preRedturn = false;
    public bool heuristic = false;
    // Start is called before the first frame update
    void Start()
    {
        preRedCount = board.redPiecesPos.Count;
        preBlueCount = board.bluePiecesPos.Count;
        preRedturn = board.isRedTurn;
    }

    // Update is called once per frame
    void Update()
    {
        // if(board.isRedTurn == isRed){
        //     this.RequestDecision();
        // }else{

        // }
        
        // Check board.redPiecesPos.count()
        GameUpdate();
        Count_Update();
        
        // Check board.bluePiecesPos.count()
                

    }
    private void GameUpdate(){
        if(board.winner != ""){
            if(board.winner == "red"){
                if(isRed){
                    // AddReward(4.0f);
                }else{
                    // AddReward(-4.0f);
                }
            }else if(board.winner == "blue"){
                if(isRed){
                    // AddReward(-4.0f);
                }else{
                    // AddReward(4.0f);
                }
            }
            EndEpisode();
            board.Restart();
        }else if(board.isRedTurn != preRedturn && preRedturn == isRed){
            EndEpisode();
        }
        if(board.isRedTurn == isRed){
            if(heuristic){
                if(Input.GetMouseButtonDown(0)){
                    this.RequestDecision();
                }
            }else{
                this.RequestDecision();
            }
            // Academy.Instance.EnvironmentStep();
        }
        preRedturn = board.isRedTurn;
    }
    private void Count_Update(){
        int self_count = isRed? board.redPiecesPos.Count : board.bluePiecesPos.Count;
        int oppo_count = isRed? board.bluePiecesPos.Count : board.redPiecesPos.Count;
        int pre_self_count = isRed? preRedCount : preBlueCount;
        int pre_oppo_count = isRed? preBlueCount : preRedCount;

        if(board.bluePiecesPos.Count < 4 || board.redPiecesPos.Count < 4){
            // AddReward(0.0f);
            EndEpisode();
            board.Restart();
        }
        if(self_count < pre_self_count){
            // AddReward(-4.0f);
            // EndEpisode();
        }
        if(oppo_count < pre_oppo_count){
            // AddReward(4.0f);
            // EndEpisode();
        }
        
        preRedCount = board.redPiecesPos.Count;
        preBlueCount = board.bluePiecesPos.Count;
    }
    private int TypeToInt(string type){
        switch (type){
            case "chariot":
                return 1;
            case "cannon":
                return 2;
            case "horse":
                return 3;
            case "elephant":
                return 4;
            case "advisor":
                return 5;
            case "general": 
                return 6;
            case "soldier":
                return 7;
            default:
                return 0;
        }
    }
    IEnumerator WaitHalfSecond()
    {
        Debug.Log("Waiting for half a second...");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Done waiting for half a second");
    }

    
    public override void CollectObservations(VectorSensor sensor)
    {
        if(board.isRedTurn == isRed){
            ChessPiece[,] pieces = board.pieces;
            int[,] processedBoard = new int[10, 9];

            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 9; j++){
                    if (pieces[i, j] != null){
                        int ind = TypeToInt(pieces[i, j].Type);
                        processedBoard[i, j] = pieces[i, j].GetRed()==isRed ? ind : -ind;
                    }else{
                        processedBoard[i, j] = 0;
                    }
                }
            }
            // Print2DArray("Processed board: ", processedBoard);
            int[,] processedBoard2 = processedBoard.Clone() as int[,];
            if(!isRed){
                for (int i = 0; i < 10; i++){
                    for (int j = 0; j < 9; j++){
                        processedBoard2[i, j] = processedBoard[9-i, 8-j];
                    }
                }
            }
            
            // Debug.Log($"Reversed board: {processedBoard2} ");
            // Print2DArray("Reversed board: ", processedBoard2);
            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 9; j++){
                    sensor.AddObservation(processedBoard2[i, j]);
                }
            }
            
            int[] redPiecesFrontLines = board.redPiecesFrontLines;
            int[] bluePiecesFrontLines = board.bluePiecesFrontLines;      
            int[] redPiecesFrontLines2 = board.redPiecesFrontLines.Clone() as int[];
            int[] bluePiecesFrontLines2 = board.bluePiecesFrontLines.Clone() as int[];      
            // Debug.Log(
            //     $"Pre redPiecesFrontLines: {redPiecesFrontLines} Pre bluePiecesFrontLines: {bluePiecesFrontLines} "
            //     );
            // Print1DArray("Pre redPiecesFrontLines: ", redPiecesFrontLines);
            // Print1DArray("Pre bluePiecesFrontLines: ", bluePiecesFrontLines);
            for (int i = 0; i < 9; i++){
                if(isRed){
                    sensor.AddObservation(redPiecesFrontLines[i]);
                    sensor.AddObservation(bluePiecesFrontLines[i]);
                }else{
                    redPiecesFrontLines2[i] = 9 - redPiecesFrontLines[8-i];
                    bluePiecesFrontLines2[i] = 9 - bluePiecesFrontLines[8-i];
                    sensor.AddObservation(9 - bluePiecesFrontLines[8-i]);
                    sensor.AddObservation(9 - redPiecesFrontLines[8-i]);
                }
            }
            // Debug.Log(
            //     $"Post redPiecesFrontLines: {redPiecesFrontLines2} Post bluePiecesFrontLines: {bluePiecesFrontLines2} "
            //     );
            // Print1DArray("Post redPiecesFrontLines: ", redPiecesFrontLines2);
            // Print1DArray("Post bluePiecesFrontLines: ", bluePiecesFrontLines2);

            int[,] debris = board.spaceDebrisCount;
            
            // Debug.Log($"Pre debris: {debris} ");
            // Print2DArray("Pre debris: ", debris);
            int[,] debris2 = debris.Clone() as int[,];
            if(isRed){
                for (int i = 0; i < 10; i++){
                    for (int j = 0; j < 9; j++){
                        sensor.AddObservation(debris[i, j]);
                    }
                }
            }else{
                for (int i = 9; i >= 0; i--){
                    for (int j = 8; j >= 0; j--){
                        debris2[i,j] = debris[9-i, 8-j];
                        sensor.AddObservation(debris[9-i, 8-j]);
                    }
                }
            }
            
            sensor.AddObservation(board.selectedPiece == null? 0 : 1);
            // Debug.Log($"Post debris: {debris2} ");
            // Print2DArray("Post debris: ", debris2);
            // base.CollectObservations(sensor);
        }
    }
    private void Print2DArray(String start, int[,] array){
        String debug = $"{start}\n";
        for (int i = 0; i < 10; i++){
            for (int j = 0; j < 9; j++){
                debug += $"{array[i, j]}, ";
            }
            debug += "\n";
        }
        Debug.Log(debug);
    }
    private void Print1DArray(String start, int[] array){
        String debug = $"{start}\n";
        for (int i = 0; i < 9; i++){
            debug += $"{array[i]}, ";
        }
        Debug.Log(debug);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
            actionsOut.DiscreteActions.Array[0] = isRed? (int)board.boardPosition.x : 9 - (int)board.boardPosition.x;
            actionsOut.DiscreteActions.Array[1] = isRed? (int)board.boardPosition.y : 8 - (int)board.boardPosition.y;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if(!isRed){
            Debug.Log("OnActionReceived" + actions.DiscreteActions[0] + actions.DiscreteActions[1]);
        }
        // WaitHalfSecond();
        if(board.isRedTurn == isRed){
            // Debug.Log("OnActionReceived" + actions.DiscreteActions[0]
            // + actions.DiscreteActions[1]
            // + actions.DiscreteActions[2]
            // + actions.DiscreteActions[3]
            // );
            // AddReward(0.0f);
            if(board.selectedPiece == null){
                int startX = isRed? actions.DiscreteActions[0] : 9 - actions.DiscreteActions[0];
                int startY = isRed? actions.DiscreteActions[1] : 8 - actions.DiscreteActions[1];
                board.SelectPiece(startX, startY);
                switch(board.SelectPiece(startX, startY)){
                    case 0:
                        AddReward(-1.0f);
                        break;
                    case 1:
                        AddReward(8.0f);
                        break;
                    case 2:
                        AddReward(0.0f);
                        break;
                    default:
                        AddReward(-1.0f);
                        break;
                }
            }
            
            // if(board.SelectPiece(startX, startY)){
            //     AddReward(8.0f);
            //     // EndEpisode();
            // }else{
            //     AddReward(-1.0f);
            // }

            
            // Wait half a second

            else{
                int endX = isRed? actions.DiscreteActions[0] : 9 - actions.DiscreteActions[0];
                int endY = isRed? actions.DiscreteActions[1] : 8 - actions.DiscreteActions[1];  

                switch(board.SelectPiece(endX, endY)){
                    case 0:
                        AddReward(-1.0f);
                        break;
                    case 1:
                        AddReward(0.0f);
                        break;
                    case 2:
                        AddReward(6.0f);
                        break;
                    default:
                        AddReward(-1.0f);
                        break;
                }

            }
            // if(board.SelectPiece(endX, endY)){
            //     // AddReward(6.0f);
            //     // Debug.Log(board.redPiecesPos.Count);
            //     // Debug.Log(board.bluePiecesPos.Count);
            //     EndEpisode();
            // }else{
            //     // AddReward(-1.0f);
            // }
            // take_action(actions.DiscreteActions[0]);
            // update_graphic();
        }
    }

    public Vector2 ReverseBoard(int x, int y){
        return new Vector2(9 - x, 8 - y);
    }

}
