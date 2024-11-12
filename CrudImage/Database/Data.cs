
using CrudImage.Core;
using Microsoft.EntityFrameworkCore;

namespace CrudImage.Database;

public class Data : DbContext
{
    public Data(DbContextOptions<Data> options) : base(options) { }
    public DbSet<ImageData> Images { get; set; }
}