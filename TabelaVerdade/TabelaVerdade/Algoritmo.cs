using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabelaVerdade
{
	public class Algoritmo
	{
		private static int quantidadeElementos;

		private static int quantidadeLinhas;

		public static void Main(string[] args)
		{
			var continuar = true;
			do
			{
				GerarTabela();
				Console.WriteLine("Deseja construir uma nova tabela? (Y/N)");
				if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
				{
					Console.Clear();
					continuar = true;
				}
				else
				{
					continuar = false;
				}
			} while (continuar);
			Console.ReadKey();
		}

		private static void GerarTabela()
		{
			try
			{
				Console.Write("Digite o número de elementos\n");
				quantidadeElementos = int.Parse(Console.ReadLine());

				//O número de linhas na tabela verdade é igual a 2 elevado ao número de elementos.
				quantidadeLinhas = (int)Math.Pow(2, quantidadeElementos);
				Console.Write("Número de linhas : " + quantidadeLinhas + "\n\n");

				//Declaração de listas e Matrizes
				var colunas = new List<Coluna>();
				var linha = new List<int>[quantidadeElementos];
				var matriz = new int[quantidadeLinhas, quantidadeElementos];

				//Preenchimento da lista de elementos, sendo cada elemento uma "Coluna" da tabela verdade.
				for (var i = 0; i < quantidadeElementos; i++)
				{
					var col = new Coluna
					{
						numero = i,
						//Determinando a regra de repetição de cada coluna
						regra = ((int)Math.Pow(2, i + 1)) / 2
					};
					colunas.Add(col);
				}

				Console.Write("Tabela Verdade : \n");
				var strColunas = "";

				//Criação da primeira linha da tabela, também chamada de header, onde são nomeadas as colunas.
				foreach (var col in colunas)
				{
					strColunas = $"{strColunas}{Convert.ToChar(col.numero + 65)} - ";
				}
				Console.WriteLine($" {strColunas.Remove(strColunas.Length - 2)}");


				/*
                    Para cada coluna é criado uma lista de números, já com a regra de repetição
				    implementada e preenchida.
                */

				foreach (var col in colunas)
				{
					linha[col.numero] = new List<int>();
					for (var i = 0; i < quantidadeLinhas; i++)
					{
						for (var j = 0; j < col.regra; j++)
						{
							linha[col.numero].Add(0);
						}
						for (var j = 0; j < col.regra; j++)
						{
							linha[col.numero].Add(1);
							i++;
						}
					}
				}

				var indexLinha = 0;
				//Agora preenchemos a matriz com as colunas e suas listas, sendo essas suas linhas na matriz.
				while (indexLinha < quantidadeLinhas)
				{
					foreach (var col in colunas)
					{
						matriz[indexLinha, col.numero] = linha[col.numero][indexLinha];
					}
					indexLinha++;
				}

				//Agora é só imprimir a matriz na tela e teremos uma tabela verdade.
				for (var i = 0; i < matriz.GetLength(0); i++)
				{
					for (var j = matriz.GetLength(1) - 1; j >= 0; j--)
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
		}
	}
}

