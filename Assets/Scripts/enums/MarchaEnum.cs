using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class MarchaEnum {
    public static readonly MarchaEnum RE = new MarchaEnum("R�", 380, -3.2f);
    public static readonly MarchaEnum NEUTRO = new MarchaEnum("Neutro", 0, 0.0f);
    public static readonly MarchaEnum PRIMEIRA = new MarchaEnum("1�", 300, 4f);
    public static readonly MarchaEnum SEGUNDA = new MarchaEnum("2�", 240, 2.2f);
    public static readonly MarchaEnum TERCEIRA = new MarchaEnum("3�", 180, 1.4f);
    public static readonly MarchaEnum QUARTA = new MarchaEnum("4�", 140, 1.0f);
    public static readonly MarchaEnum QUINTA = new MarchaEnum("5�", 110, 0.8f);
    public static readonly MarchaEnum SEXTA = new MarchaEnum("6�", 90, 0.7f);

    public string Nome { get; }
    public int Torque { get; }
    public float Relacao { get; }

    private MarchaEnum(string nome, int torque, float relacao)
    {
        Nome = nome;
        Torque = torque;
        Relacao = relacao;
    }

    public override string ToString()
    {
        return $"{Nome} (Torque: {Torque} Nm)";
    }


}