namespace EntityFrameworkExample.Data

{
   public static class Seeder
   {
       private static readonly List<String> _names = new List<String>{
         "John",
         "James",
         "Jenny",
         "Jessica",
         "Jessy"
       };

       public static void Seed(this EntityContext entityContext)
       {
           if (!entityContext.Entities.Any())
           {
            var entities = new List<Entity>();

            int i = 0;
            foreach (String n in _names) 
            {
                i++;
                Entity entity = new Entity{EntityId = i, Name = n};
                entities.Add(entity);
            }
            
            entityContext.AddRange(entities);
            entityContext.SaveChanges();
          }
       }
   }
}