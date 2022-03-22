using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    // Array de pontos que o personangem precisa percorrer
    public GameObject[] points;

    // Força do movimento
    public float Force = 1500;

    // Distância mínima que personagem precisa estar do alvo
    public float minDistance = 0.1f;

    // Define se personagem faz um loop nos pontos ou para no último
    public bool loopPoints = false;

    // Index do ponto atual do array
    int alvoIndex = 0;

    Rigidbody rb;

    void Start()
    {
        //Pegar Rigidbody do personagem para adicionar força
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Pegar distância ente o alvo e o personagem. Zera eixo Y para que movimento ocorra apenas no plano XZ (Ignorar altura)
        Vector3 dir = (points[alvoIndex].transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);

        // Verificar se o personagem já atingiu uma distância aceitável do alvo, se não, adicionar força na direção dele
        if (dir.magnitude > minDistance)
        {
            rb.AddForce(dir.normalized * Force * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar se colisão foi com o alvo, se sim...
        if (other.gameObject == points[alvoIndex])
        {
            // Mudar a sua cor para cor do personagem
            points[alvoIndex].GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

            // Verificar se index não está fora do array, se estiver dentro, mudar para o próximo alvo
            if (alvoIndex < points.Length - 1)
            {
                alvoIndex++;
            }
            // Se estiver fora e loop estiver como verdadeiro, resetar para o primeiro ponto
            else if (loopPoints)
            {
                alvoIndex = 0;
            }
        }
    }
}
