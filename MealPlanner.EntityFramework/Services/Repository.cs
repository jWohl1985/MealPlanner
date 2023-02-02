using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;

namespace MealPlanner.EntityFramework.Services;

public class Repository<T> : IRepository<T> where T : DomainObject
{
    private readonly DietContextFactory _contextFactory;

    public Repository(DietContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEnumerable<T> GetAll()
    {
        using DietContext context = _contextFactory.CreateDbContext();
        return context.Set<T>().ToList();
    }

    public async Task<T> CreateAsync(T entity)
    {
        using DietContext context = _contextFactory.CreateDbContext();

        EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        return createdResult.Entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using DietContext context = _contextFactory.CreateDbContext();

        T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

        if (entity is null)
            return false;

        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using DietContext context = _contextFactory.CreateDbContext();

        IEnumerable<T> entities = await context.Set<T>().ToListAsync();
        return entities;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        using DietContext context = _contextFactory.CreateDbContext();

        T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }

    public async Task<T> UpdateAsync(int id, T entity)
    {
        using DietContext context = _contextFactory.CreateDbContext();

        entity.Id = id;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}
