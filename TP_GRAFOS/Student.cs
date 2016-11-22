namespace TP_GRAFOS
{
    public class Student
    {

        #region Properties

        public int ResearchField { get; set; }
        public int StudentCode { get; set; }

        #endregion

        #region Constructor

        public Student(int sc, int rf)
        {
            this.StudentCode = sc;
            this.ResearchField = rf;
        }

        public Student()
        {

        }

        #endregion
    }
}
