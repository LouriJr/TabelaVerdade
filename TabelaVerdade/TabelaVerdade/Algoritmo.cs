using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabelaVerdade
{
    class Algoritmo
    {
        static void Main(string[] args)
        {
            int qtdElementos, qtdLinhas;
        Inicio:
            try
            {
                Console.Write("Digite o número de elementos\n");
                qtdElementos = int.Parse(Console.ReadLine());

                //O número de linhas na tabela verdade é igual a 2 elevado ao número de elementos.
                qtdLinhas = (int)Math.Pow(2, qtdElementos);
                Console.Write("Número de linhas : " + qtdLinhas + "\n\n");

                //Declaração de listas e Matrizes
                List<Coluna> colunas = new List<Coluna>();
                List<int>[] listLinha = new List<int>[qtdElementos];
                int[,] matriz = new int[qtdLinhas, qtdElementos];

                //Preenchimento da lista de elementos, sendo cada elemento uma "Coluna" da tabela verdade.
                for (int i = 0; i < qtdElementos; i++)
                {
                    Coluna col = new Coluna();
                    col.numero = i;
                    //Determinando a regra de repetição de cada coluna
                    col.regra = ((int)Math.Pow(2, i + 1)) / 2;
                    colunas.Add(col);
                }

                Console.Write("Tabela Verdade : \n");
                string strColunas = "";

                //Criação da primeira linha da tabela, também chamada de header, onde são nomeadas as colunas.
                foreach (Coluna col in colunas)
                {
                    strColunas = strColunas + Convert.ToChar(col.numero + 65) + " - ";
                }
                Console.WriteLine(" " + strColunas.Remove(strColunas.Length - 2));


                /*
                    Para cada coluna é criado uma lista de números, já com a regra de repetição
                implementada e preenchida.
                */
                foreach (Coluna col in colunas)
                {
                    listLinha[col.numero] = new List<int>();
                    for (int i = 0; i < qtdLinhas; i++)
                    {
                        for (int j = 0; j < col.regra; j++)
                        {
                            listLinha[col.numero].Add(0);
                        }
                        for (int j = 0; j < col.regra; j++)
                        {
                            listLinha[col.numero].Add(1);
                            i++;
                        }
                    }
                }

                int linha = 0;
                //Agora preenchemos a matriz com as colunas e suas listas, sendo essas suas linhas na matriz.
                while (linha < qtdLinhas)
                {
                    foreach (Coluna col in colunas)
                    {
                        matriz[linha, col.numero] = listLinha[col.numero][linha];
                    }
                    linha++;
                }

                //Agora é só imprimir a matriz na tela e teremos uma tabela verdade.
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = matriz.GetLength(1) - 1; j >= 0; j--)
                    {
                        Console.Write(" " + matriz[i, j] + "  ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Número inválido");
            }
            Console.WriteLine("Deseja construir uma nova tabela? (Y/N)");
            if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
            {
                Console.Clear();
                goto Inicio;
            }
            else
            {
                Console.ReadKey();
            }
        }
    }
}

