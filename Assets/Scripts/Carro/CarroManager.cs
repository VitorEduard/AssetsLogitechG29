using Logitech;
using Unity.VisualScripting;
using UnityEngine;

public class CarroManager : MonoBehaviour
{

    private LogitechGSDK.DIJOYSTATE2ENGINES inputsLogi;
    private int _INDEX_LOGI = 0;
    private float _MAXIMO_INPUT_VOLANTE = 32768f;
    private int _DOWN_FORCE = 50;

    [SerializeField] private VolanteManager volante;
    [SerializeField] private RodasManager rodas;
    [SerializeField] private MotorManager motor;
    private Rigidbody carroRigidBody;
    private GameObject centroDeMassa;


    // Variveis do Carro
    private float embreagem = 0;
    private float freio = 0;
    private float acelerador = 0;
    private float rotacaoVolanteAbsoluta = 0;
    private float rotacaoVolanteAbsolutaNormalizado = 0;
    private float KPH;

    // Variavies para quando não usa o logitech
    private float rotacaoVolanteAbsolutaSimulada = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));
        carroRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InicializarVariaveis();

        addDownForce();

        volante.RotacionarVolante(rotacaoVolanteAbsoluta);
        rodas.GirarRodas(rotacaoVolanteAbsolutaNormalizado);
        rodas.Mover(acelerador, MarchaEnum.PRIMEIRA);
        rodas.Freiar(freio);
    }

    void InicializarVariaveis()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(_INDEX_LOGI))
        {
            LogitechGSDK.LogiPlaySpringForce(_INDEX_LOGI, 0, 20, 10);
            inputsLogi = LogitechGSDK.LogiGetStateUnity(_INDEX_LOGI);

            embreagem = 0;
            freio = 0;
            acelerador = 0;
            if (!ValidarBugInicio(inputsLogi))
            {
                embreagem = NormalizarDadoPedal(inputsLogi.rglSlider[0]);
                freio = NormalizarDadoPedal(inputsLogi.lRz);
                acelerador = NormalizarDadoPedal(inputsLogi.lY);
            }

            rotacaoVolanteAbsoluta = inputsLogi.lX;
            rotacaoVolanteAbsolutaNormalizado = rotacaoVolanteAbsoluta / _MAXIMO_INPUT_VOLANTE;
        }
        else
        {
            embreagem = 0;
            freio = Input.GetKey(KeyCode.S) ? 1f : 0f;
            acelerador = Input.GetKey(KeyCode.W) ? 1f : 0f;
            if (Input.GetKey(KeyCode.A)) rotacaoVolanteAbsolutaNormalizado = -1;
            else if (Input.GetKey(KeyCode.D)) rotacaoVolanteAbsolutaNormalizado = 1;
            else rotacaoVolanteAbsolutaNormalizado = 0;

            rotacaoVolanteAbsoluta += (rotacaoVolanteAbsolutaNormalizado * 6000);
        }

        KPH = carroRigidBody.linearVelocity.magnitude * 3.6f;
        centroDeMassa = GameObject.Find("Massa");
        carroRigidBody.centerOfMass = centroDeMassa.transform.localPosition;
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    float NormalizarDadoPedal(float dadoBruto)
    {
        return 1 - (dadoBruto + 32768) / 65535;
    }

    // Quando o jogo inicia antes de apertar em qualquer pedal o input deles é marcado como todos em 0 ao mesmo tempo
    bool ValidarBugInicio(LogitechGSDK.DIJOYSTATE2ENGINES rec)
    {
        return rec.rglSlider[0] == 0f && rec.lRz == 0f && rec.lY == 0f;
    }

    void addDownForce()
    {
        carroRigidBody.AddForce(-transform.up * _DOWN_FORCE * carroRigidBody.linearVelocity.magnitude);
    }

}
