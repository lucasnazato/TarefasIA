using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    // Criar array para receber os waypoints na cena e criar variavel para o inddex
    public GameObject[] waypoints;
    int currentWP = 0;

    // Definir valores das variaveis que definem o movimento
    public float speed = 1.0f;
    public float accuracy = 1.0f;
    public float rotSpeed = 0.4f;

    private void Start()
    {
        // Pegar todos os waypoints com base na tag do objeto
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    private void LateUpdate()
    {
        // Se nenhum waypoint estiver na cena retornar
        if (waypoints.Length == 0) return;

        // Pegar a direção que o personagem deve olhar baseado na posição do waypoint
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);

        // Calcular direção do personagem para o waypoint
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        // Se a distância do personagem para o alvo for menor que a distância definida mudar para o próximo alvo
        if (direction.magnitude < accuracy)
        {
            // Incrementar variavel usada como index do array de Waypoints
            currentWP++;

            // Se o index atual for maior que o array...
            if (currentWP >= waypoints.Length)
            {
                // ... voltar para o primeiro waypoint do array
                currentWP = 0;
            }
        }

        // Mover personagem na direção do alvo
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
