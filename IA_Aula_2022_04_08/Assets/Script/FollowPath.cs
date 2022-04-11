using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    public float speed = 5.0f;
    public float accuracy = 0.05f;
    public float rotSpeed = 0.2f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        // Peagr o waypoint manager e o Graph e colocar o node atual como o primeiro waypoint do array
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    // Fun��o que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Heli)
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    // Fun��o que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Ruins)
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[7]);
        currentWP = 0;
    }

    // Fun��o que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Factory)
    public void GoToFactory()
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        // Se a dist�ncia do caminho for 0 OU o waypoint atual for igual ao �ltimo ponto do caminho retornar e n�o executar nenhum c�digo
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) return;

        //O n� que estar� mais pr�ximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais pr�ximo bastante do n� o tanque se mover� para o pr�ximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }

        // Se o waypoint atual for menor que a dist�ncia do caminho executar o c�digo de movimenta��o
        if (currentWP < g.getPathLength())
        {
            // Definir o objetivo como a transform do pr�ximo ponto
            goal = g.getPathPoint(currentWP).transform;

            // Calcular vetor que indica a posi��o do objetivo
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

            // Calcular a dire��o que o tanque deve seguir para chegar ao objetivo
            Vector3 direction = lookAtGoal - this.transform.position;

            //  Rotacionar e mover o tanque na dire��o do objetivo
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }
}
