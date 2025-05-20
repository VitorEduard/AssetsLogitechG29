
using UnityEngine;

public class RodasManager : MonoBehaviour
{
    [SerializeField] private WheelCollider[] pneus = new WheelCollider[4];
    [SerializeField] private GameObject[] meshPneus = new GameObject[4];
    [SerializeField] private TracaoEnum tracao = TracaoEnum.DIANTEIRA;
    [SerializeField] private int poderDeFreio = 900000;
    private int _ANGULO_MAX = 45;
    private int raio = 6;

    public void Mover(float aceleradorNormalizado, MarchaEnum marchaAtual)
    {
        if (aceleradorNormalizado > 0)
        {
            float torquePorRoda = marchaAtual.Torque / tracao.qtdRodas;

            for (int i = tracao.inicioRodas; i < tracao.fimRodas; i++)
            {
                pneus[i].motorTorque = aceleradorNormalizado * torquePorRoda * 5000 * Time.deltaTime;
            }
        }
    }

    public void Freiar(float freioNormalizado)
    {
        pneus[2].motorTorque = freioNormalizado * poderDeFreio * Time.deltaTime;
        pneus[3].motorTorque = freioNormalizado * poderDeFreio * Time.deltaTime;
    }

    public void GirarRodas(float anguloAbsoluto)
    {

        // acerman steering formula
        Debug.Log(anguloAbsoluto);
        if (anguloAbsoluto > 0)
        {
            pneus[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (raio + 0.75f)) * anguloAbsoluto;
            pneus[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (raio - 0.75f)) * anguloAbsoluto;
        }
        else if (anguloAbsoluto < 0)
        {
            pneus[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (raio - 0.75f)) * anguloAbsoluto;
            pneus[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (raio + 0.75f)) * anguloAbsoluto;
        }
        else
        {
            pneus[0].steerAngle = 0;
            pneus[1].steerAngle = 0;
        }


        AnimarPneus();
    }

    void AnimarPneus()
    {
        Vector3 posicaoPneus = Vector3.zero;
        Quaternion rotacaoPneus = Quaternion.identity;

        // Corrige a rotação com base na inclinação do seu modelo
        Quaternion correcao = Quaternion.Euler(new Vector3(0f, 0f, 90f));

        for (int i = 0; i < pneus.Length; i++)
        {
            pneus[i].GetWorldPose(out posicaoPneus, out rotacaoPneus);
            meshPneus[i].transform.position = posicaoPneus;
            meshPneus[i].transform.rotation = rotacaoPneus * correcao;
        }
    }

}