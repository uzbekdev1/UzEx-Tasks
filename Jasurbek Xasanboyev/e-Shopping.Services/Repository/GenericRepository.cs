﻿using e_Shopping.Data.Contexts;
using e_Shopping.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace e_Shopping.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        #region Private Members

        private readonly AppDbContext _context;

        private DbSet<T> _db;

        #endregion

        #region Constructors

        public GenericRepository(AppDbContext context)
        {
            _context = context;

            _db = _context.Set<T>();
        }

        #endregion


        #region CRUD

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            //await _db.BulkDeleteAsync(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes is not null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (includes is not null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes is not null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }


            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }


        public async Task InsertRange(IEnumerable<T> entities)
        {
            //await _db.BulkInsertAsync(entities);
        }

        public async Task<IPagedList<T>> SearchPagedList(RequestParams requestParams, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (requestParams.Search != null && expression != null)
            {
                query = query.Where(expression);
            }

            if (includes is not null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
