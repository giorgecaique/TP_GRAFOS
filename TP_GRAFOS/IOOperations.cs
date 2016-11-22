using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TP_GRAFOS
{
    static public class IOOperations
    {
        static private  int counter = 0;

        #region Methods

        /// <summary>
        /// Popula lista de estudantes
        /// </summary>
        /// <returns> List<Student> </returns>
        static public List<Student> PopulateStudentList()
        {
            List<Student> students = new List<Student>();
            StreamReader reader = new StreamReader("entrada999.txt");
            string line;
            for (int i = 0; !reader.EndOfStream; i++)
            {
                line = reader.ReadLine();
                string[] splitaux = line.Split(' ');
                Student temp = new Student(int.Parse(splitaux[0]), int.Parse(splitaux[1]));
                students.Add(temp);
            }
            reader.Close();
            return students;
        }

        /// <summary>
        /// Retorna lista de dissimilaridade
        /// </summary>
        /// <returns> List<string> </returns>
        static public List<string> GetList()
        {

            List<string> temp = new List<string>();
            StreamReader reader = new StreamReader("areaPesquisa20.txt");
            string line;
            for (int i = 0; !reader.EndOfStream; i++)
            {
                line = reader.ReadLine();
                temp.Add(line);
                counter++;
            }
            reader.Close();
            return temp;
        }

        /// <summary>
        /// Popula matriz de dissimilaridade 
        /// </summary>
        /// <returns> int[,] </returns>
        static public int[,] PopulateMatrix()
        {
            int[,] matrix;

            List<string> temp = new List<string>();
            temp =  GetList();

            matrix = new int[counter, counter];
            for (int i = 0; i < temp.Count; i++)
            {
                string[] splitaux = temp[i].Split(' ');
                for (int j = 0; j < splitaux.Length; j++)
                {
                        matrix[i, j] = int.Parse(splitaux[j]);
                }

            }
            return matrix;
        }

        /// <summary>
        /// retorna vetor de áreas de pesquisa que serão usadas como vértices
        /// </summary>
        /// <param name="NumProfessor"></param>
        /// <returns> int[,] </returns>
        static public Vertex[] GetResearchAreaVector()
        {
            List<Student> students;
            int[,] matrix;

            students = IOOperations.PopulateStudentList();
            matrix = IOOperations.PopulateMatrix();

            Vertex[] researchareas = new Vertex[counter];

            int x = 0;

            for (int i = x; i < researchareas.Length; i++) // Estrutura de repetição para percorrer todas áreas de pesquisa
            {
                    
                
                
                var query = from student in students  // Linq para obter os estudantes de cada área de pesquisa
                            where student.ResearchField == i 
                            select student;

                researchareas[i] = new Vertex(i);

                foreach (Student student in query)
                {
                    researchareas[i].students.Add(student);
                }
            }

            return researchareas;
        }


        #endregion
    }
}
