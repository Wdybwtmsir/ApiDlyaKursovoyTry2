namespace ApiDlyaKursovoyTry2.Services;
using ApiDlyaKursovoyTry2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class NumbersOtherService : IService<NumbersOther>
{
    private readonly NormalnayaKursovayaContext db;
    public NumbersOtherService(NormalnayaKursovayaContext _db) => this.db = _db;
    public async Task<IEnumerable<NumbersOther>> GetAll()
    {
        return await db.NumbersOthers.ToListAsync();
    }
    public async Task<NumbersOther> GetById(int id)
    {
        return await db.NumbersOthers.FindAsync(id);
    }
    public async Task Create(NumbersOther entity)
    {
        db.NumbersOthers.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var nmbo = await db.NumbersOthers.FindAsync(id);
        if (nmbo != null)
        {
            db.NumbersOthers.Remove(nmbo);
            await db.SaveChangesAsync();
        }
    }
    public async Task Update(NumbersOther entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        db.NumbersOthers.Update(entity);
        await db.SaveChangesAsync();
    }
}

