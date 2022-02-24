namespace Music.Views.GlobalViews
{
    public class NameDTO<T>
    {
        public NameDTO(T id, string name)
        {
            Id = id;
            Name = name;
        }
        public NameDTO()
        {

        }
        public T Id { get; set; }
        public string Name { get; set; }
    }
}