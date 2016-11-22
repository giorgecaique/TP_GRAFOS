using System.Collections.Generic;

namespace TP_GRAFOS
{
    public class Group
    {
        #region Properties

        public int Teacher { get; set; }
        public List<Student> students = new List<Student>();

        #endregion

        #region Constructors

        public Group(int teacher)
        {
            this.Teacher = teacher;
        }

        #endregion
    }
}
