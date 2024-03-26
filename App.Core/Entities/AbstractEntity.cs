using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public abstract class AbstractEntity
    {
        [Key]
        public int Id { get; set; }

        protected AbstractEntity()
        {
                
        }
        protected AbstractEntity(int id)
        {
            Id = id;
        }
    }
}
