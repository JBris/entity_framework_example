using Microsoft.AspNetCore.Mvc;
using EntityFrameworkExample.Data;

namespace EntityFrameworkExample.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityFrameworkController : ControllerBase
{
   private readonly EntityContext _entityContext;

   public EntityFrameworkController (EntityContext entityContext)
   {
       _entityContext = entityContext;
   }

   [HttpGet]
   public ActionResult Get(int take = 10, int skip = 0)
   {
       return Ok(_entityContext.Entities.OrderBy(e => e.EntityId).Skip(skip).Take(take));
   }

   [HttpPost]
   public ActionResult Post()
   {
       return Ok(_entityContext.Database.EnsureCreated());
   }

   [HttpPut]
   public ActionResult Put(int take = 10, int skip = 0)
   {
       _entityContext.Seed();
       return Get(take, skip);
   }

   [HttpDelete]
   public ActionResult Delete()
   {
       return Ok(_entityContext.Database.EnsureDeleted());
   }
}