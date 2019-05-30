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

				PegarValoresParaTabela();

				var modeloCoulunas = CriarColunas();
				CriarCabecalho(modeloCoulunas);

				var colunas = PreencherColuna(modeloCoulunas);
				var matriz = CriarMatriz(modeloCoulunas, colunas);

				ExibirMatriz(matriz);
			}
			catch (Exception)
			{
				Console.WriteLine("Número inválido");
			}
		}
		private static void PegarValoresParaTabela()
		{
			Console.Write("Digite o número de elementos\n");
			quantidadeElementos = int.Parse(Console.ReadLine());

			//O número de linhas na tabela verdade é igual a 2 elevado ao número de elementos.
			quantidadeLinhas = (int)Math.Pow(2, quantidadeElementos);
			Console.Write("Número de linhas : " + quantidadeLinhas + "\n\n");
		}
		private static List<Coluna> CriarColunas()
		{
			var colunas = new List<Coluna>();
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

			return colunas;
		}
		private static void CriarCabecalho(List<Coluna> colunas)
		{
			Console.Write("Tabela Verdade : \n");
			var strColunas = "";

			//Criação da primeira linha da tabela, também chamada de header, onde são nomeadas as colunas.
			foreach (var coluna in colunas)
			{
				strColunas = $"{strColunas}{Convert.ToChar(coluna.numero + 65)} - ";
			}
			Console.WriteLine($" {strColunas.Remove(strColunas.Length - 2)}");
		}
		private static List<int>[] PreencherColuna(List<Coluna> colunas)
		{
			/*
			Para cada coluna é criado uma lista de números, já com a regra de repetição
			implementada e preenchida.
			*/

			var colunasPreenchidas = new List<int>[quantidadeElementos];
			foreach (var col in colunas)
			{
				colunasPreenchidas[col.numero] = new List<int>();
				for (var i = 0; i < quantidadeLinhas; i++)
				{
					for (var j = 0; j < col.regra; j++)
					{
						colunasPreenchidas[col.numero].Add(0);
					}
					for (var j = 0; j < col.regra; j++)
					{
						colunasPreenchidas[col.numero].Add(1);
						i++;
					}
				}
			}

			return colunasPreenchidas;
		}
		private static int[,] CriarMatriz(List<Coluna> colunas, List<int>[] colunasPreenchidas)
		{
			var indexLinha = 0;
			var matriz = new int[quantidadeLinhas, quantidadeElementos];
			//Agora preenchemos a matriz com as colunas e suas listas, sendo essas suas linhas na matriz.
			while (indexLinha < quantidadeLinhas)
			{
				foreach (var coluna in colunas)
				{
					matriz[indexLinha, coluna.numero] = colunasPreenchidas[coluna.numero][indexLinha];
				}
				indexLinha++;
			}
			return matriz;
		}
		public static void ExibirMatriz(int[,] matriz)
		{
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
	}
}

