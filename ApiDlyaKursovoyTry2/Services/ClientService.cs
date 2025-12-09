namespace ApiDlyaKursovoyTry2.Services;
using ApiDlyaKursovoyTry2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ClientService : IService<Client>
{
    private readonly NormalnayaKursovayaContext db;
    public ClientService(NormalnayaKursovayaContext _db) => this.db = _db;
    public async Task<IEnumerable<Client>> GetAll()
    {
        return await db.Clients.ToListAsync();
    }
    public async Task<Client> GetById(int id)
    {
        return await db.Clients.FindAsync(id);
    }
    public async Task Create(Client entity)
    {
        db.Clients.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var clie = await db.Clients.FindAsync(id);
        if (clie != null)
        {
            db.Clients.Remove(clie);
            await db.SaveChangesAsync();
        }
    }
    public async Task Update(Client entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        db.Clients.Update(entity);
        await db.SaveChangesAsync();
    }
}

