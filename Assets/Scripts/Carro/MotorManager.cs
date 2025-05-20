using Logitech;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MotorManager : MonoBehaviour
{
    [SerializeField] private float modificadorFreio = 5f;
    [SerializeField] private float modificadorAceleradorMarcha1 = 1f;

    private MarchaEnum marchaAtual; 
    private float velocidadeAtual = 0;
    private float rotacaoMotor = 0f;
    private float torque = 200f;


    System.Random random;

    private void Start()
    {
        random = new System.Random();
        marchaAtual = MarchaEnum.PRIMEIRA;
    }

    // Update is called once per frame
    void Update()
    {
        //ControlarTrocaDeMarcha(rec);
        //ControlarVelocidadeVeiculo(rec);
    }



    private void ControlarTrocaDeMarcha(LogitechGSDK.DIJOYSTATE2ENGINES rec)
    {
        //if (ValidarBugInicio(rec)) return;

        CalcularRotacaoMotor();
    }

    void ControlarVelocidadeVeiculo(LogitechGSDK.DIJOYSTATE2ENGINES rec)
    {
        //if (ValidarBugInicio(rec)) return;
        /*
        if (freio > 0)
        {
            velocidadeAtual -= freio * 5f * Time.deltaTime;
        }
        if (acelerador > 0)
        {
            for (int i = 0; 1 < pneus.Length; i++)
            {
                pneus[i].motorTorque = marchaAtual.Torque;
            }
        }
        */
    }


    void CalcularRotacaoMotor()
    {
        /*
        if (acelerador > 0)
        {
            rotacaoMotor += 135 * acelerador * Time.deltaTime;
        }
        else
        {
            rotacaoMotor -= 170 * Time.deltaTime;
            rotacaoMotor = Math.Max(rotacaoMotor, random.Next(800, 900));
        }
        */
    }
}
