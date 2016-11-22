using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TP_GRAFOS
{
    class Program
    {
        #region Global Objects

        static Graph graph; // Grafo inicial 

        static Stopwatch watch; // Marca o tempo de execução do programa

        static Graph minimalSpanningTree = new Graph(1); // árvore gerada pelo algoritmo de Kruskal

        static Group[] groups; // Grupos

        #endregion

        #region Methods

        /// <summary>
        /// Cria os grupos e os preenche
        /// </summary>
        /// <param name="k"> número de professores </param>
        static void CreateGroups(int k) // Recebe o número de professores por parâmetro
        {
            groups = new Group[k]; // O número de grupos é igual ao número de professores 

            List<int> vIncluded = new List<int>(); // Lista de Ids dos vértices incluídos 


            if (k > graph.vertex.Length) // Se o número de professores for maior que o número de áreas de pesquisa
            {
                throw new Exception("Número de professores deve ser menor que número de áreas de pesquisa");
            }

            else if (k == graph.vertex.Length) // se o número de professores for igual ao número de áreas de pesquisa 
            {
                for (int i = 0; i < minimalSpanningTree.vertex.Length; i++) // enquanto i for menor que o número de vértices da árvore geradora mínima
                {
                    var query = from Vertex vertex in minimalSpanningTree.vertex // Linq query para encontrar a lista de alunos de cada área de pesquisa
                                where vertex.Id == i
                                select minimalSpanningTree.vertex[i].students;

                    foreach (List<Student> item in query) // Cria um grupo com esses alunos
                    {
                        groups[i] = new Group(i);
                        groups[i].students = minimalSpanningTree.vertex[i].students;
                    }
                }
            }

            else // se o número de professores for menor que o número de áreas de pesquisa
            {
                int tempK = k, tempV = minimalSpanningTree.vertex.Length, tempCounter = 0; // declaração de variáveis temporárias para gerar os grupos. 
                int temp = tempV - tempK; // variável temporária da diferença entre o número de vértices e o número de professores

                for (int i = 0; i < k; i++)
                {
                    if ((tempCounter < temp && temp < k) && (!(vIncluded.Contains(minimalSpanningTree.edges[i].borders[0].Id)) && (!(vIncluded.Contains(minimalSpanningTree.edges[i].borders[1].Id))))) // contador tem que ser menor que diferença entre número de v e k. As extremidades da aresta não devem ter sido incluídas 
                    {
                        groups[tempCounter] = new Group(tempCounter);
                        groups[tempCounter].students = minimalSpanningTree.edges[i].borders[0].students;  //adiciona os alunos ao grupo
                        groups[tempCounter].students.AddRange(minimalSpanningTree.edges[i].borders[1].students); // adiciona os alunos ao grupo
                        vIncluded.Add(minimalSpanningTree.edges[i].borders[0].Id);  // coloca o vértice nos incluídos
                        vIncluded.Add(minimalSpanningTree.edges[i].borders[1].Id);  // coloca o vértice nos incluídos
                        tempV -= 2;
                        tempK--;
                        tempCounter++;
                    }
                }

                if(tempCounter == k) // se o número dos grupos feitos for igual ao número de professores, adicione o resto dos alunos a esses grupos
                {
                    for(int i = 0; i < k; i++)
                    {
                        var query = from Vertex v in minimalSpanningTree.vertex
                                    where !vIncluded.Contains(v.Id)
                                    select v;

                        foreach (Vertex v in query)
                        {
                            groups[i].students.AddRange(v.students);
                            vIncluded.Add(v.Id);
                            break;
                        }

                        tempV--;
                        tempK--;
                    }
                }
                for (int i = tempCounter; i < k; i++) 
                {
                    groups[i] = new Group(i);


                    if ((tempK == tempV) && (!(vIncluded.Contains(minimalSpanningTree.vertex[i].Id)))) // se o número de professores for igual o número de áreas de pesquisa
                    {
                     
                        groups[i].students = minimalSpanningTree.vertex[i].students; // atribui a lista de estudantes ao grupo
                        vIncluded.Add(minimalSpanningTree.vertex[i].Id);
                        
                    }
                    else if(tempK == 1) // se tiver apenas um professor 
                    {
                        foreach(Vertex item in minimalSpanningTree.vertex) 
                        {
                            if (!vIncluded.Contains(item.Id))
                            {
                                groups[i].students.AddRange(item.students);  // adiciona todos os alunos ao grupo dele
                            }
                        }
                        break;
                    }
                    else // se k < v
                    {
                        
                        if(!(vIncluded.Contains(minimalSpanningTree.edges[i].borders[0].Id))) // se apenas o vértice 1 das extremidades não estiver incluído
                        {
                            groups[i].students = minimalSpanningTree.edges[i].borders[0].students;
                            vIncluded.Add(minimalSpanningTree.edges[i].borders[0].Id);
                            tempV--;
                            tempK--;
                        }
                        else if(!(vIncluded.Contains(minimalSpanningTree.edges[i].borders[1].Id))) // se apenas o vértice 2 das extremidades não estiver incluído
                        {
                            groups[i].students = minimalSpanningTree.edges[i].borders[1].students;
                            vIncluded.Add(minimalSpanningTree.edges[i].borders[1].Id);
                            tempV--;
                            tempK--;
                        }
                        else
                        {
                            var query = from Vertex v in minimalSpanningTree.vertex
                                        where !vIncluded.Contains(v.Id)
                                        select v;

                            foreach (Vertex v in query)
                            {
                                groups[i].students = v.students;
                                vIncluded.Add(v.Id);
                                break;
                            }

                            tempV--;
                            tempK--;

                        }
                    }
                }



            }
            Print();



        }

        /// <summary>
        /// Imprime o resultado
        /// </summary>
        static void Print()
        {
            Console.WriteLine("{0} {1}", "Arestas ", " Peso\n");

            foreach(Edge item in minimalSpanningTree.edges) // para cada aresta na árvore geradora mínima, imprima a aresta e o seu peso
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", item.borders[0].Id, " - ", item.borders[1].Id,"  " ,item.Weight);
            }
            Console.WriteLine("\n-------------------------------------------------\n");
            for (int i = 0; i < groups.Length; i++) // Para cada grupo criado, imprima o professor e o código dos alunos daquele grupo
            {
                Console.WriteLine("Professor: " + (i+1) +" \n");
                Console.Write("Código dos alunos: ");
                for (int j = 0; j < groups[i].students.Count; j++)
                {
                    Console.Write(groups[i].students[j].StudentCode + " ");
                }
                Console.WriteLine("\n-------------------------------------------------\n");
            }
        }

        /// <summary>
        /// Recebe os dados de entrada por teclado
        /// </summary>
        static void GetInput() 
        {
            try
            {
                int k; // Número de professores
                Console.Write("Digite a quantidade de professores: ");
                k = int.Parse(Console.ReadLine());
                if (k == 0)
                {
                    Console.WriteLine("\nO número de professores deve ser maior que zero \n");
                    GetInput();
                }
                Console.WriteLine();
                watch.Start();

                CreateGroups(k); // Chama método para criar os grupos
            }
            catch (FormatException) { Console.WriteLine("Formato inválido"); }
            catch (Exception e) { Console.WriteLine("{0} {1}", "Erro: ", e.Message); }
        }

        static void Header()
        {
            Console.WriteLine("|Trabalho Prático - Algoritmos em Grafos|");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("| Grupo:                                |");
            Console.WriteLine("|                                       |");
            Console.WriteLine("| Giorge Caique                  548749 |");
            Console.WriteLine("| Gabriel Correa                 491260 |");
            Console.WriteLine("| Renato Souza                   516729 |");
            Console.WriteLine("|---------------------------------------|");
        }

        #endregion

        static void Main(string[] args)
        {
            Header();

            watch = new Stopwatch(); 
            watch.Start();

            graph = new Graph();
            minimalSpanningTree = Kruskal.Run(graph, graph.vertex.Length); 

            watch.Stop();

            GetInput();

            watch.Stop();
            Console.WriteLine("{0} {1}", "Tempo de processamento: " ,watch.Elapsed);
            Console.ReadKey();
        }
    }
}
