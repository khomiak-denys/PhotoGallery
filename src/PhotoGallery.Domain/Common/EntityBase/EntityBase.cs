namespace PhotoGallery.Domain.Common.EntityBase;

public class EntityBase 
    {
        public Guid Id { get; protected set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }


        protected EntityBase(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id must be valid.");

            Id = id;
        }
        protected EntityBase() { }

        protected void OnCreate()
        {
            Created = DateTime.UtcNow;
        }
        protected void OnModify()
        {
            Modified = DateTime.UtcNow;
        }
    }