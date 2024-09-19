using Newtonsoft.Json;
using System.Text.Json;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;

namespace TechWizWebApp.Services
{
    public interface ISeedService
    {
        public void SeedProduct();
    }

    public class SeedService : ISeedService
    {
        private readonly DecorVistaDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        public SeedService(IWebHostEnvironment webHostEnvironment,DecorVistaDbContext decorVistaDbContext)
        {
            _environment = webHostEnvironment;
            _dbContext = decorVistaDbContext;
        }
        public void SeedProduct()
        {
            var jsonEntityPath = Path.Combine(_environment.ContentRootPath,"Data","MockData","product.json");

            var jsonContent = System.IO.File.ReadAllText(jsonEntityPath);

            var product = JsonConvert.DeserializeObject<List<Product>>(jsonContent);

            _dbContext.Products.AddRange(product);
            _dbContext.SaveChanges();
        }
    }
}
