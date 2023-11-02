using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerNotesGenerator : MonoBehaviour
{

    [SerializeField] int startLane = 0; //ロングノーツの始点がどこのレーンにあるか
    [SerializeField] int endLane = 2; //ロングノーツの終点がどこのレーンにあるか
    [SerializeField] float startTim = 1.0f; //ロングノーツの始点が来るタイミング
    [SerializeField] float endTim = 6.0f; //ロングノーツの終点が来るタイミング

    float laneWidth = 2.0f;　//レーンの太さ( = ノーツの太さ )

    void Start()
    {
        //テスト用
        GameObject testNotes = new GameObject();
        testNotes.AddComponent<MeshFilter>();
        testNotes.AddComponent<MeshRenderer>();

        //位置座標を求める。
        Vector3 startPos = new Vector3(-2 * laneWidth + laneWidth * startLane + laneWidth / 2, 0.1f, startTim);
        Vector3 endPos = new Vector3(-2 * laneWidth + laneWidth * endLane + laneWidth / 2, 0.1f, endTim);

        //ロングノーツ生成
        Generate(startPos, endPos, testNotes);
    }



    void Generate(Vector3 sPos, Vector3 ePos, GameObject notesObj)
    {
        //メッシュを生成し、MeshFilterに渡す。
        //MeshFilterがMeshRendererにメッシュを渡して、メッシュが描画される。
        Mesh mesh = new Mesh();
        notesObj.GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        vertices[0] = sPos + new Vector3(-laneWidth / 2, 0, 0);//始点の左端
        vertices[1] = sPos + new Vector3(laneWidth / 2, 0, 0); //始点の右端
        vertices[2] = ePos + new Vector3(-laneWidth / 2, 0, 0); //終点の左端
        vertices[3] = ePos + new Vector3(laneWidth / 2, 0, 0); //終点の右端

        triangles = new int[6] { 0, 2, 1, 3, 1, 2 };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}