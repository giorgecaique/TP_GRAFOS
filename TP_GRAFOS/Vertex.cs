using System.Collections.Generic;

namespace TP_GRAFOS
{
    public class Vertex
    {
        #region Properties

        public int Id { get; set; }
        public List<Student> students = new List<Student>();

        #endregion

        #region Constructors

        public Vertex(int i)
        {
            this.Id = i;
        }

        #endregion

    }
}
