using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class MarchaEnum {
    public static readonly MarchaEnum RE = new MarchaEnum("Ré", -100);
    public static readonly MarchaEnum NEUTRO = new MarchaEnum("Neutro", 0);
    public static readonly MarchaEnum PRIMEIRA = new MarchaEnum("1ª", 10250);
    public static readonly MarchaEnum SEGUNDA = new MarchaEnum("2ª", 300);
    public static readonly MarchaEnum TERCEIRA = new MarchaEnum("3ª", 280);
    public static readonly MarchaEnum QUARTA = new MarchaEnum("4ª", 260);
    public static readonly MarchaEnum QUINTA = new MarchaEnum("5ª", 240);
    public static readonly MarchaEnum SEXTA = new MarchaEnum("6ª", 220);

    public string Nome { get; }
    public int Torque { get; }

    private MarchaEnum(string nome, int torque)
    {
        Nome = nome;
        Torque = torque;
    }

    public override string ToString()
    {
        return $"{Nome} (Torque: {Torque} Nm)";
    }


}