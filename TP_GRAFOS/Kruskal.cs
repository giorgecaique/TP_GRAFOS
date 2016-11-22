using System.Collections.Generic;

namespace TP_GRAFOS
{
    static public class Kruskal
    {
        /// <summary>
        /// Roda o algoritmo de Kruskal
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vLength"></param>
        /// <returns> Graph </returns>
        static public Graph Run(Graph graph, int vLength)
        {

            List<Edge> edges = graph.edges;
            Graph T = new Graph(1);
            int nIncluded = 0;
            int counter = 0;

            while(nIncluded < vLength - 1)
            {
                Vertex[] temp = new Vertex[2];
                temp[0] = edges[nIncluded].borders[0];
                temp[1] = edges[nIncluded].borders[1];

                Edge e = edges[counter];
                T.vertex = graph.vertex;

                if ((e.borders[0].Id != e.borders[1].Id) && (e.Weight > 0))
                {
                  T.edges.Add(e);
                  nIncluded++;
                  edges.RemoveAt(0); // Remove as duas arestas do grafo original pois são paralelas
                  
                }
                
                counter++;
            }

            return T;
        }
    }
}
