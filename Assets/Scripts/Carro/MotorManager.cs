using Logitech;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MotorManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve eficienciaMotor;
    [SerializeField] private AnimationCurve curvaTorque;
    [SerializeField] private AnimationCurve curvaFreioMotor;

    private MarchaEnum marchaAtual;
    private float rotacaoLivre = 900f;
    private float rpmMotor = 800f;
    private float diferencial = 4.1f;
    private float velocidadeSuavizada = 0.0f;
    private float freioMotor = 2_000f;


    public float CalcularFreioMotor()
    {
        return curvaFreioMotor.Evaluate(rpmMotor) * freioMotor;
    }

    public float RelacaoMarcha()
    {
        return marchaAtual.Relacao * diferencial;
    }

    public float RpmAlvoMotorLivre(float pedalAceleracao)
    {
        return Mathf.Max(rotacaoLivre, rpmMotor + (pedalAceleracao * 400_000 * Time.deltaTime));
    }

    public bool EhMarchaNeutra()
    {
        return MarchaEnum.NEUTRO.Equals(marchaAtual);
    }

    public float CalcularPotenciaMotor(float pedalAceleracao)
    {
        float eficienciaMotor = curvaTorque.Evaluate(rpmMotor);
        // Calcula o torque usando a curva de eficiência (Injeção Eletrônica)
        float aceleracaoAplicada = Mathf.Min(eficienciaMotor, pedalAceleracao);
        return aceleracaoAplicada * marchaAtual.Torque * 600;
    }

    public float CalcularRpmMotor(float rpmMotorAlvo, float impactoEmbreagem)
    {
        rpmMotor = Mathf.SmoothDamp(rpmMotor, rpmMotorAlvo, ref velocidadeSuavizada, impactoEmbreagem);
        return rpmMotor;
    }

    public float RpmMotor() { return rpmMotor; }

    public void ValidarSeRpmMuitoBaixo(float rpmMotorAlvo, float pedalEmbreagem)
    {
        if (rpmMotorAlvo < (rotacaoLivre * 0.2) && EmbreagemPressionada(pedalEmbreagem))
        {
            // MATAR CARRO!!!
        }
    }

    public void ValidarVariacaoRpmMuitoAlta(float rpmMotorAlvo, float pedalEmbreagem)
    {
        if (Mathf.Abs(rpmMotorAlvo - rpmMotor) > 3000 && EmbreagemPressionada(pedalEmbreagem))
        {
            // MATAR CARRO!!!
        }
    }

    private void Start()
    {
        marchaAtual = MarchaEnum.PRIMEIRA;
    }


    private bool EmbreagemPressionada(float pedalEmbreagem)
    {
        return pedalEmbreagem < 0.8f;
    }

}
