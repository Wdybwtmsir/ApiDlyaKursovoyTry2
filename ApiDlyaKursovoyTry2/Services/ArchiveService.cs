namespace ApiDlyaKursovoyTry2.Services;
using ApiDlyaKursovoyTry2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ArchiveService : IService<Archive>
{
    private readonly NormalnayaKursovayaContext db;
    public ArchiveService(NormalnayaKursovayaContext _db) => this.db = _db;
    public async Task<IEnumerable<Archive>> GetAll()
    {
        return await db.Archives.ToListAsync();
    }
    public async Task<Archive> GetById(int id)
    {
        return await db.Archives.FindAsync(id);
    }

    public async Task<IEnumerable<Archive>> GetByName(string name)
    {
        return await db.Archives.Where(p=>p.FirstName!.Contains(name)).ToListAsync();
    }
    public async Task Create(Archive entity)
    {
        db.Archives.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var arch = await db.Archives.FindAsync(id);
        if (arch != null)
        {
            db.Archives.Remove(arch);
            await db.SaveChangesAsync();
        }
    }
    public async Task Update(Archive entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        db.Archives.Update(entity);
        await db.SaveChangesAsync();
    }
}

