using System.Collections.Generic;
using System.Linq;

namespace TP_GRAFOS
{
    public class Graph
    {
        #region Properties

        public Vertex[] vertex;
        public List<Edge> edges = new List<Edge>();

        #endregion

        #region Constructors

        public Graph(int i)
        {
            //  Construtor alternativo
        }

        public Graph()
        {
            vertex = IOOperations.GetResearchAreaVector();
            PopulateEdges();
            Sort();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Preenche a matriz de arestas
        /// </summary>
        public void PopulateEdges()
        {
            int[,] temp = IOOperations.PopulateMatrix(); // Arquivo temporário

            int counter = 0;
            for (int i = 0; i < vertex.Length; i++)
            {
                for (int j = 0; j < vertex.Length; j++)
                {
                    Edge tempEdge = new Edge();
                    tempEdge.Weight = temp[i, j]; // Adiciona o peso da aresta
                    tempEdge.borders[0] = vertex[i]; // Adiciona o vértice de uma borda  
                    tempEdge.borders[1] = vertex[j]; // Adiciona o vértice da outra borda
                    edges.Add(tempEdge);
                    counter++;
                }
            }
        }

        public void Sort()
        {
            edges = edges.OrderBy(o => o.Weight).ToList();
        }

        #endregion
    }
}
