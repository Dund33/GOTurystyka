using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Gottier
    {
        public Gottier()
        {
            Got3s = new HashSet<Got3>();
            Got4s = new HashSet<Got4>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<Got3> Got3s { get; set; }
        public virtual ICollection<Got4> Got4s { get; set; }
    }
}